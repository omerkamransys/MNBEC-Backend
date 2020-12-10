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
    /// ClaimApplication inherits from BaseApplication and implements IClaimApplication. It provides the implementation for ApplicationClaim related operations.
    /// </summary>
    public class ClaimApplication : BaseApplication, IClaimApplication
    {
        #region Constructor
        /// <summary>
        /// ClaimApplication initailizes object instance.
        /// </summary>
        /// <param name="claimInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ClaimApplication(IClaimInfrastructure claimInfrastructure, IConfiguration configuration, ILogger<ClaimApplication> logger) : base(configuration, logger)
        {
            this.ClaimInfrastructure = claimInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// ApplicationClaimInfrastructure holds the Infrastructure object.
        /// </summary>
        public IClaimInfrastructure ClaimInfrastructure { get; }
        #endregion

        #region Interface IClaimApplication Implementation
        /// <summary>
        /// Add calls ApplicationClaimInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<int> Add(ApplicationClaim applicationClaim)
        {
            return await this.ClaimInfrastructure.Add(applicationClaim);
        }

        /// <summary>
        /// Activate calls ApplicationClaimInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<bool> Activate(ApplicationClaim applicationClaim)
        {
            return await this.ClaimInfrastructure.Activate(applicationClaim);
        }

        /// <summary>
        /// Get calls ApplicationClaimInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<ApplicationClaim> Get(ApplicationClaim applicationClaim)
        {
            return await this.ClaimInfrastructure.Get(applicationClaim);
        }

        /// <summary>
        /// GetAll calls ApplicationClaimInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationClaim>> GetAll(AllRequest<ApplicationClaim> applicationClaim)
        {
            return await this.ClaimInfrastructure.GetAll(applicationClaim);
        }

        /// <summary>
        /// GetList calls ApplicationClaimInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaim>> GetList(ApplicationClaim applicationClaim)
        {
            return await this.ClaimInfrastructure.GetList(applicationClaim);
        }

        /// <summary>
        /// GetListByRole fetch and returns queried list of items by Role.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaim>> GetListByRole(ApplicationRole applicationRole)
        {
            return await this.ClaimInfrastructure.GetListByRole(applicationRole);
        }

        /// <summary>
        /// GetListByUser fetch and returns queried list of items by User.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaim>> GetListByUser(ApplicationUser applicationUser)
        {
            return await this.ClaimInfrastructure.GetListByUser(applicationUser);
        }

        /// <summary>
        /// Update calls ApplicationClaimInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<bool> Update(ApplicationClaim applicationClaim)
        {
            return await this.ClaimInfrastructure.Update(applicationClaim);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationRole>> GetAllClaimsWithRole() {
            return await this.ClaimInfrastructure.GetAllClaimsWithRole();
        }

        #endregion
    }
}