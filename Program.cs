
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sukalibur.Graph;
using Sukalibur.Graph.Organizers;
using Sukalibur.Graph.Trips;
using Sukalibur.Graph.Users;
using System.Text;

namespace Sukalibur
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtConfig = builder.Configuration.GetSection("JWTConfig").Get<JwtConfig>()!;
            var jwtTokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer, // Replace with your issuer
                ValidAudience = jwtConfig.Audience, // Replace with your audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret!))
            };

            // Add services to the container.
            builder.Services.AddSingleton(jwtConfig);
            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 25)));
            });
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = jwtTokenValidationParams;
                });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddGraphQLServer()
                .RegisterDbContext<AppDbContext>()
                .AddDataLoader<UserBatchDataLoader>()
                .AddDataLoader<OrganizerBatchDataLoader>()
                .AddDataLoader<TripBatchDataLoader>()
                .AddDataLoader<TripCategoryBatchDataLoader>()
                .AddMutationType<Mutation>()
                .AddQueryType<Query>()
                .AddTypeExtension<UserQueryResolvers>()
                .AddTypeExtension<OrganizerQueryResolvers>()
                .AddTypeExtension<TripQueryResolvers>()
                .AddTypeExtension<TripCategoryQueryResolvers>()
                .AddFiltering()
                .AddSorting()
                .AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGraphQL();

            app.Run();
        }
    }
}
