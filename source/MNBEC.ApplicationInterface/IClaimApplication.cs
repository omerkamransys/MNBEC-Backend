using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.ApplicationInterface
{
    /// <summary>
    /// IClaimApplication inherits IBaseApplication interface to provide interface for ApplicationClaim related Application.
    /// </summary>
    public interface IClaimApplication : IBaseApplication<ApplicationClaim>
    {
        /// <summary>
        /// GetListByRole fetch and returns queried list of items by Role.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        Task<List<ApplicationClaim>> GetListByRole(ApplicationRole applicationRole);

        /// <summary>
        /// GetListByUser fetch and returns queried list of items by User.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        Task<List<ApplicationClaim>> GetListByUser(ApplicationUser applicationUser);

        Task<List<ApplicationRole>> GetAllClaimsWithRole();
    }
}
