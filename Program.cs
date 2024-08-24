
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sukalibur.Graph;
using Sukalibur.Graph.Auth;
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
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
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
            builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
            {
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 25)));
            });
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = jwtTokenValidationParams;
                });

            builder.Services.AddSingleton(jwtConfig);
            builder.Services.AddSingleton(mapper);
      
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<OrganizerService>();
            builder.Services.AddScoped<TripService>();
            builder.Services.AddScoped<TripCategoryService>();
            builder.Services.AddScoped<UserService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddGraphQLServer()
                .RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
                .AddDefaultTransactionScopeHandler()
                .AddMutationConventions()
                .AddDataLoader<UserBatchDataLoader>()
                .AddDataLoader<OrganizerBatchDataLoader>()
                .AddDataLoader<TripBatchDataLoader>()
                .AddDataLoader<TripCategoryBatchDataLoader>()
                .AddMutationType<Mutation>()
                .AddQueryType<Query>()
                .AddTypeExtension<OrganizerExtendedFieldResolvers>()
                .AddTypeExtension<TripExtendedFieldResolvers>()
                .AddTypeExtension<AuthMutationResolvers>()
                .AddTypeExtension<UserQueryResolvers>()
                .AddTypeExtension<UserMutationResolvers>()
                .AddTypeExtension<OrganizerQueryResolvers>()
                .AddTypeExtension<OrganizerMutationResolvers>()
                .AddTypeExtension<TripQueryResolvers>()
                .AddTypeExtension<TripMutationResolvers>()
                .AddTypeExtension<TripCategoryQueryResolvers>()
                .AddTypeExtension<TripCategoryMutationResolvers>()
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
