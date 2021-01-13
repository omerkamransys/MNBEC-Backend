using MNBEC.Domain;
using MNBEC.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.ApplicationInterface
{
    /// <summary>
    /// IQuestionApplication inherits IBaseApplication interface to provide interface for Question related Application.
    /// </summary>
    public interface IQuestionApplication : IBaseApplication<Question>
    {
        Task<bool> UpdateOrder(List<Question> questions);

        Task<List<LookUpVM>> AreaLookUpGetList();
        Task<List<LookUpVM>> FourPLookUpGetList();
        Task<List<LookUpVM>> ResponsibleLookUpGetList();
        Task<List<LookUpVM>> LevelLookUpGetList();
        Task<bool> AddBulk(List<Question> questions);
    }
}
