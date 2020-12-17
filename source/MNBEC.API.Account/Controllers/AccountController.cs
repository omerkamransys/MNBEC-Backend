using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MNBEC.API.Account.Extensions;
using MNBEC.API.Core.Controllers;
using MNBEC.ApplicationInterface;
using MNBEC.Core;
using MNBEC.Core.Extensions;
using MNBEC.Core.Interface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Domain.Enumerations;
using MNBEC.ServiceConnectorInterface;
using MNBEC.ViewModel.Account;
using MNBEC.ViewModel.Common;
using WebApp.Models.AccountViewModels;

namespace MNBEC.API.Account.Controllers
{
    /// <summary>
    /// Controller used to for logging in and signing up in connection
    /// authentication or authorization used (Unprotected controller)
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : APIBaseController
    {
        #region Constructor        
        /// <summary>
        /// AccountController initializes class object .
        /// </summary>
        /// <param name="userApplication"></param>
        /// <param name="dealerApplication"></param>
        /// <param name="dealershipApplication"></param>
        /// <param name="employeeApplication"></param>
        /// <param name="roleApplication"></param>  
        /// <param name="emailServiceConnector"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AccountController(IUserApplication userApplication, IEmailApplication emailApplication,  IEmployeeApplication employeeApplication, IRoleApplication roleApplication, IClaimApplication claimApplication,  IEmailServiceConnector emailServiceConnector, IHeaderValue headerValue, IConfiguration configuration, ILogger<AccountController> logger) : base(headerValue, configuration, logger)
        {
            this.UserApplication = userApplication;
            this.EmployeeApplication = employeeApplication;
            this.RoleApplication = roleApplication;
            this.EmailServiceConnector = emailServiceConnector;
            
            this.EmailApplication = emailApplication;
            this.ClaimApplication = claimApplication;
           
        }
        #endregion

        #region Properties and Data Members
        public const string UserId = "UserId";
        public const string ExpirationTime = "ExpirationTime";

        public string AdminPortal = "AdminPortal";
        public string AdminTypeCode = "001";
        public string DealerPortal = "DealerPortal";
        public string InspectorPortal = "InspectorPortal";

        public IUserApplication UserApplication { get; }       
        public IEmployeeApplication EmployeeApplication { get; }
        public IRoleApplication RoleApplication { get; }
        public IEmailServiceConnector EmailServiceConnector { get; }
        public IEmailApplication EmailApplication { get; }
        public IClaimApplication ClaimApplication { get; }
       // public IUserNotificationApplication UserNotificationApplication { get; }
        #endregion

        #region API Methods

        /// <summary>
        /// GetUserById provides API to fetch and returns queried item.
        /// API Path:  api/account/getUserById
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getUserById")]
        [Authorize(Policy = "ACC_GUBI")]
        public async Task<UserResponseVM> GetUserById([FromQuery] UserRequestVM requestVM)
        {
            ApplicationUser request = requestVM.Convert();

            ApplicationUser response = await this.UserApplication.GetbyUserID(request);

            UserResponseVM responseVm = response.Convert(base.UseDefaultLanguage);

            return responseVm;

        }
        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/account/getall
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        [HttpGet("getall")]
        [Authorize(Policy = "ACC_GA")]
        // [AllowAnonymous]
        public async Task<AllResponseVM<UserAllResponseVM>> GetAll([FromQuery] UserAllRequestVM requestVm)
        {
            AllRequest<ApplicationUser> request = requestVm.ConvertAll();

            AllResponse<ApplicationUser> response = await this.UserApplication.GetAll(request);

            AllResponseVM<UserAllResponseVM> responseVm = response.ConvertAll();

            return responseVm;
        }
        /// <summary>
        /// Login provides API to verify user and returns authentication token.
        /// API Path:  api/account/login
        /// </summary>
        /// <param name="paramUser">Username and Password</param>
        /// <returns>{Token: [Token] }</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserRequestVM paramUser, CancellationToken ct)
        {
            var result = await UserApplication.PasswordSignInAsync(paramUser.Email, paramUser.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser request = paramUser.Convert();

                ApplicationUser currentUser = await this.UserApplication.GetUserWithRolesByEmail(request);

                List<ApplicationClaim> UserClaims = await this.ClaimApplication.GetListByUser(currentUser); // commented getUserClaims for claim-based-auth

                var Claims = new ClaimsIdentity(new Claim[]
                            {
                                    new Claim(JwtRegisteredClaimNames.Sub, paramUser.Email.ToString()),
                                    new Claim(UserId, currentUser.UserId.ToString())
                             });

                foreach (var item in UserClaims)
                {
                    Claims.AddClaim(new Claim(item.ClaimCode, string.Empty));
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var encryptionkey = Configuration["Jwt:Encryptionkey"];
                var key = Encoding.ASCII.GetBytes(encryptionkey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = Configuration["Jwt:Issuer"],
                    Subject = Claims,

                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(Configuration["Jwt:ExpiryTimeInMinutes"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = tokenString
                });

            }
            return BadRequest("Wrong Username or password");
        }
        /// <summary>
        /// Update provides API to update existing object in database and returns true if action was successfull.
        /// API Path:  api/account/update
        /// </summary>


        [HttpPost("update")]
        [Authorize(Policy = "ACC_UP")]
        public async Task<IActionResult> Update([FromBody] UserRequestVM requestVm, CancellationToken ct)
        {
            ApplicationUser user = requestVm.Convert();

            var result = await UserApplication.Update(user, ct);
            // Checking if user was created
            if (result.Succeeded)
            {
                // bool isExisted;
                await UserApplication.RemoveExistingRolesForUser(user, ct);

                foreach (var role in requestVm.Roles)
                {      
                    await UserApplication.InsertRole(user, role.RoleName, ct);
                }

                return Ok();
            }
            else
            {
                return BadRequest("User Not Updated");
            }
        }

        /// <summary>
        /// CreateUser provides API to add new object in database and returns provided ObjectId.
        /// API Path: api/account/createuser
        /// </summary>
        /// <param name="requestVm"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("createuser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestVM requestVm, CancellationToken ct)
        {
            var password = Constant.AlphanumericCaps.RandomPassword(Constant.DefaultPasswordLength);
            ApplicationUser user = requestVm.ConvertAdd();
            var result = await UserApplication.CreateAsync(user, password, false, base.UseDefaultLanguage);//TODO: need to modified zeeshan

            // Checking if user was created
            if (result.Succeeded)
            {
                await UserApplication.AddToRolesAsync(user, requestVm.Roles, ct);
                Employee employee = new Employee
                {
                    UserTypeId = requestVm.UserTypeId,
                    EmployeeId = user.UserId
                };

                await EmployeeApplication.Add(employee);
                return Ok(user.UserId);
            }
            else
            {
                return BadRequest(user.UserId);

            }
        }

        /// <summary>
        /// GenerateResetLink provides link for password reset
        /// </summary>
        /// <param name="user"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string GenerateResetLink(ApplicationUser user, string code)
        {
            return "?userId=" + user.UserId + "&code=" + code;
        }

        /// <summary>
        /// GetResetMessage provides message for password reset link
        /// </summary>
        /// <param name="callbackUrl"></param>
        /// <returns></returns>
        private static string GetResetMessage(string callbackUrl)
        {
            return $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>";
        }

        /// <summary>
        /// ForgotPassword provides API for generate link for password reset
        /// API Path: api/account/forgotPassword
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model, CancellationToken ct)
        {
            if (base.HeaderValue?.ApplicationKey == this.DealerPortal)
            {
                return await DealerForgetPassword(model, ct);
            }
            else if (base.HeaderValue?.ApplicationKey == this.InspectorPortal)
            {
                return await InspectorForgetPassword(model, ct);
            }
            else
            {
                return await AdminForgetPassword(model, ct);
            }
        }

        private async Task<IActionResult> AdminForgetPassword(ForgotPasswordViewModel model, CancellationToken ct)
        {
            ApplicationUser user = null;
            user = await UserApplication.GetbyEmail(model.Email, ct);
            if (user == null)
            {
                return BadRequest("No user exist with this email");
            }
            else
            {
                await UserApplication.SendForgetPasswordEmail(user, base.UseDefaultLanguage);
                return Ok();
            }
        }

        private async Task<IActionResult> InspectorForgetPassword(ForgotPasswordViewModel model, CancellationToken ct)
        {
            ApplicationUser user = null;
            user = await UserApplication.GetInspectorbyEmail(model.Email, ct);
            if (user == null)
            {
                return BadRequest("No user exist with this email");
            }
            else
            {
                await UserApplication.InspectorSendForgetPasswordEmail(user, base.UseDefaultLanguage);
                return Ok();
            }
        }

        private async Task<IActionResult> DealerForgetPassword(ForgotPasswordViewModel model, CancellationToken ct)
        {
            ApplicationUser user = null;
            user = await UserApplication.GetDealerbyEmail(model.Email, ct);
            if (user == null)
            {
                return BadRequest("No user exist with this email");
            }
            else
            {
                await UserApplication.DealerSendForgetPasswordEmail(user, base.UseDefaultLanguage);
                return Ok();
            }
        }

        /// <summary>
        /// ResetPassword provides API to reset password
        /// API Path: api/account/forgotPassword
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(UserRequestVM requestVM, CancellationToken ct)
        {
            ApplicationUser applicationUser = requestVM.Convert();
            var user = await UserApplication.GetbyUserID(applicationUser);
            if (user == null)
            {

                // Don't reveal that the user does not exist
                //return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await UserApplication.ResetPasswordAsync(user, requestVM.Code, requestVM.Password);
            if (result.Succeeded)
            {
                return Ok();

            }
            return BadRequest("Not updated");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>

        [HttpPost("changepassword")]
        [Authorize(Policy = "ACC_CP")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, CancellationToken ct)
        {
            var user = await UserApplication.GetbyEmail(model.Email, ct);
            if (user == null)
            {

                return BadRequest("User dose not exist");
            }
            var obj = await UserApplication.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (obj.Succeeded)
            {
                var result = await UserApplication.ResetPasswordAsync(user, model.Code, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok();

                }
            }

            return BadRequest("you have entered an invalid password");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>

        [HttpGet("getUserByEmail")]
        [Authorize(Policy = "ACC_GBE")]
        public async Task<ApplicationUser> GetUserByEmail([FromQuery] UserRequestVM requestVM)
        {
            ApplicationUser request = requestVM.Convert();
            var user = await UserApplication.GetUserbyEmail(request.Email);

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>

        [HttpGet("GetUsersByRole")]
        //[Authorize(Policy = "ACC_GUBR")]
        [AllowAnonymous]
        public async Task<List<UserListResponseVM>> GetUsersByRole([FromQuery] UserGetByRoleRequestVM requestVM)
        {
            ApplicationUser request = requestVM.Convert();
            List<ApplicationUser> response = await UserApplication.GetUsersByRole(request);

            //todo: convert to VM
            List<UserListResponseVM> responseVm = response.ConvertList();

            return responseVm;
        }

        /// <summary>
        /// Search provides API to fetch and returns queried item.
        /// API Path:  api/account/search
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [Authorize(Policy = "ACC_SR")]
        public async Task<List<UserListResponseVM>> Search([FromQuery] string search)
        {
            List<ApplicationUser> response = await this.UserApplication.Search(search);

            List<UserListResponseVM> responseVm = response.ConvertList();

            return responseVm;
        }

        

        

        
        #endregion API Methods

        #region Private Methods
        
        #endregion API Methods

        /// <summary>
        /// Index Method used to keep api alive.
        /// Azure always on feature ping this method. don't remove it. 
        /// API Path:  /
        /// </summary>
        /// <returns>Test string</returns>
        [HttpGet("/")]
        [AllowAnonymous]
        public string Index()
        {
            return "Hello Test at: " + DateTime.Now;
        }
    }
}
