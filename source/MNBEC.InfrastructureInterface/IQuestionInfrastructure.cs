using MNBEC.Domain;
using MNBEC.ViewModel;
using MNBEC.ViewModel.LookUp;
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

        Task<int> AreaLookUpAdd(LookUpRequestVM request);
        Task<bool> AreaLookUpUpdate(LookUpRequestVM request);
        Task<bool> AreaLookUpActivate(LookUpRequestVM request);
        Task<LookUpRequestVM> AreaLookUpGet(LookUpVM request);
    }
}
