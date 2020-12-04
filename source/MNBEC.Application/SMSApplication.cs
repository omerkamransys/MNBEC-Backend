using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.InfrastructureInterface;
using MNBEC.ServiceConnectorInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// EmailApplication inherits from BaseApplication. It provides the implementation for Email related operations.
    /// </summary>
    public class SMSApplication : BaseApplication, ISMSApplication
    {
        #region Constructor
        /// <summary>
        /// EmailApplication initailizes object instance.
        /// </summary>
        /// <param name="emailInfrastructure"></param>
        /// <param name="emailServiceConnector"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public SMSApplication(ISMSInfrastructure smsInfrastructure,  ISMSServiceConnector smsServiceConnector, IConfiguration configuration, ILogger<EmailApplication> logger) : base(configuration, logger)
        {
            this.SMSInfrastructure = smsInfrastructure;
            this.SMSServiceConnector = smsServiceConnector;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// EmailInfrastructure holds the Infrastructure object.
        /// </summary>
        /// 

        public ISMSInfrastructure SMSInfrastructure { get; }
        private ISMSServiceConnector SMSServiceConnector { get; }


        private const string AppointmentCreateSMSCode = "AppCre";
        private const string AppointmentCreateSMSCodeNE = "NEAppCre";
        private const string FleetSaleEnquiryCreateSMSCode = "FleetSL";
        private const string AppointmentReminderSMSCode = "AppRmndr";
        private const string AppointmentReminderNESMSCode = "NEAppRmd";
        private const string Message = @"Please find below the dealership\seller details:";

        private const string AppointmentTimeKeyTag = "{{Time}}";
        private const string AppointmentDateKeyTag = "{{Date}}";
        private const string CentreNameTag = "{{CentreName}}";
        private const string LocationURLTag = "{{LocationURL}}";
        private const string CenterLocationUrlTag = "{{CenterLocationUrl}}";
        private const string CentreAddressTag = "{{CenterAddress}}";
        private const string ValuationMinPriceTag = "{{ValuationMinPrice}}";


        #endregion

        #region Interface ISMSApplication Implementation
        

        private void SendSMS(SMS sms)
        {
            this.SMSServiceConnector.Sendsms(sms);
        }

        #region Factory Methods
        private string buildMapUrl(string longitude, string latitude)
        {
            return $@"{base.Configuration["Urls:MapUrl"]}?q={longitude},{latitude}";
        }

        #endregion

        #endregion
    }
}
