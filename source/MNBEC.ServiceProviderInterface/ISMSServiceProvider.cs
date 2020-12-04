using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.ServiceProviderInterface
{
    /// <summary>
    /// ISMSServiceProvider inhertis IBaseServiceProvider for SMS Service Providers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISMSServiceProvider : IBaseServiceProvider<SMS>
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