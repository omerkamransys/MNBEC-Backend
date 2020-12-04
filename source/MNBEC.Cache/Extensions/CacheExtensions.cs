using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MNBEC.CacheInterface;

namespace MNBEC.Cache.Extensions
{
    /// <summary>
    /// CacheExtensions class provides implementation for extension methods.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// RegisterCacheService registers cache components.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            string configurationValue = configuration.GetConnectionString("CacheConnection");
            string instanceName = configuration["Cache:InstanceName"];

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configurationValue;
                options.InstanceName = instanceName;
            });

            return services;
        }

        /// <summary>
        /// RegisterVehicleCacheService registers cache components for Vehicle.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterVehicleCacheService(this IServiceCollection services)
        {
           // services.AddSingleton<IMakeCache, MakeCache>();

            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }

        /// <summary>
        /// RegisterInspectionCentreCacheService registers cache components for Inspection Centre.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionCentreCacheService(this IServiceCollection services)
        {

            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();

            return services;
        }



        /// <summary>
        /// RegisterCommonCacheService registers cache components for Common.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommonCacheService(this IServiceCollection services)
        {

            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();

            return services;
        }


        /// <summary>
        /// RegisterAccountCacheService registers cache components for Common.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAccountCacheService(this IServiceCollection services)
        {
            services.AddSingleton<IRoleCache, RoleCache>();
            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }

        /// <summary>
        /// RegisterInspectionCacheService registers cache components for Inspection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionCacheService(this IServiceCollection services)
        {
            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }

        /// <summary>
        /// RegisterDocumentCacheService registers cache components for Document.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDocumentCacheService(this IServiceCollection services)
        {
            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }

        /// <summary>
        /// RegisterDealerCacheService registers cache components for Dealer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDealerCacheService(this IServiceCollection services)
        {
           // services.AddSingleton<IMakeCache, MakeCache>();

            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }

        /// <summary>
        /// RegisterNotificationCacheService registers cache components for Notification.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterNotificationCacheService(this IServiceCollection services)
        {
            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }

        /// <summary>
        /// RegisterReportCacheService registers cache components for Report.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterReportCacheService(this IServiceCollection services)
        {
            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }

        /// <summary>
        /// RegisterDashboardCacheService registers cache components for Dashboard.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDashboardCacheService(this IServiceCollection services)
        {
            services.AddSingleton<IRoleClaimCache, RoleClaimCache>();
            services.AddSingleton<IEmailTemplateCache, EmailTemplateCache>();
            return services;
        }
    }
}