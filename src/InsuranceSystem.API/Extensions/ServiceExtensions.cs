using AspNetCoreRateLimit;
using InsuranceSystem.API.Filters;
using InsuranceSystem.Entities;
using InsuranceSystem.RepositoryContracts.IUnitOfWork;
using InsuranceSystem.RepositoryServices.UnitOfWork;
using InsuranceSystem.ServiceContracts;
using InsuranceSystem.ServiceContracts.UtilityServiceInterface;
using InsuranceSystem.Services;
using InsuranceSystem.Services.UtilityServices;
using Marvin.Cache.Headers;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace InsuranceSystem.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
         {
             options.AddPolicy("CorsPolicy", builder =>
             builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithExposedHeaders("X-Pagination")
          );
         });

        public static void ConfigureLogger(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureFilters(this IServiceCollection services) =>
        services.AddScoped<ValidationFilterAttribute>();

        public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("InsuranceDBContext")));

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
            services.AddResponseCaching();
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
        services.AddHttpCacheHeaders(
        (expirationOpt) =>
        {
            expirationOpt.MaxAge = 65;
            expirationOpt.CacheLocation = CacheLocation.Private;
        },
        (validationOpt) =>
        {
            validationOpt.MustRevalidate = true;
        });

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                Endpoint = "*",
                Limit = 30,
                Period = "5m"
                }
             };
            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static class LoggerManagerLocator
        {
            private static readonly ILoggerManager _loggerManager;

            static LoggerManagerLocator()
            {
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();

                _loggerManager = new LoggerManager(logger);
            }

            public static ILoggerManager GetLoggerManager() => _loggerManager;
        }

    }
}
