using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.ServiceConnectorInterface;
using MNBEC.ServiceProviderInterface;

namespace MNBEC.ServiceConnector
{
    /// <summary>
    /// SMSServiceConnector class inherits BaseServiceConector and provide Connector implementation for SMS Service.
    /// </summary>
    public class SMSServiceConnector : BaseServiceConnector, ISMSServiceConnector
    {
        #region Constructor
        /// <summary>
        ///  MakeController initializes class object.
        /// </summary>
        /// <param name="smsServiceProvider"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public SMSServiceConnector(ISMSServiceProvider smsServiceProvider, IConfiguration configuration, ILogger<SMSServiceConnector> logger) : base(configuration, logger)
        {
            this.SMSServiceProvider = smsServiceProvider;
        }
        #endregion

        #region Properties and Data Members
        public ISMSServiceProvider SMSServiceProvider { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Send process SMS and send to service provider.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> Send(SMS message)
        {
            return await this.SMSServiceProvider.Send(message);
        }

        public void Sendsms(SMS message)
        {
            this.SMSServiceProvider.Sendsms(message);
        }
        #endregion
    }
}
