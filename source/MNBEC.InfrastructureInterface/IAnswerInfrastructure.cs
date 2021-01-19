using MNBEC.Domain;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;
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
        Task<int> QuestionnaireStatusToSumbit(StakeholderQuestionnaireStatus request);

    }
}
