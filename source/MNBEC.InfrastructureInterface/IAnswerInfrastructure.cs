using MNBEC.Domain;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;
using MNBEC.ViewModel;
using MNBEC.ViewModel.ReportResponseVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.InfrastructureInterface
{
    public  interface IAnswerInfrastructure : IBaseInfrastructure<Answer>
    {
        Task<List<LookUpVM>> LevelTypeLookUpGetList();
        Task<List<Answer>> GetListByStakeholderId(StakeholderAnswerRequest request);
        Task<ReportResponseVM> GetReportList(StakeholderAnswerRequest request);
        Task<List<SPResponseVM>> GetParentReportList(int LevelId);

        Task<int> QuestionnaireStatusToSumbit(StakeholderQuestionnaireStatus request);

    }
}
