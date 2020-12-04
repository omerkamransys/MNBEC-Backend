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
    /// ConfigurationApplication inherits from BaseApplication and implements IConfigurationApplication. It provides the implementation for Configuration related operations.
    /// </summary>
    public class ConfigurationApplication : BaseApplication, IConfigurationApplication
    {
        #region Constructor
        /// <summary>
        /// ConfigurationApplication initailizes object instance.
        /// </summary>
        /// <param name="configurationInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ConfigurationApplication(IConfigurationInfrastructure configurationInfrastructure, IConfiguration configuration, ILogger<ConfigurationApplication> logger) : base(configuration, logger)
        {
            this.ConfigurationInfrastructure = configurationInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// ConfigurationInfrastructure holds the Infrastructure object.
        /// </summary>
        public IConfigurationInfrastructure ConfigurationInfrastructure { get; }
        #endregion

        #region Interface IConfigurationApplication Implementation
        /// <summary>
        /// Add calls ConfigurationInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<uint> Add(Configuration configuration)
        {
            return await this.ConfigurationInfrastructure.Add(configuration);
        }

        /// <summary>
        /// Activate calls ConfigurationInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Configuration configuration)
        {
            return await this.ConfigurationInfrastructure.Activate(configuration);
        }

        /// <summary>
        /// Get calls ConfigurationInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<Configuration> Get(Configuration configuration)
        {
            return await this.ConfigurationInfrastructure.Get(configuration);
        }

        /// <summary>
        /// GetAll calls ConfigurationInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<AllResponse<Configuration>> GetAll(AllRequest<Configuration> configuration)
        {
            return await this.ConfigurationInfrastructure.GetAll(configuration);
        }

        /// <summary>
        /// GetAll calls ConfigurationInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<List<Configuration>> GetList(Configuration configuration)
        {
            return await this.ConfigurationInfrastructure.GetList(configuration);
        }

        /// <summary>
        /// Update calls ConfigurationInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<bool> Update(Configuration configuration)
        {
            return await this.ConfigurationInfrastructure.Update(configuration);
        }
        #endregion
    }
}