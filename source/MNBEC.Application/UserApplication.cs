using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;
using MNBEC.ServiceConnectorInterface;

namespace MNBEC.Application
{
    public class UserApplication : BaseApplication, IUserApplication, IPasswordHasher<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #region Constructor
        /// <summary>
        /// UserApplication initailizes object instance.
        /// </summary>
        /// <param name="applicationUserInfrastructure"></param>
        /// <param name="emailApplication"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UserApplication(IUserInfrastructure applicationUserInfrastructure, IEmailApplication emailApplication, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,  IConfiguration configuration, ILogger<UserApplication> logger) : base(configuration, logger)
        {
            this.ApplicationUserInfrastructure = applicationUserInfrastructure;
            this.EmailApplication = emailApplication;
           

            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
        #region Properties and Data Members
        /// <summary>
        /// MakeInfrastructure holds the Infrastructure object.
        /// </summary>
        /// 
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();
        public IUserInfrastructure ApplicationUserInfrastructure { get; }
        public IEmailApplication EmailApplication { get; }
      

        #endregion
        /// <summary>
        /// CreateAsync calls UserInfrastructure to adds new object in database and returns provided IdentityResult.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string Password, bool isdealer, bool useDefaultLanguage)
        {
            var result = await _userManager.CreateAsync(user, Password);
            user.ResetUrlKey = await GetPasswordRestToken(user);
            //if (result.Succeeded == true && isdealer == false)
            //{
            //    await this.EmailApplication.SendEmployeeCreationEmail(user, useDefaultLanguage);
            //}
            if (result.Succeeded)
            {
                user.PasswordHash = "";
              
            }
           
            return result;

        }
        public async Task<string> Get(ApplicationUser user, CancellationToken ct)
        {
            return await _userManager.GetUserIdAsync(user);
        }
        public async Task<AllResponse<ApplicationUser>> GetAll(AllRequest<ApplicationUser> user)
        {
            return await this.ApplicationUserInfrastructure.GetAll(user);
        }
        public async Task<ApplicationUser> GetbyUserID(ApplicationUser User)
        {
            return await this.ApplicationUserInfrastructure.GetByUserIdAsync(User);
        }

        public async Task<ApplicationUser> GetUserWithRolesByEmail(ApplicationUser User)
        {
            return await this.ApplicationUserInfrastructure.GetUserWithRolesByEmail(User);
        }

        public async Task<ApplicationUser> GetbyEmail(string normalizedEmail, CancellationToken ct)
        {
            return await _userManager.FindByEmailAsync(normalizedEmail);
        }
        public async Task<ApplicationUser> GetDealerbyEmail(string normalizedEmail, CancellationToken ct)
        {
            return await this.ApplicationUserInfrastructure.GetDealerbyEmail(normalizedEmail, ct);
        }
        public async Task<ApplicationUser> GetInspectorbyEmail(string normalizedEmail, CancellationToken ct)
        {
            return await this.ApplicationUserInfrastructure.GetInspectorbyEmail(normalizedEmail, ct);
        }
        public async Task<ApplicationUser> GetUserTypeByEmailAsync(string normalizedEmail)
        {
            return await this.ApplicationUserInfrastructure.GetUserTypeByEmailAsync(normalizedEmail);
        }

        public async Task<ApplicationUser> GetUserbyEmail(string normalizedEmail)
        {
            return await _userManager.FindByEmailAsync(normalizedEmail);
        }

        public async Task<List<ApplicationUser>> GetUsersByRole(ApplicationUser applicationUser)
        {
            return await this.ApplicationUserInfrastructure.GetUsersByRole(applicationUser);
        }

        public async Task<List<ApplicationUser>> GetAdmins()
        {
            return await this.ApplicationUserInfrastructure.GetAdmins();
        }
        public async Task AddToRoleAsync(ApplicationUser user, string role, CancellationToken ct)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
        public async Task AddToRolesAsync(ApplicationUser user, IEnumerable<UserRoles> role, CancellationToken ct)
        {
            foreach (var r in role)
            {
                await _userManager.AddToRoleAsync(user, r.RoleName);
            }

        }
        public async Task<bool> GetUserRole(ApplicationUser user, string role, CancellationToken ct)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            //  return await _userManager.ResetPasswordAsync(user, token, newPassword);
            var p = HashPassword(user, newPassword);
            await this.ApplicationUserInfrastructure.SetPasswordHashAsync(user, p, new CancellationToken());
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> UpdatePasswordAsync(ApplicationUser user, string newPassword)
        {
            var result = await _userManager.RemovePasswordAsync(user);

            var p = HashPassword(user, newPassword);

            await this.ApplicationUserInfrastructure.SetPasswordHashAsync(user, p, new CancellationToken());
            return result;


        }
        public async Task<IdentityResult> Update(ApplicationUser user, CancellationToken ct)
        {
            var result = await _userManager.UpdateAsync(user);
            
            return result;
            //return await _userManager.UpdateAsync(user);
        }

        public async Task<bool> RemoveExistingRolesForUser(ApplicationUser user, CancellationToken ct)
        {
            // return await _userManager.UpdateAsync(user);
            return await ApplicationUserInfrastructure.RemoveExistingRolesForUser(user, ct);
        }
        public async Task<string> GetPasswordRestToken(ApplicationUser user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return code;
        }
        public async Task<ApplicationUser> GetbyUserName(string userName, CancellationToken ct)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
        public async Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }
        public async Task<List<ApplicationUser>> Search(string searchText)
        {
            return await this.ApplicationUserInfrastructure.Search(searchText);
        }
        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
        public virtual string HashPassword(ApplicationUser user, string password)
        {
            //if (_compatibilityMode == PasswordHasherCompatibilityMode.IdentityV2)
            //{
            return Convert.ToBase64String(HashPasswordV2(password, rng));
            //}
            //else
            //{
            //    return Convert.ToBase64String(HashPasswordV3(password, _rng));
            //}
        }

        public async Task<bool> SendForgetPasswordEmail(ApplicationUser user, bool useDefault)
        {
            user.ResetUrlKey = await GetPasswordRestToken(user);

            return await this.EmailApplication.EmployeeSendForgotPasswordEmail(user, useDefault);
        }

        public async Task<bool> DealerSendForgetPasswordEmail(ApplicationUser user, bool useDefault)
        {
            user.ResetUrlKey = await GetPasswordRestToken(user);

            return await this.EmailApplication.DealerSendForgotPasswordEmail(user, useDefault);

        }

       
        public async Task<bool> DealerSendResetPasswordEmail(ApplicationUser user, bool useDefault)
        {
            user.ResetUrlKey = await GetPasswordRestToken(user);

            return await this.EmailApplication.DealerSendForgotPasswordEmail(user, useDefault);

        }


        private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // Produce a version 2 text hash.
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return outputBytes;
        }

        public async Task<int> InsertRole(ApplicationUser user, string roleName, CancellationToken ct)
        {
            return await ApplicationUserInfrastructure.InsertRole(user, roleName, ct);
        }

        public Task<bool> InspectorSendForgetPasswordEmail(ApplicationUser user, bool useDefault)
        {
            throw new NotImplementedException();
        }
    }
}
