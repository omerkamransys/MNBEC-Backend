using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

using MNBEC.ApplicationInterface;

using MNBEC.Domain;


using MNBEC.API.Core.Controllers;
using MNBEC.Core.Interface;
using MNBEC.ViewModel.Configuration;
using MNBEC.API.Vehicle.Extensions;
using MNBEC.ViewModel.Common;
using MNBEC.Domain.Common;

namespace MNBEC.API.Configuration.Controllers
{
    /// <summary>
    /// MakeController is controller class and inherits APIBaseController. It provides APIs for Make.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChannelController : APIBaseController
    {
        #region Constructor
        /// <summary>
        /// ChannelController initializes class object.
        /// </summary>
        /// <param name="ChannelApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ChannelController(IChannelApplication ChannelApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<ChannelController> logger) : base(headerValue, configuration, logger)
        {
            this.ChannelApplication = ChannelApplication;
        }
        #endregion

        #region Properties and Data Members
        public IChannelApplication ChannelApplication { get; }
        #endregion

        #region API Methods

        /// <summary>
        /// Add provides API to add new object in database and returns provided ObjectId.
        /// API Path: api/Channel/add
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<uint> Add([FromBody] ChannelAddRequestVM requestVm)
        {
            Channel request = requestVm.ConvertAdd();

            var response = await this.ChannelApplication.Add(request);

            return response;
        }


        /// <summary>
        /// Activate provides API to activasste/deactivate record and returns true if action was successfull.
        /// API Path: api/Channel/activate
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        [HttpPut("activate")]
        [AllowAnonymous]
        public async Task<bool> Activate([FromBody] ChannelActivateRequestVM requestVm)
        {
            Channel request = requestVm.ConvertActivate();

            var response = await this.ChannelApplication.Activate(request);

            return response;
        }
       

        /// <summary>
        /// Get provides API to fetch and returns queried item.
        /// API Path:  api/Channel/get
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [AllowAnonymous]
        public async Task<ChannelResponseVM> Get([FromQuery] ChannelRequestVM requestVm)
        {
            Channel request = requestVm.Convert();

            Channel response = await this.ChannelApplication.Get(request);

            ChannelResponseVM responseVm = response.Convert(base.UseDefaultLanguage);

            return responseVm;
        }

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/Channel/getall
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<AllResponseVM<ChannelAllResponseVM>> GetAll([FromQuery] ChannelAllRequestVM requestVm)
        {
            AllRequest<Channel> request = requestVm.ConvertAll();

            AllResponse<Channel> response = await this.ChannelApplication.GetAll(request);

            AllResponseVM<ChannelAllResponseVM> responseVm = response.ConvertAll(base.UseDefaultLanguage);

            return responseVm;
        }

        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/Channel/getlist
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        [AllowAnonymous]
        public async Task<List<ChannelListResponseVM>> GetList([FromQuery] ChannelRequestVM requestVm)
        {
            Channel request = requestVm.Convert();

            List<Channel> response = await this.ChannelApplication.GetList(request);

            List<ChannelListResponseVM> responseVm = response.ConvertList(base.UseDefaultLanguage);

            return responseVm;
        }
        

        /// <summary>
        /// Update provides API to update existing object in database and returns true if action was successfull.
        /// API Path:  api/Channel/update
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        // GET api/Channel/update
        [HttpPut("update")]
        [AllowAnonymous]
        public async Task<bool> Update([FromBody] ChannelUpdateRequestVM requestVm)
        {
            Channel request = requestVm.ConvertUpdate();

            var response = await this.ChannelApplication.Update(request);

            return response;
        }
        #endregion
    }
}
