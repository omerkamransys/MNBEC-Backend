using MNBEC.Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// MakeApplication inherits from BaseApplication and implements IMakeApplication. It provides the implementation for Make related operations.
    /// </summary>
    public class UserActivityApplication : BaseApplication, IUserActivityApplication
    {
        #region Constructor
        /// <summary>
        /// UserActivityApplication initailizes object instance.
        /// </summary>
        /// <param name="UserActivityInfrastructure"></param>
        /// <param name="UserActivityCache"></param>
        public UserActivityApplication(IUserActivityInfrastructure UserActivityInfrastructure, IConfiguration configuration, ILogger<UserActivityApplication> logger) : base(configuration, logger)
        {
            this.UserActivityInfrastructure = UserActivityInfrastructure;
           
         
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// UserActivityInfrastructure holds the Infrastructure object.
        /// </summary>
        public IUserActivityInfrastructure UserActivityInfrastructure { get; }
       
      
        #endregion

        #region Interface IUserActivityApplication Implementation
        /// <summary>
        /// Add calls UserActivityInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="UserActivity"></param>
        /// <returns></returns>
        public async Task<int> Add(UserActivity UserActivity)
        {
            var response= await this.UserActivityInfrastructure.Add(UserActivity);
            
            return response;
        }

        /// <summary>
        /// Activate calls UserActivityInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="UserActivity"></param>
        /// <returns></returns>
        public async Task<bool> Activate(UserActivity UserActivity)
        {
            return await this.UserActivityInfrastructure.Activate(UserActivity);
        }

        /// <summary>
        /// Get calls UserActivityInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="UserActivity"></param>
        /// <returns></returns>
        public async Task<UserActivity> Get(UserActivity UserActivity)
        {
            return await this.UserActivityInfrastructure.Get(UserActivity);
        }

        /// <summary>
        /// GetAll calls UserActivityInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="UserActivity"></param>
        /// <returns></returns>
        public async Task<AllResponse<UserActivity>> GetAll(AllRequest<UserActivity> UserActivity)
        {
            return await this.UserActivityInfrastructure.GetAll(UserActivity);

           
        }

        /// <summary>
        /// GetAll calls UserActivityInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="UserActivity"></param>
        /// <returns></returns>
        public async Task<List<UserActivity>> GetList(UserActivity UserActivity)
        {
            return await this.UserActivityInfrastructure.GetList(UserActivity);
        }
        
        

        /// <summary>
        /// Update calls UserActivityInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="UserActivity"></param>
        /// <returns></returns>
        public async Task<bool> Update(UserActivity UserActivity)
        {
            //  return await this.UserActivityInfrastructure.Update(UserActivity);
            var response = await this.UserActivityInfrastructure.Update(UserActivity);
           
            return response;
        }
        
        #endregion
    }
}