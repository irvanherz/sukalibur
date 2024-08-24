using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sukalibur.Graph.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sukalibur.Graph.Auth
{
    public class AuthService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        public AuthService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper, JwtConfig jwtConfig)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _jwtConfig = jwtConfig;
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterUserInput input)
        {
            await using var context = _contextFactory.CreateDbContext();
            var user = _mapper.Map<User>(input);
            user.Password = HashPassword(input.Password);

            context.Users.Add(user);

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiredAt = DateTime.Now.AddDays(7)
            });
            await context.SaveChangesAsync();

            return new AuthResult
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResult> LoginUserAsync(LoginUserInput input)
        {
            await using var context = _contextFactory.CreateDbContext();

            var user = await context.Users.Where(u => u.Username == input.Username).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (IsPasswordMatch(input.Password, user.Password) == false)
            {
                throw new Exception("Password is incorrect");
            }

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiredAt = DateTime.Now.AddDays(7)
            });
            await context.SaveChangesAsync();

            return new AuthResult
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResult> RefreshTokenAsync(RefreshTokenInput input)
        {
            await using var context = _contextFactory.CreateDbContext();

            var refreshTokenData = await context.RefreshTokens.Where(t => t.Token == input.RefreshToken).FirstOrDefaultAsync();
            if (refreshTokenData == null)
            {
                throw new Exception("Refresh token not found");
            }
            if (refreshTokenData.ExpiredAt < DateTime.Now)
            {
                throw new Exception("Refresh token is expired");
            }
            var user = await context.Users.Where(u => u.Id == refreshTokenData.UserId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var accessToken = GenerateAccessToken(user);
            refreshTokenData.ExpiredAt = DateTime.Now.AddDays(7);

            await context.SaveChangesAsync();

            user.Password = string.Empty;

            return new AuthResult
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshTokenData.Token
            };
        }

        private bool IsPasswordMatch(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var additionalClaims = new List<Claim>() {
                new Claim("sub", user.Id.ToString()),
                new Claim("role", user.Role.ToString())
            };
            var securityToken = new JwtSecurityToken(_jwtConfig.Issuer, _jwtConfig.Audience, additionalClaims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
