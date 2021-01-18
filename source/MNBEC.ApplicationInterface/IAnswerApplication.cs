using MNBEC.Domain;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.ApplicationInterface
{
    public interface IAnswerApplication : IBaseApplication<Answer>
    {
        Task<List<LookUpVM>> LevelTypeLookUpGetList();
        Task<List<Answer>> GetListByStakeholderId(StakeholderAnswerRequest request);
    }
}
