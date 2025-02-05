using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using NodaTime.Extensions;
using Sukalibur.Graph.Carts;
using Sukalibur.Graph.Notifications;
using Sukalibur.Graph.Users;
using Sukalibur.Shared;
using Sukalibur.Shared.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sukalibur.Graph.Auth
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtAuthOptions _jwtConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthContext _authContext;
        private readonly EmailService _emailService;
        public AuthService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper, IOptions<JwtAuthOptions> jwtAuthOptions, IHttpContextAccessor httpContextAccessor, AuthContext authContext, EmailService emailService)
        {
            _context = contextFactory.CreateDbContext();
            _mapper = mapper;
            _jwtConfig = jwtAuthOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            _authContext = authContext;
            _emailService = emailService;
        }

        public async Task<AuthResult> SignupAsync(SignupInput input)
        {
            var user = _mapper.Map<User>(input);
            user.Email = user.Email.ToLower();
            user.Username = user.Username.ToLower();
            user.Password = HashPassword(input.Password);
            user.Carts.Add(new Cart());
            _context.Users.Add(user);

            var accessTokenTtl = _jwtConfig.AccessTokenTtl;
            var refreshTokenTtl = _jwtConfig.RefreshTokenTtl;
            var accessTokenExpiredAt = DateTime.UtcNow.AddSeconds(accessTokenTtl).ToInstant();
            var refreshTokenExpiredAt = DateTime.UtcNow.AddSeconds(refreshTokenTtl).ToInstant();

            var accessToken = _generateAccessToken(user, accessTokenTtl);
            var refreshToken = _generateRefreshToken(user);

            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiredAt = refreshTokenExpiredAt
            });
            await _context.SaveChangesAsync();
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refresh_token", refreshToken);

            return new AuthResult
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiredAt = accessTokenExpiredAt
            };
        }

        public async Task<AuthResult> SigninAsync(SigninInput input)
        {
            input.UsernameOrEmail = input.UsernameOrEmail.ToLower();
            var user = await _context.Users.Where(u => u.Username == input.UsernameOrEmail || u.Email == input.UsernameOrEmail).FirstOrDefaultAsync();
            if (user == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_CREDENTIALS").SetMessage("Invalid username or password").Build());

            if (_checkPasswordMatch(input.Password, user.Password) == false)
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_CREDENTIALS").SetMessage("Invalid username or password").Build());

            var accessTokenTtl = _jwtConfig.AccessTokenTtl;
            var refreshTokenTtl = _jwtConfig.RefreshTokenTtl;
            var accessTokenExpiredAt = DateTime.UtcNow.AddSeconds(accessTokenTtl).ToInstant();
            var refreshTokenExpiredAt = DateTime.UtcNow.AddSeconds(refreshTokenTtl).ToInstant();

            var accessToken = _generateAccessToken(user, accessTokenTtl);
            var refreshToken = _generateRefreshToken(user);

            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiredAt = refreshTokenExpiredAt
            });

            await _context.SaveChangesAsync();
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refresh_token", refreshToken);

            return new AuthResult
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiredAt = accessTokenExpiredAt
            };
        }

        public async Task<AuthResult> RefreshTokenAsync(RefreshTokenInput? input)
        {
            _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue("refresh_token", out var refreshTokenFromCookie);
            var refreshToken = refreshTokenFromCookie ?? input?.RefreshToken;
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_TOKEN").SetMessage("Invalid refresh token").Build());
            var refreshTokenData = await _context.RefreshTokens.Where(t => t.Token == refreshToken).FirstOrDefaultAsync();
            if (refreshTokenData == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_TOKEN").SetMessage("Invalid refresh token").Build());

            if (refreshTokenData.ExpiredAt < SystemClock.Instance.GetCurrentInstant())
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_TOKEN").SetMessage("Invalid refresh token").Build());

            var user = await _context.Users.Where(u => u.Id == refreshTokenData.UserId).FirstOrDefaultAsync();
            if (user == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_TOKEN").SetMessage("Invalid refresh token").Build());

            var accessTokenTtl = _jwtConfig.AccessTokenTtl;
            var refreshTokenTtl = _jwtConfig.RefreshTokenTtl;
            var accessTokenExpiredAt = DateTime.UtcNow.AddSeconds(accessTokenTtl).ToInstant();
            var refreshTokenExpiredAt = DateTime.UtcNow.AddSeconds(refreshTokenTtl).ToInstant();

            var accessToken = _generateAccessToken(user, accessTokenTtl);
            var newRefreshToken = _generateRefreshToken(user);
            refreshTokenData.Token = newRefreshToken;
            refreshTokenData.ExpiredAt = refreshTokenExpiredAt;

            await _context.SaveChangesAsync();
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refresh_token", refreshToken);

            return new AuthResult
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshTokenData.Token,
                AccessTokenExpiredAt = accessTokenExpiredAt
            };
        }

        private bool _checkPasswordMatch(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string _generateAccessToken(User user, long ttl)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var additionalClaims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var securityToken = new JwtSecurityToken(_jwtConfig.Issuer, _jwtConfig.Audience, additionalClaims,
              expires: DateTime.Now.AddSeconds(ttl),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        private string _generateRefreshToken(User user)
        {
            var userIdBytes = BitConverter.GetBytes(user.Id);
            var timestampBytes = BitConverter.GetBytes(SystemClock.Instance.GetCurrentInstant().ToUnixTimeSeconds());
            var randomBytes = new byte[8];
            RandomNumberGenerator.Fill(randomBytes);
            var combinedBytes = userIdBytes.Concat(timestampBytes).Concat(randomBytes).ToArray();
            return Convert.ToHexStringLower(combinedBytes);
        }

        public async Task<bool> TestSendMailAsync()
        {
            var user = await _context.Users.FindAsync(1);
            return await _emailService.SendWelcomeMessageAsync(user!);
        }

        internal async Task SignoutAsync(SignoutInput? input)
        {
            _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue("refresh_token", out var refreshTokenFromCookie);
            var refreshToken = refreshTokenFromCookie ?? input?.RefreshToken;
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_TOKEN").SetMessage("Invalid refresh token").Build());
            var refreshTokenData = await _context.RefreshTokens.Where(t => t.Token == refreshToken).FirstOrDefaultAsync();
            if (refreshTokenData == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_TOKEN").SetMessage("Invalid refresh token").Build());
            if (refreshTokenData.UserId != _authContext.CurrentUser!.Id)
                throw new GraphQLException(ErrorBuilder.New().SetCode("INVALID_TOKEN").SetMessage("Invalid refresh token").Build());

            _context.RefreshTokens.Remove(refreshTokenData);
            await _context.SaveChangesAsync();
            _httpContextAccessor.HttpContext!.Response.Cookies.Delete("refresh_token");
        }
    }
}
