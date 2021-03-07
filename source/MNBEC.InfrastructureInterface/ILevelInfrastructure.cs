using MNBEC.Domain;
using MNBEC.ViewModel.Level;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// ILevelInfrastructure inherites IBaseInfrastructure and provides the interface for Level operations in Databasse.
    /// </summary>
    public interface ILevelInfrastructure : IBaseInfrastructure<Level>
    {
        Task<List<StakeholderLevelModel>> GetListByStakeholderId(int stakeholderId);
        Task<PlanReportComment> GetPlanReportComment(PlanReportComment level);
        Task<int> AddPlanReportComment(PlanReportComment level);
        Task<bool> UpdatePlanReportComment(PlanReportComment entity);
    }
}
