using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IClaimGroupInfrastructure inherites IBaseInfrastructure and provides the interface for ClaimGroup operations in Databasse.
    /// </summary>
    public interface IClaimGroupInfrastructure : IBaseInfrastructure<ApplicationClaimGroup>
    {

        /// <summary>
        /// GetAllByUser fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        Task<AllResponse<ApplicationClaimGroup>> GetAllByUser(AllRequest<ApplicationClaimGroup> applicationClaimGroup);
    }
}