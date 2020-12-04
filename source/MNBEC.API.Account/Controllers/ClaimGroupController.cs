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
using MNBEC.Domain.Common;
using MNBEC.ViewModel.Account;
using MNBEC.ViewModel.Common;

namespace MNBEC.API.Account.Controllers
{
    /// <summary>
    /// ClaimGroupController is controller class and inherits APIBaseController. It provides APIs for ClaimGroup.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimGroupController : APIBaseController
    {
        #region Constructor
        /// <summary>
        /// ClaimGroupController initializes class object.
        /// </summary>
        /// <param name="claimGroupApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ClaimGroupController(IClaimGroupApplication claimGroupApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<ClaimGroupController> logger) : base(headerValue, configuration, logger)
        {
            this.ClaimGroupApplication = claimGroupApplication;
        }
        #endregion

        #region Properties and Data Members
        public IClaimGroupApplication ClaimGroupApplication { get; }
        #endregion

        #region API Methods

        ///// <summary>
        ///// Add provides API to add new object in database and returns provided ObjectId.
        ///// API Path: api/applicationClaimGroup/add
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpPost("add")]
        //public async Task<uint> Add([FromBody] ClaimGroupRequestVM requestVM)
        //{
        //    ApplicationClaimGroup request = requestVM.Convert();

        //    var applicationClaimGroupId = await this.ClaimGroupApplication.Add(request);

        //    return applicationClaimGroupId;
        //}


        ///// <summary>
        ///// Delete provides API to delete record and returns true if action was successfull.
        ///// API Path: api/applicationClaimGroup/activate
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpPut("activate")]
        //public async Task<bool> Activate([FromBody] ApplicationClaimGroupRequestVM requestVM)
        //{
        //    ApplicationClaimGroup request = requestVM.Convert();

        //    var response = await this.ClaimGroupApplication.Activate(request);

        //    return response;
        //}

        ///// <summary>
        ///// Get provides API to fetch and returns queried item.
        ///// API Path:  api/applicationClaimGroup/get
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpGet("get")]
        //public async Task<ApplicationClaimGroupResponseVM> Get([FromQuery] ApplicationClaimGroupRequestVM requestVM)
        //{
        //    ApplicationClaimGroup request = requestVM.Convert();

        //    ApplicationClaimGroup response = await this.ClaimGroupApplication.Get(request);

        //    ApplicationClaimGroupResponseVM responseVM = response.Convert();

        //    return responseVM;
        //}

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/applicationClaimGroup/getall
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getall")]
        [Authorize(Policy = "CLMG_GA")]
        public async Task<AllResponseVM<ClaimGroupResponseVM>> GetAll([FromQuery] ClaimGroupAllRequestVM requestVM)
        {
            AllRequest<ApplicationClaimGroup> request = requestVM.ConvertAll();

            AllResponse<ApplicationClaimGroup> response = await this.ClaimGroupApplication.GetAll(request);

            AllResponseVM<ClaimGroupResponseVM> responseVM = response.ConvertAll(base.UseDefaultLanguage);

            return responseVM;
        }

        /// <summary>
        /// GetAllByUser provides API to fetch and returns queried list of items.
        /// API Path:  api/applicationClaimGroup/getall
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getallbyuser")]
        [Authorize(Policy = "CLMG_GABU")]
        public async Task<AllResponseVM<ClaimGroupResponseVM>> GetAllByUser([FromQuery] ClaimGroupAllRequestVM requestVM)
        {
            AllRequest<ApplicationClaimGroup> request = requestVM.ConvertAll();

            AllResponse<ApplicationClaimGroup> response = await this.ClaimGroupApplication.GetAllByUser(request);

            AllResponseVM<ClaimGroupResponseVM> responseVM = response.ConvertAll(base.UseDefaultLanguage);

            return responseVM;
        }

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/applicationClaimGroup/getlist
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        [Authorize(Policy = "CLMG_GL")]
        public async Task<List<ClaimGroupListResponseVM>> GetList([FromQuery] ClaimGroupRequestVM requestVM)
        {
            ApplicationClaimGroup request = requestVM.Convert();
            List<ApplicationClaimGroup> response = await this.ClaimGroupApplication.GetList(request);

            List<ClaimGroupListResponseVM> responseVM = response.ConvertList(base.UseDefaultLanguage);

            return responseVM;
        }

        ///// <summary>
        ///// Update provides API to update existing object in database and returns true if action was successfull.
        ///// API Path:  api/applicationClaimGroup/update
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //// GET api/applicationClaimGroup/update
        //[HttpPut("update")]
        //public async Task<bool> Update([FromBody] ApplicationClaimGroupRequestVM requestVM)
        //{
        //    ApplicationClaimGroup request = requestVM.Convert();

        //    var isUpdated = await this.ClaimGroupApplication.Update(request);

        //    return isUpdated;
        //}
        #endregion
    }
}
