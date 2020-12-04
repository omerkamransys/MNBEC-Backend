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
    public class ChannelApplication : BaseApplication, IChannelApplication
    {
        #region Constructor
        /// <summary>
        /// ChannelApplication initailizes object instance.
        /// </summary>
        /// <param name="ChannelInfrastructure"></param>
        /// <param name="ChannelCache"></param>
        public ChannelApplication(IChannelInfrastructure ChannelInfrastructure, IConfiguration configuration, ILogger<ChannelApplication> logger) : base(configuration, logger)
        {
            this.ChannelInfrastructure = ChannelInfrastructure;
           
         
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// ChannelInfrastructure holds the Infrastructure object.
        /// </summary>
        public IChannelInfrastructure ChannelInfrastructure { get; }
       
      
        #endregion

        #region Interface IChannelApplication Implementation
        /// <summary>
        /// Add calls ChannelInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<uint> Add(Channel Channel)
        {
            //return await this.ChannelInfrastructure.Add(Channel);
            var response= await this.ChannelInfrastructure.Add(Channel);
            
            return response;
        }

        /// <summary>
        /// Activate calls ChannelInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Channel Channel)
        {
            return await this.ChannelInfrastructure.Activate(Channel);
        }

        /// <summary>
        /// Get calls ChannelInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<Channel> Get(Channel Channel)
        {
            return await this.ChannelInfrastructure.Get(Channel);
        }

        /// <summary>
        /// GetAll calls ChannelInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<AllResponse<Channel>> GetAll(AllRequest<Channel> Channel)
        {
            return await this.ChannelInfrastructure.GetAll(Channel);

           
        }

        /// <summary>
        /// GetAll calls ChannelInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<List<Channel>> GetList(Channel Channel)
        {
            return await this.ChannelInfrastructure.GetList(Channel);
        }
        
        

        /// <summary>
        /// Update calls ChannelInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<bool> Update(Channel Channel)
        {
            //  return await this.ChannelInfrastructure.Update(Channel);
            var response = await this.ChannelInfrastructure.Update(Channel);
           
            return response;
        }
        
        #endregion
    }
}