using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.CacheInterface
{
    /// <summary>
    /// IBaseCache is base interface with all basic crud functions required to Cache.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseCache<T>
    {
        #region Methods
        /// <summary>
        /// Get fetch and returns queried item.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Get(T entity);

        /// <summary>
        /// GetAll fetch and returns queried items.
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Remove delete provided record from cache.
        /// </summary>
        /// <param name="entity"></param>
        Task Remove(T entity);

        /// <summary>
        /// Remove delete provided record from cache.
        /// </summary>
        Task RemoveAll();

        /// <summary>
        /// Set adds/sets new object in cache.
        /// </summary>
        /// <param name="entity"></param>
        Task Set(T entity);

        /// <summary>
        /// Set adds/sets new object in cache.
        /// </summary>
        /// <param name="entity"></param>
        Task SetAll(List<T> entity);
        #endregion
    }
}
