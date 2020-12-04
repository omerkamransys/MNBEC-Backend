using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.ServiceConnectorInterface;
using MNBEC.ServiceProviderInterface;

namespace MNBEC.ServiceConnector
{
    /// <summary>
    /// EmailServiceConnector class inherits BaseServiceConector and provide Connector implementation for Email Service.
    /// </summary>
    public class EmailServiceConnector : BaseServiceConnector, IEmailServiceConnector
    {
        #region Constructor
        /// <summary>
        ///  MakeController initializes class object.
        /// </summary>
        /// <param name="emailServiceProvider"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public EmailServiceConnector(IEmailServiceProvider emailServiceProvider, IConfiguration configuration, ILogger<EmailServiceConnector> logger) : base(configuration, logger)
        {
            this.EmailServiceProvider = emailServiceProvider;
        }
        #endregion

        #region Properties and Data Members
        public IEmailServiceProvider EmailServiceProvider { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Send process Email and send to service provider.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> Send(Email message)
        {
            return await this.EmailServiceProvider.Send(message);
        }
        public void Sendemail(Email message)
        {
           this.EmailServiceProvider.Sendemail(message);
        }
        #endregion
    }
}
