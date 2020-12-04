using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IapplicationuserInfrastructure inherites IBaseInfrastructure and provides the interface for applicationuser operations in Databasse.
    /// </summary>
    public interface IUserInfrastructure 
    {
        Task<ApplicationUser> GetByUserIdAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserWithRolesByEmail(ApplicationUser user);
        Task<List<ApplicationUser>> GetUsersByRole(ApplicationUser user);
        Task<AllResponse<ApplicationUser>> GetAll(AllRequest<ApplicationUser> make);
       
        Task<List<ApplicationUser>> Search(string searchText);
        Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken);
        Task<bool> RemoveExistingRolesForUser(ApplicationUser user, CancellationToken ct);
        Task<uint> InsertRole(ApplicationUser user, string roleName, CancellationToken ct);
        Task<List<ApplicationUser>> GetAdmins();
        Task<ApplicationUser> GetDealerbyEmail(string normalizedEmail, CancellationToken ct);
        Task<ApplicationUser> GetInspectorbyEmail(string normalizedEmail, CancellationToken ct);

        Task<ApplicationUser> GetUserTypeByEmailAsync(string normalizedEmail);
    }
}