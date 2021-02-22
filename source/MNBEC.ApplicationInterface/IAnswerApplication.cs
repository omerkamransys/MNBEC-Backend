using MNBEC.Domain;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;
using MNBEC.ViewModel.ReportResponseVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.ApplicationInterface
{
    public interface IAnswerApplication : IBaseApplication<Answer>
    {
        Task<List<LookUpVM>> LevelTypeLookUpGetList();
        Task<List<Answer>> GetListByStakeholderId(StakeholderAnswerRequest request);
        Task<ReportResponseVM> GetReportList(StakeholderAnswerRequest request);

        Task<int> QuestionnaireStatusToSumbit(StakeholderQuestionnaireStatus request);
    }
}
