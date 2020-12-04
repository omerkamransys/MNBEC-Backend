using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IClaimInfrastructure inherites IBaseInfrastructure and provides the interface for ApplicationClaim operations in Databasse.
    /// </summary>
    public interface IClaimInfrastructure : IBaseInfrastructure<ApplicationClaim>
    {
        /// <summary>
        /// GetListByRole fetch and returns queried list of items with specific fields from database by role.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        Task<List<ApplicationClaim>> GetListByRole(ApplicationRole applicationRole);

        /// <summary>
        /// GetListByUser fetch and returns queried list of items with specific fields from database by user.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        Task<List<ApplicationClaim>> GetListByUser(ApplicationUser applicationUser);

        Task<List<ApplicationRole>> GetAllClaimsWithRole();
    }
}