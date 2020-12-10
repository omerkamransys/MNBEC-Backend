using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.API.Account.Extensions;
using MNBEC.API.Core.Controllers;
using MNBEC.ApplicationInterface;
using MNBEC.Core.Interface;
using MNBEC.Domain;
using MNBEC.ViewModel.Account;

namespace MNBEC.API.Account.Controllers
{
    /// <summary>
    /// ClaimController is controller class and inherits APIBaseController. It provides APIs for ApplicationClaim.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimController : APIBaseController
    {
        #region Constructor
        /// <summary>
        /// ClaimController initializes class object.
        /// </summary>
        /// <param name="claimApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ClaimController(IClaimApplication claimApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<ClaimController> logger) : base(headerValue, configuration, logger)
        {
            this.ClaimApplication = claimApplication;
        }
        #endregion

        #region Properties and Data Members
        public IClaimApplication ClaimApplication { get; }
        #endregion

        #region API Methods

        ///// <summary>
        ///// Add provides API to add new object in database and returns provided ObjectId.
        ///// API Path: api/applicationClaim/add
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpPost("add")]
        //public async Task<int> Add([FromBody] ApplicationClaimRequestVM requestVM)
        //{
        //    ApplicationClaim request = requestVM.Convert();

        //    var applicationClaimId = await this.ClaimApplication.Add(request);

        //    return applicationClaimId;
        //}


        ///// <summary>
        ///// Delete provides API to delete record and returns true if action was successfull.
        ///// API Path: api/applicationClaim/activate
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpPut("activate")]
        //public async Task<bool> Activate([FromBody] ApplicationClaimRequestVM requestVM)
        //{
        //    ApplicationClaim request = requestVM.Convert();

        //    var response = await this.ClaimApplication.Activate(request);

        //    return response;
        //}

        ///// <summary>
        ///// Get provides API to fetch and returns queried item.
        ///// API Path:  api/applicationClaim/get
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpGet("get")]
        //public async Task<ApplicationClaimResponseVM> Get([FromQuery] ApplicationClaimListRequestVM requestVM)
        //{
        //    ApplicationClaim request = requestVM.Convert();

        //    ApplicationClaim response = await this.ClaimApplication.Get(request);

        //    ApplicationClaimResponseVM responseVM = response.Convert();

        //    return responseVM;
        //}

        ///// <summary>
        ///// GetAll provides API to fetch and returns queried list of items.
        ///// API Path:  api/applicationClaim/getall
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpGet("getall")]
        //public async Task<AllResponseVM<ApplicationClaimAllResponseVM>> GetAll([FromQuery] ApplicationClaimAllRequestVM requestVM)
        //{
        //    AllRequest<ApplicationClaim> request = requestVM.ConvertAll();

        //    AllResponse<ApplicationClaim> response = await this.ClaimApplication.GetAll(request);

        //    AllResponseVM<ApplicationClaimAllResponseVM> responseVM = response.ConvertAll();

        //    return responseVM;
        //}

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/applicationClaim/getlist
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        [Authorize(Policy = "CLM_GL")]
        public async Task<List<ClaimListResponseVM>> GetList([FromQuery] ClaimListRequestVM requestVM)
        {
            ApplicationClaim request = requestVM.Convert();
            bool language = base.UseDefaultLanguage;
            List<ApplicationClaim> response = await this.ClaimApplication.GetList(request);

            List<ClaimListResponseVM> responseVM = response.ConvertList(base.UseDefaultLanguage);

            return responseVM;
        }

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/applicationClaim/getlist
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getlistbyrole")]
        //[Authorize(Policy = "CLM_GLBR")]
        [AllowAnonymous]
        public async Task<List<ClaimListResponseVM>> GetListByRole([FromQuery] RoleRequestVM requestVM)
        {
            ApplicationRole request = requestVM.Convert();

            List<ApplicationClaim> response = await this.ClaimApplication.GetListByRole(request);

            List<ClaimListResponseVM> responseVM = response.ConvertList(base.UseDefaultLanguage);

            return responseVM;
        }

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/applicationClaim/getlist
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getlistbyuser")]
        [Authorize(Policy = "CLM_GLBU")]
        public async Task<List<ClaimListResponseVM>> GetListByUser([FromQuery] UserRequestVM requestVM)
        {
            ApplicationUser request = requestVM.Convert();

            List<ApplicationClaim> response = await this.ClaimApplication.GetListByUser(request);

            List<ClaimListResponseVM> responseVM = response.ConvertList(base.UseDefaultLanguage);

            return responseVM;
        }

        ///// <summary>
        ///// Update provides API to update existing object in database and returns true if action was successfull.
        ///// API Path:  api/applicationClaim/update
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //// GET api/applicationClaim/update
        //[HttpPut("update")]
        //public async Task<bool> Update([FromBody] ApplicationClaimRequestVM requestVM)
        //{
        //    ApplicationClaim request = requestVM.Convert();

        //    var isUpdated = await this.ClaimApplication.Update(request);

        //    return isUpdated;
        //}
        #endregion
    }
}
