using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.CacheInterface;
using MNBEC.Domain;

namespace MNBEC.Cache
{
    /// <summary>
    /// RoleCache inherits from BaseCache and implements IRoleCache. It provides the implementation for ApplicationRole related operations.
    /// </summary>
    public class RoleCache : BaseCache, IRoleCache
    {
        #region Constructor
        /// <summary>
        /// MakeCache initailizes object instance.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="configuration"></param>
        public RoleCache(IDistributedCache cache, IConfiguration configuration) : base(cache, configuration,
            RoleCache.RoleKey)
        {
        }
        #endregion

        #region Properties and Data Members
        private const string RoleKey = "Role";
        #endregion

        #region Interface IRoleCache Implementation
        /// <summary>
        /// Get gets applicationRole object from cache and returns queried.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<ApplicationRole> Get(ApplicationRole applicationRole)
        {
            return await base.Get<ApplicationRole>(applicationRole.RoleId);
        }

        /// <summary>
        /// Get gets applicationRole object from cache and returns queried.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationRole>> GetAll()
        {
            return await base.GetAll<ApplicationRole>();
        }

        /// <summary>
        /// Remove deletes applicationRole object from cache.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task Remove(ApplicationRole applicationRole)
        {
            await base.Remove(applicationRole.RoleId);
        }


        /// <summary>
        /// Set adds new ApplicationRole object in cache with key.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task Set(ApplicationRole role)
        {
            await base.Set<ApplicationRole>(role.RoleId, role);
        }

        /// <summary>
        /// SetAll adds new ApplicationRole List in cache with key.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task SetAll(List<ApplicationRole> roles)
        {
            await base.SetAll<ApplicationRole>(roles);
        }
        #endregion
    }
}
