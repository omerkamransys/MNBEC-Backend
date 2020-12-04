using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;

namespace MNBEC.ApplicationInterface
{
    /// <summary>
    /// IClaimGroupApplication inherits IBaseApplication interface to provide interface for ApplicationClaimGroup related Application.
    /// </summary>
    public interface IClaimGroupApplication : IBaseApplication<ApplicationClaimGroup>
    {
        /// <summary>
        /// GetAllByUser calls ClaimGroupInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        Task<AllResponse<ApplicationClaimGroup>> GetAllByUser(AllRequest<ApplicationClaimGroup> applicationClaimGroup);
    }
}
