using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MNBEC.Cache;
using MNBEC.CacheInterface;
using MNBEC.Domain;
using MNBEC.InfrastructureInterface;
using Vitol.Enzo.Infrastructure;

namespace MNBEC.Infrastructure.Extensions
{
    /// <summary>
    ///  InfrastructureExtensions class provides implementation for extension methods.
    /// </summary>
    public static class InfrastructureExtensions
    {
        /// <summary>
        /// RegisterVehicleInfrastructure registers Infrastructure components for Vehicle.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterVehicleInfrastructure(this IServiceCollection services)
        {
           
            services.AddSingleton<ISMSInfrastructure, SMSInfrastructure>();
            services.AddSingleton<IEmailInfrastructure, EmailInfrastructure>();
            return services;
        }

        /// <summary>
        /// RegisterInspectionCentreInfrastructure registers Infrastructure components for InspectionCentre.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionCentreInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();

            services.AddSingleton<IEmailInfrastructure, EmailInfrastructure>();


            services.AddSingleton<ISMSInfrastructure, SMSInfrastructure>();

            return services;
        }

        public static IServiceCollection RegisterSMSInfrastructure(this IServiceCollection services)
        {           
            services.AddSingleton<ISMSInfrastructure, SMSInfrastructure>();

            return services;
        }

        /// <summary>
        /// RegisterAccountInfrastructure registers Infrastructure components for Account.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAccountInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserInfrastructure>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleInfrastructure>();
            services.AddTransient<IUserInfrastructure, UserInfrastructure>();
            services.AddTransient<IRoleInfrastructure, RoleInfrastructure>();
            services.AddTransient<IEmployeeInfrastructure, EmployeeInfrastructure>();
            services.AddTransient<IClaimGroupInfrastructure, ClaimGroupInfrastructure>();
            services.AddTransient<IClaimInfrastructure, ClaimInfrastructure>();
            services.AddSingleton<IEmailInfrastructure, EmailInfrastructure>();
            services.AddSingleton<IUserActivityInfrastructure, UserActivityInfrastructure>();

            

            return services;
        }

        public static IServiceCollection RegisterExternalInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentInfrastructure, DocumentInfrastructure>();

            

            return services;
        }
       
        public static IServiceCollection RegisterConfigurationInfrastructure(this IServiceCollection services)
        {           
            
            services.AddSingleton<IChannelInfrastructure, ChannelInfrastructure>();
            

            
            services.AddSingleton<IUserActivityInfrastructure, UserActivityInfrastructure>();
            
            return services;
        }
        

        /// <summary>
        /// RegisterInspectionCentreInfrastructure registers Infrastructure components for InspectionCentre.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommonInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();

            services.AddSingleton<IConfigurationInfrastructure, ConfigurationInfrastructure>();
            return services;
        }

        /// <summary>
        /// RegisterCustomerInfrastructure registers Infrastructure components for Customer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCustomerInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();
            services.AddSingleton<IEmailInfrastructure, EmailInfrastructure>();
            

            return services;
        }

        /// <summary>
        /// RegisterEmailInfrastructure registers Infrastructure components for Customer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterEmailInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IEmailInfrastructure, EmailInfrastructure>();
            return services;
        }

        /// <summary>
        /// RegisterInspectionInfrastructure registers Infrastructure components for Inspection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInspectionInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserInfrastructure>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleInfrastructure>();
            services.AddTransient<IRoleInfrastructure, RoleInfrastructure>();

            services.AddTransient<IClaimGroupInfrastructure, ClaimGroupInfrastructure>();
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();
            
            services.AddSingleton<IEmailInfrastructure, EmailInfrastructure>();
           
            services.AddSingleton<IUserInfrastructure, UserInfrastructure>();


            return services;
        }

        /// <summary>
        /// RegisterDocumentInfrastructure registers Infrastructure components for Document.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDocumentInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();

            return services;
        }

        /// <summary>
        /// RegisterDealerInfrastructure registers Infrastructure components for Dealer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDealerInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserInfrastructure, UserInfrastructure>();


            services.AddTransient<IUserStore<ApplicationUser>, UserInfrastructure>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleInfrastructure>();
            services.AddTransient<IUserInfrastructure, UserInfrastructure>();
            services.AddTransient<IRoleInfrastructure, RoleInfrastructure>();
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();

            


            services.AddTransient<IEmailInfrastructure, EmailInfrastructure>();



            return services;
        }

        /// <summary>
        /// RegisterNotificationInfrastructure registers Infrastructure components for Notification.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterNotificationInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserInfrastructure>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleInfrastructure>();
            services.AddTransient<IUserInfrastructure, UserInfrastructure>();
            services.AddTransient<IRoleInfrastructure, RoleInfrastructure>();

            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();
          
            services.AddTransient<IEmailInfrastructure, EmailInfrastructure>();

            return services;
        }

        /// <summary>
        /// RegisterReportInfrastructure registers Infrastructure components for Report.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterReportInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();


            return services;
        }

        /// <summary>
        /// RegisterDashboardInfrastructure registers Infrastructure components for Dashboard.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDashboardInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClaimInfrastructure, ClaimInfrastructure>();


            return services;
        }

        public static IServiceCollection RegisterQuestionnaireTemplateInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IQuestionnaireTemplateInfrastructure, QuestionnaireTemplateInfrastructure>();


            return services;
        }
        public static IServiceCollection RegisterQuestionInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IQuestionInfrastructure, QuestionInfrastructure>();


            return services;
        }

        public static IServiceCollection RegisterLevelInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ILevelInfrastructure, LevelInfrastructure>();


            return services;
        }
    }
}