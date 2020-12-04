using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.ServiceConnectorInterface
{
    /// <summary>
    /// ISMSServiceConnector inherits IBaseServiceConnector and provides interface for SMS Services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISMSServiceConnector : IBaseServiceConnector<SMS>
    {
        #region Methods
        /// <summary>
        /// Send process SMS and send to service provider.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> Send(SMS message);
        void Sendsms(SMS message);
        #endregion
    }
}