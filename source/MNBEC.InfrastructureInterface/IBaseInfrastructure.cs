﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain.Common;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IBaseInfrastructure is base interface with all basic crud functions required to Infrastructure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseInfrastructure<T>
    {
        #region Methods
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Add(T entity);
        /// <summary>
        /// Update calls ExternalAuctionInfrastructure to update the external Acution
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update(T entity);

        
        /// <summary>
        /// Activate activate/deactive provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Activate(T entity);

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Get(T entity);

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<AllResponse<T>> GetAll(AllRequest<T> entity);

        /// <summary>
        /// GetAll fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<List<T>> GetList(T entity);

        #endregion
    }
}