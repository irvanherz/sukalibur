using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Sukalibur.Shared.Options
{
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtAuthOptions _options;

        public ConfigureJwtBearerOptions(IOptions<JwtAuthOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public void Configure(JwtBearerOptions options) => configureJwtBearerOptions(options);

        public void Configure(string? name, JwtBearerOptions options) => configureJwtBearerOptions(options);

        private void configureJwtBearerOptions(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _options.Issuer, // Replace with your issuer
                ValidAudience = _options.Audience, // Replace with your audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret!)),
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.HttpContext.Items["JwtAuthenticationFailure"] = context.Exception;
                    return Task.CompletedTask;
                }
            };
        }
    }
}
