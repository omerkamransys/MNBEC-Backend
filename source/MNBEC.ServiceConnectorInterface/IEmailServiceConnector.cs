using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.ServiceConnectorInterface
{
    /// <summary>
    /// IEmailServiceConnector inherits IBaseServiceConnector and provides interface for Email Services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEmailServiceConnector : IBaseServiceConnector<Email>
    {
        #region Methods
        /// <summary>
        /// Send process Email and send to service provider.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> Send(Email message);
        void Sendemail(Email message);
        #endregion
    }
}