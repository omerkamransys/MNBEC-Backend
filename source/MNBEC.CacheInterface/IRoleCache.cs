using MNBEC.Domain;

namespace MNBEC.CacheInterface
{
    /// <summary>
    /// IRoleCache inherits IBaseCache interface to provide interface for ApplicationRole Cache.
    /// </summary>
    public interface IRoleCache : IBaseCache<ApplicationRole>
    {
    }
}
