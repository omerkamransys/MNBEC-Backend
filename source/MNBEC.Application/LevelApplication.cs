using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;
using MNBEC.ViewModel.Level;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.Application
{
    /// <summary>
    /// LevelApplication inherits from BaseApplication and implements ILevelApplication. It provides the implementation for Level related operations.
    /// </summary>
    public class LevelApplication : BaseApplication, ILevelApplication
    {
        #region Constructor
        /// <summary>
        /// LevelApplication initailizes object instance.
        /// </summary>
        /// <param name="levelInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public LevelApplication(ILevelInfrastructure levelInfrastructure, IConfiguration configuration, ILogger<LevelApplication> logger) : base(configuration, logger)
        {
            this.LevelInfrastructure = levelInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// LevelInfrastructure holds the Infrastructure object.
        /// </summary>
        public ILevelInfrastructure LevelInfrastructure { get; }
        #endregion

        #region Interface ILevelApplication Implementation
        /// <summary>
        /// Add calls LevelInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<int> Add(Level level)
        {
            return await this.LevelInfrastructure.Add(level);
        }

        /// <summary>
        /// Activate calls LevelInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Level level)
        {
            return await this.LevelInfrastructure.Activate(level);
        }

        /// <summary>
        /// Get calls LevelInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<Level> Get(Level level)
        {
            return await this.LevelInfrastructure.Get(level);
        }

        /// <summary>
        /// GetAll calls LevelInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<AllResponse<Level>> GetAll(AllRequest<Level> level)
        {
            return await this.LevelInfrastructure.GetAll(level);
        }

        /// <summary>
        /// GetList calls LevelInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<Level>> GetList(Level level)
        {
            return await this.LevelInfrastructure.GetList(level);
        }

        /// <summary>
        /// Update calls LevelInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<bool> Update(Level level)
        {
            return await this.LevelInfrastructure.Update(level);
        }

        public async Task<List<StakeholderLevelModel>> GetListByStakeholderId(int stakeholderId)
        {
            return await this.LevelInfrastructure.GetListByStakeholderId(stakeholderId);
        }

        public async Task<PlanReportComment> GetPlanReportComment(PlanReportComment level)
        {
            return await this.LevelInfrastructure.GetPlanReportComment(level);
        }

        public async Task<int> AddPlanReportComment(PlanReportComment entity)
        {
            return await this.LevelInfrastructure.AddPlanReportComment(entity);
        }
        public async Task<bool> UpdatePlanReportComment(PlanReportComment entity)
        {
            return await this.LevelInfrastructure.UpdatePlanReportComment(entity);
        }
       


        #endregion
    }
}