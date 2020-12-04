using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// ClaimGroupApplication inherits from BaseApplication and implements IClaimGroupApplication. It provides the implementation for ApplicationClaimGroup related operations.
    /// </summary>
    public class ClaimGroupApplication : BaseApplication, IClaimGroupApplication
    {
        #region Constructor
        /// <summary>
        /// ClaimGroupApplication initailizes object instance.
        /// </summary>
        /// <param name="claimGroupInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ClaimGroupApplication(IClaimGroupInfrastructure claimGroupInfrastructure, IConfiguration configuration, ILogger<ClaimGroupApplication> logger) : base(configuration, logger)
        {
            this.ClaimGroupInfrastructure = claimGroupInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// ClaimGroupInfrastructure holds the Infrastructure object.
        /// </summary>
        public IClaimGroupInfrastructure ClaimGroupInfrastructure { get; }
        #endregion

        #region Interface IClaimGroupApplication Implementation
        /// <summary>
        /// Add calls ClaimGroupInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<uint> Add(ApplicationClaimGroup applicationClaimGroup)
        {
            return await this.ClaimGroupInfrastructure.Add(applicationClaimGroup);
        }

        /// <summary>
        /// Activate calls ClaimGroupInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<bool> Activate(ApplicationClaimGroup applicationClaimGroup)
        {
            return await this.ClaimGroupInfrastructure.Activate(applicationClaimGroup);
        }

        /// <summary>
        /// Get calls ClaimGroupInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<ApplicationClaimGroup> Get(ApplicationClaimGroup applicationClaimGroup)
        {
            return await this.ClaimGroupInfrastructure.Get(applicationClaimGroup);
        }

        /// <summary>
        /// GetAll calls ClaimGroupInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationClaimGroup>> GetAll(AllRequest<ApplicationClaimGroup> applicationClaimGroup)
        {
            return await this.ClaimGroupInfrastructure.GetAll(applicationClaimGroup);
        }


        /// <summary>
        /// GetAllByUser calls ClaimGroupInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationClaimGroup>> GetAllByUser(AllRequest<ApplicationClaimGroup> applicationClaimGroup)
        {
            return await this.ClaimGroupInfrastructure.GetAllByUser(applicationClaimGroup);
        }

        /// <summary>
        /// GetAll calls ClaimGroupInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaimGroup>> GetList(ApplicationClaimGroup applicationClaimGroup)
        {
            return await this.ClaimGroupInfrastructure.GetList(applicationClaimGroup);
        }

        /// <summary>
        /// Update calls ClaimGroupInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<bool> Update(ApplicationClaimGroup applicationClaimGroup)
        {
            return await this.ClaimGroupInfrastructure.Update(applicationClaimGroup);
        }
        #endregion
    }
}