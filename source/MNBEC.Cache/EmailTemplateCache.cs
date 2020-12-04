using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.CacheInterface;
using MNBEC.Domain;

namespace MNBEC.Cache
{
    /// <summary>
    /// EmailTemplateCache inherits from BaseCache and implements IEmailTemplateCache. It provides the implementation for EmailTemplate related operations.
    /// </summary>
    public class EmailTemplateCache : BaseCache, IEmailTemplateCache
    {
        #region Constructor
        /// <summary>
        /// EmailTemplateCache initailizes object instance.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="configuration"></param>
        public EmailTemplateCache(IDistributedCache cache, IConfiguration configuration) : base(cache, configuration, EmailTemplateCache.EmailTemplateKey)
        {
        }
        #endregion

        #region Properties and Data Members

        private const string EmailTemplateKey = "EmailTemplate";
        #endregion

        #region Interface IEmailTemplateCache Implementation
        /// <summary>
        /// Get gets emailTemplate object from cache and returns queried.
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        public async Task<EmailTemplate> Get(EmailTemplate emailTemplate)
        {
            return await base.Get<EmailTemplate>(emailTemplate.EmailTemplateCode);
        }

        /// <summary>
        /// Get gets emailTemplate object from cache and returns queried.
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmailTemplate>> GetAll()
        {
            return await base.GetAll<EmailTemplate>();
        }

        /// <summary>
        /// Remove deletes emailTemplate object from cache.
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        public async Task Remove(EmailTemplate emailTemplate)
        {
            await base.Remove(emailTemplate.EmailTemplateCode);
        }

        /// <summary>
        /// Set adds new emailTemplate object in cache with key.
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        public async Task Set(EmailTemplate emailTemplate)
        {
            await base.Set(emailTemplate.EmailTemplateCode, emailTemplate);
        }

        /// <summary>
        /// SetAll adds new emailTemplate List in cache with key.
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        public async Task SetAll(List<EmailTemplate> emailTemplate)
        {
            await base.SetAll<EmailTemplate>(emailTemplate);
        }
        #endregion
    }
}
