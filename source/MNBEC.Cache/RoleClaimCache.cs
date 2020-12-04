using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.CacheInterface;
using MNBEC.Domain;

namespace MNBEC.Cache
{
    /// <summary>
    /// RoleClaimTemplateCache inherits from BaseCache and implements IRoleClaimTemplateCache. It provides the implementation for RoleClaim template related operations.
    /// </summary>
    public class RoleClaimCache : BaseCache, IRoleClaimCache
    {
        #region Constructor
        /// <summary>
        /// RoleClaimTemplateCache initailizes object instance.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="configuration"></param>
        public RoleClaimCache(IDistributedCache cache, IConfiguration configuration) : base(cache, configuration, RoleClaimCache.RoleClaimKey)
        {
        }
        #endregion

        #region Properties and Data Members

        private const string RoleClaimKey = "RoleClaim";

        #endregion

        #region Interface IRoleClaimCache Implementation
        /// <summary>
        /// Get gets ApplicationClaim object from cache and returns queried.
        /// </summary>
        /// <param name="ApplicationClaim"></param>
        /// <returns></returns>
        public async Task<ApplicationClaim> Get(ApplicationClaim applicationClaim)
        {
            return await base.Get<ApplicationClaim>(applicationClaim.ClaimCode);
        }

        /// <summary>
        /// Get gets ApplicationClaim object from cache and returns queried.
        /// </summary>
        /// <param name="ApplicationClaim"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, HashSet<string>>> GetBulk(string key)
        {
            return await base.Get<Dictionary<string, HashSet<string>>>(key);
        }

        /// <summary>
        /// Get gets ApplicationClaim object from cache and returns queried.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationClaim>> GetAll()
        {
            return await base.GetAll<ApplicationClaim>();
        }

        /// <summary>
        /// Remove deletes ApplicationClaim object from cache.
        /// </summary>
        /// <param name="ApplicationClaim"></param>
        /// <returns></returns>
        public async Task Remove(ApplicationClaim applicationClaim)
        {
            await base.Remove(applicationClaim.ClaimCode);
        }

        /// <summary>
        /// Set adds new ApplicationClaim object in cache with key.
        /// </summary>
        /// <param name="ApplicationClaim"></param>
        /// <returns></returns>
        public async Task Set(ApplicationClaim applicationClaim)
        {
            await base.Set(applicationClaim.ClaimCode, applicationClaim);
        }

        /// <summary>
        /// SetBulk adds new dictionary to the cache with a dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SetBulk(string key, Dictionary<string, HashSet<string>> data)
        {
            await base.Set<Dictionary<string, HashSet<string>>>(key, data);
        }

        /// <summary>
        /// SetAll adds new Notification List in cache with key.
        /// </summary>
        /// <param name="Notification"></param>
        /// <returns></returns>
        public async Task SetAll(List<ApplicationClaim> applicationClaims)
        {
            await base.SetAll<ApplicationClaim>(applicationClaims);
        }

        #endregion
    }
}
