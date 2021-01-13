using MNBEC.Domain;
using MNBEC.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IQuestionInfrastructure inherites IBaseInfrastructure and provides the interface for Question operations in Databasse.
    /// </summary>
    public interface IQuestionInfrastructure : IBaseInfrastructure<Question>
    {
        Task<bool> UpdateOrder(List<Question> questions);
        Task<List<LookUpVM>> AreaLookUpGetList();
        Task<List<LookUpVM>> FourPLookUpGetList();
        Task<List<LookUpVM>> ResponsibleLookUpGetList();
        Task<List<LookUpVM>> LevelLookUpGetList();
        Task<bool> AddBulk(List<Question> questions);
    }
}
