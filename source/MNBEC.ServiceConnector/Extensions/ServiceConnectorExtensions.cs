using Microsoft.Extensions.DependencyInjection;
using MNBEC.ServiceConnectorInterface;

namespace MNBEC.ServiceConnector.Extensions
{
    /// <summary>
    /// ServiceConnectorExtensions class provides implementation for extension methods.
    /// </summary>
    public static class ServiceConnectorExtensions
    {
        /// <summary>
        /// RegisterCommonServiceConnector registers Common Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommonServiceConnector(this IServiceCollection services)
        {
            services.AddSingleton<ISMSServiceConnector, SMSServiceConnector>();
            services.AddSingleton<IEmailServiceConnector, EmailServiceConnector>();
            services.AddSingleton<IExternalStorageConnector, ExternalStorageConnector>();

            return services;
        }

        /// <summary>
        /// RegisterVehicleServiceConnector registers Vehicle Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterVehicleServiceConnector(this IServiceCollection services)
        {

            return services;
        }

        /// <summary>
        /// RegisterInspectionCentreServiceConnector registers Inspection Centre Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionCentreServiceConnector(this IServiceCollection services)
        {
            //TODO: add Services Here
            return services;
        }

        /// <summary>
        /// RegisterCustomerServiceConnector registers Customer Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCustomerServiceConnector(this IServiceCollection services)
        {

            return services;
        }
        /// <summary>
        /// RegisterCustomerServiceConnector registers Customer Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDealershipServiceConnector(this IServiceCollection services)
        {

            return services;
        }
        /// <summary>
        /// RegisterApppointmentServiceConnector registers Customer Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAppointmentServiceConnector(this IServiceCollection services)
        {

            return services;
        }

        /// <summary>
        /// RegisterInspectionServiceConnector registers Inspection Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionServiceConnector(this IServiceCollection services)
        {
            services.AddSingleton<IEmailServiceConnector, EmailServiceConnector>();
            return services;
        }

        /// <summary>
        /// RegisterDocumentServiceConnector registers Document Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDocumentServiceConnector(this IServiceCollection services)
        {
            services.AddSingleton<IExternalStorageConnector, ExternalStorageConnector>();

            return services;
        }

        /// <summary>
        /// RegisterDealerServiceConnector registers Dealer Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDealerServiceConnector(this IServiceCollection services)
        {
            //TODO: add Services Here
            return services;
        }

        /// <summary>
        /// RegisterNotificationServiceConnector registers Notification Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterNotificationServiceConnector(this IServiceCollection services)
        {
            //TODO: add Services Here

            return services;
        }

        /// <summary>
        /// RegisterReportServiceConnector registers Report Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterReportServiceConnector(this IServiceCollection services)
        {
            //TODO: add Services Here

            return services;
        }

        /// <summary>
        /// RegisterDashboardServiceConnector registers Dashboard Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDashboardServiceConnector(this IServiceCollection services)
        {
            //TODO: add Services Here

            return services;
        }
    }
}