using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.ServiceProviderInterface
{
    /// <summary>
    /// IEmailServiceProvider inhertis IBaseServiceProvider for Email Service Providers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEmailServiceProvider : IBaseServiceProvider<Email>
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