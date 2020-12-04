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
    /// RoleController is controller class and inherits APIBaseController. It provides APIs for ApplicationRole.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : APIBaseController
    {
        #region Constructor
        /// <summary>
        /// RoleController initializes class object.
        /// </summary>
        /// <param name="roleApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public RoleController(IRoleApplication roleApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<RoleController> logger) : base(headerValue, configuration, logger)
        {
            this.RoleApplication = roleApplication;
        }
        #endregion

        #region Properties and Data Members
        public IRoleApplication RoleApplication { get; }
        #endregion

        //TODO: Remove comments
        #region API Methods
        /// <summary>
        /// Add provides API to add new object in database and returns provided ObjectId.
        /// API Path: api/role/add
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [Authorize(Policy = "RL_AD")]
        public async Task<uint> Add([FromBody] RoleAddRequestVM requestVM)
        {
            ApplicationRole request = requestVM.ConvertAdd();

            var applicationRoleId = await this.RoleApplication.Add(request);

            return applicationRoleId;
        }

        /// <summary>
        /// Delete provides API to delete record and returns true if action was successfull.
        /// API Path: api/role/activate
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("activate")]
        [Authorize(Policy = "RL_ACT")]
        public async Task<bool> Activate([FromBody] RoleRequestVM requestVM)
        {
            ApplicationRole request = requestVM.Convert();

            var response = await this.RoleApplication.Activate(request);

            return response;
        }

        /// <summary>
        /// Get provides API to fetch and returns queried item.
        /// API Path:  api/role/get
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [Authorize(Policy = "RL_GET")]
        public async Task<RoleResponseVM> Get([FromQuery] RoleRequestVM requestVM)
        {
            ApplicationRole request = requestVM.Convert();

            ApplicationRole response = await this.RoleApplication.Get(request);

            RoleResponseVM responseVM = response.Convert(base.UseDefaultLanguage);

            return responseVM;
        }

        //TODO: Remove comments.
        //[HttpGet("getRoleById")]
        //public async Task<RoleResponseVM> GetRoleById([FromQuery] RoleRequestVM requestVM)
        //{
        //    ApplicationRole request = requestVM.Convert();

        //    ApplicationRole response = await this.RoleApplication.GetRoleById(request);

        //    RoleResponseVM responseVM = response.Convert();

        //    return responseVM;
        //}

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/role/getall
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getall")]
        [Authorize(Policy = "RL_GA")]
        public async Task<AllResponseVM<RoleAllResponseVM>> GetAll([FromQuery] RoleAllRequestVM requestVM)
        {
            AllRequest<ApplicationRole> request = requestVM.ConvertAll();

            AllResponse<ApplicationRole> response = await this.RoleApplication.GetAll(request);

            AllResponseVM<RoleAllResponseVM> responseVM = response.ConvertAll(base.UseDefaultLanguage);

            return responseVM;
        }

        /// <summary>
        /// GetList provides API to fetch and returns queried list of items.
        /// API Path:  api/role/getlist
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        [Authorize(Policy = "RL_GL")]
        public async Task<List<RoleListResponseVM>> GetList([FromQuery] RoleRequestVM requestVM)
        {
            ApplicationRole request = requestVM.Convert();

            List<ApplicationRole> response = await this.RoleApplication.GetList(request);

            List<RoleListResponseVM> responseVM = response.ConvertList(base.UseDefaultLanguage);

            return responseVM;
        }

        /// <summary>
        /// Update provides API to update existing object in database and returns true if action was successfull.
        /// API Path:  api/role/update
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        // GET api/applicationRole/update
        [HttpPut("update")]
        [Authorize(Policy = "RL_UP")]
        public async Task<bool> Update([FromBody] RoleUpdateRequestVM requestVM)
        {
            ApplicationRole request = requestVM.ConvertUpdate();

            var response = await this.RoleApplication.Update(request);

            return response;
        }
        #endregion
    }
}
