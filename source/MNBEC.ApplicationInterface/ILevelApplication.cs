using MNBEC.Domain;
using MNBEC.ViewModel.Level;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.ApplicationInterface
{
    /// <summary>
    /// ILevelApplication inherits IBaseApplication interface to provide interface for Level related Application.
    /// </summary>
    public interface ILevelApplication : IBaseApplication<Level>
    {
        Task<List<StakeholderLevelModel>> GetListByStakeholderId(int stakeholderId);
    }
}
