
using Amazon.SimpleEmailV2;
using AppAny.HotChocolate.FluentValidation;
using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FluentValidation;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.PostgreSql;
using HotChocolate.Types.NodaTime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Npgsql;
using Sukalibur.Graph;
using Sukalibur.Graph.Auth;
using Sukalibur.Graph.Carts;
using Sukalibur.Graph.Medias;
using Sukalibur.Graph.Notifications;
using Sukalibur.Graph.Orders;
using Sukalibur.Graph.Organizers;
using Sukalibur.Graph.Payments;
using Sukalibur.Graph.Trips;
using Sukalibur.Graph.Users;
using Sukalibur.Shared;
using Sukalibur.Shared.Extensions;
using Sukalibur.Shared.Mapper;
using Sukalibur.Shared.Options;
using Sukalibur.Shared.Services;
using System.Text.Json;

namespace Sukalibur
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.GetApplicationDefault(),
            })!;
            var defaultMessaging = FirebaseMessaging.GetMessaging(defaultApp)!;

            builder.Services.Configure<JwtAuthOptions>(builder.Configuration.GetSection("JwtAuthOptions"));
            builder.Services.Configure<BunnyStorageOptions>(builder.Configuration.GetSection("BunnyStorageOptions"));
            builder.Services.Configure<MidtransOptions>(builder.Configuration.GetSection("MidtransOptions"));
            builder.Services.Configure<CommonOptions>(builder.Configuration.GetSection("CommonOptions"));
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddNpgsqlDataSource(connectionString, opts =>
            {
                opts
                    .EnableDynamicJson()
                    .UseNodaTime()
                    .UseNetTopologySuite()
                    .MapEnum<UserRole>()
                    .MapEnum<UserGender>()
                    .MapEnum<OrganizerStatus>()
                    .MapEnum<OrganizerMemberRole>()
                    .MapEnum<OrganizerMemberStatus>()
                    .MapEnum<TripStatus>()
                    .MapEnum<TripScheduleStatus>()
                    .MapEnum<TripPackageStatus>()
                    .MapEnum<TripAddonStatus>()
                    .MapEnum<TripReservationStatus>()
                    .MapEnum<OrderStatus>()
                    .MapEnum<PaymentChannelStatus>()
                    .MapEnum<PaymentChannelType>()
                    .MapEnum<MediaType>()
                    .MapEnum<FcmTokenDeviceType>()
                    .UseVector()
                    .ConfigureJsonOptions(new JsonSerializerOptions { AllowOutOfOrderMetadataProperties = true });
            });
            builder.Services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseNpgsql(o => o
                    .UseNodaTime()
                    .UseNetTopologySuite()
                    .MapEnum<UserRole>()
                    .MapEnum<UserGender>()
                    .MapEnum<OrganizerStatus>()
                    .MapEnum<OrganizerMemberRole>()
                    .MapEnum<OrganizerMemberStatus>()
                    .MapEnum<TripStatus>()
                    .MapEnum<TripScheduleStatus>()
                    .MapEnum<TripPackageStatus>()
                    .MapEnum<TripAddonStatus>()
                    .MapEnum<TripReservationStatus>()
                    .MapEnum<OrderStatus>()
                    .MapEnum<PaymentChannelStatus>()
                    .MapEnum<PaymentChannelType>()
                    .MapEnum<MediaType>()
                    .MapEnum<FcmTokenDeviceType>()
                    .UseVector()
                ).ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            });
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings(opts => opts
                    .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
                    .AddPgVectorJsonConverter())
                .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));
            builder.Services.AddHangfireServer();
            builder.Services.AddHttpClient();
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins(["http://localhost:5173"])
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
            builder.Services.AddSingleton<AmazonSimpleEmailServiceV2Client>();
            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mauifirebasedemo-firebase-adminsdk.json")),
            //});
            builder.Services.AddSingleton(mapper);
            builder.Services.AddSingleton<FirebaseApp>(defaultApp);
            builder.Services.AddSingleton<FirebaseMessaging>(defaultMessaging);
            builder.Services.AddSingleton<EmbeddingService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<OrganizerService>();
            builder.Services.AddScoped<OrganizerMemberService>();
            builder.Services.AddScoped<TripService>();
            builder.Services.AddScoped<TripCategoryService>();
            builder.Services.AddScoped<TripItineraryService>();
            builder.Services.AddScoped<TripScheduleService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<MediaService>();
            builder.Services.AddScoped<AuthContext>();
            builder.Services.AddScoped<PaymentService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<PushNotificationService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddGraphQLServer()
                .AddErrorFilter<ErrorFilter>()
                .AddAuthorization()
                .AddErrorFilter(error =>
                {
                    return error;
                })
                .AddSpatialTypes()
                .AddType<UploadType>()
                .AddType<InstantType>()
                .AddType<DurationType>()
                .AddType<PointSortInputType>() //https://github.com/dotnet/efcore/issues/27042
                .AddType<MediaData>()
                .AddType<ImageMediaData>()
                .AddType<VideoMediaData>()
                .AddType<DocumentMediaData>()
                .RegisterDbContextFactory<AppDbContext>()
                //.AddDefaultTransactionScopeHandler() //problematic, mutation errored
                .AddMutationConventions()
                .AddDataLoader<UserBatchDataLoader>()
                .AddDataLoader<OrganizerBatchDataLoader>()
                .AddDataLoader<OrganizerMemberBatchDataLoader>()
                .AddDataLoader<TripBatchDataLoader>()
                .AddDataLoader<TripCategoryBatchDataLoader>()
                .AddDataLoader<TripItineraryBatchDataLoader>()
                .AddDataLoader<TripScheduleBatchDataLoader>()
                .AddDataLoader<OrderBatchDataLoader>()
                .AddDataLoader<MediaBatchDataLoader>()
                .AddDataLoader<PaymentBatchDataLoader>()
                .AddMutationType<Mutation>()
                .AddQueryType<Query>()
                .AddSubscriptionType<Subscriptions>()
                .AddTypeExtension<AuthMutationResolvers>()
                .AddTypeExtension<AuthQueryResolvers>()
                .AddTypeExtension<UserQueryResolvers>()
                .AddTypeExtension<UserMutationResolvers>()
                .AddTypeExtension<OrganizerQueryResolvers>()
                .AddTypeExtension<OrganizerMutationResolvers>()
                .AddTypeExtension<OrganizerMemberQueryResolvers>()
                .AddTypeExtension<OrganizerMemberMutationResolvers>()
                .AddTypeExtension<TripQueryResolvers>()
                .AddTypeExtension<TripMutationResolvers>()
                .AddTypeExtension<TripCategoryQueryResolvers>()
                .AddTypeExtension<TripCategoryMutationResolvers>()
                .AddTypeExtension<TripItineraryQueryResolvers>()
                .AddTypeExtension<TripItineraryMutationResolvers>()
                .AddTypeExtension<TripScheduleQueryResolvers>()
                .AddTypeExtension<TripScheduleMutationResolvers>()
                .AddTypeExtension<CartQueryResolvers>()
                .AddTypeExtension<CartMutationResolvers>()
                .AddTypeExtension<OrderQueryResolvers>()
                .AddTypeExtension<OrderMutationResolvers>()
                .AddTypeExtension<MediaQueryResolvers>()
                .AddTypeExtension<MediaMutationResolvers>()
                .AddTypeExtension<PaymentQueryResolvers>()
                .AddTypeExtension<PaymentMutationResolvers>()
                .AddTypeExtension<NotificationQueryResolvers>()
                .AddTypeExtension<NotificationMutationResolvers>()
                .AddTypeExtension<NotificationSubscriptions>()
                .AddFluentValidation()
                .AddFiltering()
                .AddSpatialFiltering()
                .AddSorting()
                .AddProjections()
                .AddSpatialProjections()
                .AddInMemorySubscriptions();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseCors();

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseWebSockets();

            app.MapGraphQL();

            app.UseHangfireDashboard();

            app.Run();
        }
    }
}
