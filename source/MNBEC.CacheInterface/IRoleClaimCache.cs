using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.CacheInterface
{
    /// <summary>
    /// INotificationTemplateCache inherits IBaseCache interface to provide interface for Notification Cache.
    /// </summary>
    public interface IRoleClaimCache : IBaseCache<ApplicationClaim>
    {
        Task SetBulk(string key, Dictionary<string, HashSet<string>> data);
        Task<Dictionary<string, HashSet<string>>> GetBulk(string key);
    }
}
