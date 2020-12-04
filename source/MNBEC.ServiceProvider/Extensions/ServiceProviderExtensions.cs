using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using MNBEC.Core;
using MNBEC.Core.Extensions;
using MNBEC.Core.Interface;
using MNBEC.ServiceProviderInterface;

namespace MNBEC.ServiceProvider
{
    /// <summary>
    /// ServiceProviderExtensions class provides implementation for extension methods.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// RegisterCommonServiceProvider registers Common Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommonServiceProvider(this IServiceCollection services)
        {
            services.AddSingleton<ISMSServiceProvider, SMSServiceProvider>();
            services.AddSingleton<IEmailServiceProvider, EmailServiceProvider>();
            services.AddSingleton<IExternalStorageProvider, ExternalStorageProvider>();
            services.AddSingleton<IEnzoHttpClientFactory, EnzoHttpClientFactory>();
            services.AddHttpClient("EnzoClientFactory");
            return services;
        }
        /// <summary>
        /// RegisterVehicleServiceProvider registers Vehicle Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterVehicleServiceProvider(this IServiceCollection services)
        {

            return services;
        }

        /// <summary>
        /// RegisterInspectionCentreServiceProvider registers Inspection Centre Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionCentreServiceProvider(this IServiceCollection services)
        {
            //TODO: Add providers

            return services;
        }


        /// <summary>
        /// RegisterCustomerServiceProvider registers Customer Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCustomerServiceProvider(this IServiceCollection services, IConfiguration configuration)
        {            
            
            return services;
        }

        /// <summary>
        /// RegisterInspectionServiceProvider registers Inspection Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionServiceProvider(this IServiceCollection services)
        {
            //TODO: Add providers

            return services;
        }

        /// <summary>
        /// RegisterDocumentServiceProvider registers Document Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDocumentServiceProvider(this IServiceCollection services)
        {
            services.AddSingleton<IExternalStorageProvider, ExternalStorageProvider>();

            return services;
        }

        /// <summary>
        /// RegisterDealerServiceProvider registers Dealer Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDealerServiceProvider(this IServiceCollection services)
        {
            //TODO: Add providers

            return services;
        }
        
        /// <summary>
        /// RegisterNotificationServiceProvider registers Notification Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterNotificationServiceProvider(this IServiceCollection services)
        {
            //TODO: Add providers
 
            return services;
        }
        
        /// <summary>
        /// RegisterReportServiceProvider registers Report Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterReportServiceProvider(this IServiceCollection services)
        {
            //TODO: Add providers

            return services;
        }
        
        /// <summary>
        /// RegisterDashboardServiceProvider registers Dashboard Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDashboardServiceProvider(this IServiceCollection services)
        {
            //TODO: Add providers

            return services;
        }
    }
}