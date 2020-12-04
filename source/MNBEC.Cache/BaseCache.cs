using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.Cache
{
    /// <summary>
    /// BaseCache is base class for all Cache classes.
    /// </summary>
    public class BaseCache
    {
        #region Constructor
        /// <summary>
        /// BaseCache initailizes object instance.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="configuration"></param>
        /// <param name="baseKey"></param>
        public BaseCache(IDistributedCache cache, IConfiguration configuration, string baseKey)
        {
            this.Cache = cache;
            this.Configuration = configuration;
            this.Key = baseKey;
        }
        #endregion

        #region Properties and Data Members
        public IDistributedCache Cache { get; }
        public IConfiguration Configuration { get; }
        public string Key { get; }
        protected const string AllCard = "All";
        #endregion

        #region Methods
        /// <summary>
        /// Get gets provided object from cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<T> Get<T>(object id)
        {
            string key = this.GetKeyItem(id);
            var data = await this.Cache.GetStringAsync(key);

            return this.Deservialize<T>(data);
        }

        /// <summary>
        /// GetAll returns list of provided objects from cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async Task<List<T>> GetAll<T>()
        {
            string key = this.GetKeyItem(BaseCache.AllCard);
            string data = await this.Cache.GetStringAsync(key);
            var list = this.Deservialize<List<T>>(data);

            return list;
        }
        ///// <summary>
        ///// GetAll returns list of provided objects from cache.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //protected async Task<AllResponse<T>> GetAll<T>(AllRequest<T> request)
        //{
        //    AllResponse<T> response = new AllResponse<T>
        //    {
        //        Offset = request.Offset,
        //        PageSize = request.PageSize,
        //        TotalRecord = 0,
        //        SortColumn = request.SortColumn,
        //        SortAscending = request.SortAscending
        //    };
        //    string key = this.GetKeyItem(BaseCache.AllCard);
        //    var data = await this.Cache.GetStringAsync(key);
        //    var list = this.Deservialize<List<T>>(data);

        //    if (list != null)
        //    {
        //        response.TotalRecord = Convert.ToUInt32(list.Count);
        //        response.Data = list.Sort(request.SortColumn, request.SortAscending);
        //    }

        //    return response;
        //}

        /// <summary>
        /// Remove removes provided object from cache.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task Remove(object id)
        {
            string key = this.GetKeyItem(id);

            await this.Cache.RemoveAsync(key);
        }

        /// <summary>
        /// Remove removes all items from cache for given object.
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAll()
        {
            string key = this.GetKeyItem(BaseCache.AllCard);

            await this.Cache.RemoveAsync(key);
        }

        /// <summary>
        /// Set sets provided object in cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected async Task Set<T>(object id, T data)
        {
            string key = this.GetKeyItem(id);

            await this.Cache.SetStringAsync(key, this.Serialize(data));
        }

        /// <summary>
        /// Set sets provided object in cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected async Task SetAll<T>(List<T> data)
        {
            string key = this.GetKeyItem(BaseCache.AllCard);

            await this.Cache.SetStringAsync(key, this.Serialize(data));
        }

        /// <summary>
        /// Serialize serializes provided object in Json format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Deserialize deserializes provided data in Json format to T type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private T Deservialize<T>(string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// GetKeyItem process and return final key to use.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetKeyItem(object id)
        {
            return $"{this.Key}-{id}";
        }
        #endregion
    }
}
