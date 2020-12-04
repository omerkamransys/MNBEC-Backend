using Microsoft.Extensions.DependencyInjection;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;

namespace MNBEC.Application.Extensions
{
    /// <summary>
    /// ApplicationExtensions class provides implementation for extension methods.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// RegisterVehicleApplication registers Application components for Vehicle.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterVehicleApplication(this IServiceCollection services)
        {

            services.AddSingleton<IEmailApplication, EmailApplication>();

            services.AddSingleton<ISMSApplication, SMSApplication>();


            services.AddSingleton<IEmailApplication, EmailApplication>();


            return services;
        }

        /// <summary>
        /// RegisterInspectionCentreApplication registers Application components for Inspection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionCentreApplication(this IServiceCollection services)
        {


            return services;
        }

        /// <summary>
        /// RegisterCommonApplication registers Application components for Common services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommonApplication(this IServiceCollection services)
        {
            services.AddSingleton<IClaimApplication, ClaimApplication>();

            services.AddSingleton<IConfigurationApplication, ConfigurationApplication>();
            return services;
        }

        /// <summary>
        /// RegisterAccountApplication registers Application components for Common services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAccountApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserApplication, UserApplication>();
            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IEmployeeApplication, EmployeeApplication>();
            services.AddTransient<IClaimGroupApplication, ClaimGroupApplication>();
            services.AddTransient<IClaimApplication, ClaimApplication>();
            services.AddSingleton<IEmailApplication, EmailApplication>();
            services.AddSingleton<IUserActivityApplication, UserActivityApplication>();           

            return services;
        }

        public static IServiceCollection RegisterExternalApplication(this IServiceCollection services)
        {
            
            services.AddSingleton<IDocumentApplication, DocumentApplication>();


            return services;
        }

        public static IServiceCollection RegisterNotificationApplication(this IServiceCollection services)
        {
           
          //  services.AddSingleton<INotificationCommonApplication, NotificationCommonApplication>();

            return services;
        }

      
        public static IServiceCollection RegisterConfigurationApplication(this IServiceCollection services)
        {
           
            services.AddSingleton<IChannelApplication, ChannelApplication>();
            services.AddSingleton<IUserActivityApplication, UserActivityApplication>();


            return services;
        }

        /// <summary>
        /// RegisterEmailApplication registers Application components for Customer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterEmailApplication(this IServiceCollection services)
        {
            services.AddSingleton<IClaimApplication, ClaimApplication>();
            services.AddSingleton<IEmailApplication, EmailApplication>();


            return services;
        }

        public static IServiceCollection RegisterSMSApplication(this IServiceCollection services)
        {
            
            services.AddSingleton<ISMSApplication, SMSApplication>();

            return services;
        }

        /// <summary>
        /// RegisterDealerApplication registers Application components for Dealer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDealerApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserApplication, UserApplication>();
            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IEmailApplication, EmailApplication>();
            services.AddIdentity<ApplicationUser, ApplicationRole>();
            services.AddTransient<IClaimGroupApplication, ClaimGroupApplication>();
            services.AddTransient<IClaimApplication, ClaimApplication>();

            return services;
        }
       
    }
}