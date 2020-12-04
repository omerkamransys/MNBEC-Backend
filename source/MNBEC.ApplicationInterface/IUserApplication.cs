using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;

namespace MNBEC.ApplicationInterface
{
    public interface IUserApplication /*: IBaseApplication<ApplicationUser>*/
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string Password, bool isdealer, bool useDefaultLanguage);
        Task AddToRoleAsync(ApplicationUser user, string role, CancellationToken ct);
        Task AddToRolesAsync(ApplicationUser user, IEnumerable<UserRoles> role, CancellationToken ct);
        Task<string> GetPasswordRestToken(ApplicationUser user);
        Task<ApplicationUser> GetbyEmail(string normalizedEmail, CancellationToken ct);
        Task<ApplicationUser> GetDealerbyEmail(string normalizedEmail, CancellationToken ct);
        Task<ApplicationUser> GetInspectorbyEmail(string normalizedEmail, CancellationToken ct);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);

        Task<ApplicationUser> GetbyUserName(string userName, CancellationToken ct);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure);
        Task<IdentityResult> Update(ApplicationUser make, CancellationToken ct);
        Task<bool> GetUserRole(ApplicationUser user, string role, CancellationToken ct);
        Task<ApplicationUser> GetbyUserID(ApplicationUser user);
        Task<ApplicationUser> GetUserWithRolesByEmail(ApplicationUser user);
        Task<AllResponse<ApplicationUser>> GetAll(AllRequest<ApplicationUser> centre);
        Task<IdentityResult> UpdatePasswordAsync(ApplicationUser user, string newPassword);
        Task<List<ApplicationUser>> Search(string searchText);
        Task<ApplicationUser> GetUserbyEmail(string normalizedEmail);
        Task<List<ApplicationUser>> GetUsersByRole(ApplicationUser applicationUser);
        Task<List<ApplicationUser>> GetAdmins();
        Task<bool> SendForgetPasswordEmail(ApplicationUser user, bool useDefault);
        Task<bool> DealerSendForgetPasswordEmail(ApplicationUser user, bool useDefault);
        Task<bool> DealerSendResetPasswordEmail(ApplicationUser user, bool useDefault);
        Task<bool> InspectorSendForgetPasswordEmail(ApplicationUser user, bool useDefault);
        Task<bool> RemoveExistingRolesForUser(ApplicationUser make, CancellationToken ct);
        Task<uint> InsertRole(ApplicationUser user, string roleName, CancellationToken ct);
        Task<ApplicationUser> GetUserTypeByEmailAsync(string normalizedEmail);


    }
}
