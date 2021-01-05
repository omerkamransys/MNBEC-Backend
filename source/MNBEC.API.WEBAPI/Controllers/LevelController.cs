using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.API.Core.Controllers;
using MNBEC.ApplicationInterface;
using MNBEC.Core.Interface;
using MNBEC.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MNBEC.API.WEBAPI.Controllers
{
    /// <summary>
    /// LevelController is controller class and inherits APIBaseController. It provides APIs for Level.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LevelController : APIBaseController
    {
        #region Constructor
        /// <summary>
        ///  LevelController initializes class object.
        /// </summary>
        /// <param name="levelApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public LevelController(ILevelApplication levelApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<LevelController> logger) : base(headerValue, configuration, logger)
        {
            this.LevelApplication = levelApplication;
        }
        #endregion

        #region Properties and Data Members
        public ILevelApplication LevelApplication { get; }
        #endregion

        #region API Methods

        /// <summary>
        /// Add provides API to add new object in database and returns provided ObjectId.
        /// API Path: api/level/add
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [Authorize(Policy = "ORG_HR_AD")]
        public async Task<int> Add([FromBody] Level request)
        {

            var levelId = await this.LevelApplication.Add(request);

            return levelId;
        }


        /// <summary>
        /// Activate provides API to activate/deactivate record and returns true if action was successfull.
        /// API Path: api/level/activate
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("activate")]
        [Authorize(Policy = "ORG_HR_ACT")]
        public async Task<bool> Activate([FromBody] Level request)
        {
            var response = await this.LevelApplication.Activate(request);

            return response;
        }

        /// <summary>
        /// Get provides API to fetch and returns queried item.
        /// API Path:  api/level/get
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [Authorize(Policy = "ORG_HR_GET")]
        public async Task<Level> Get([FromQuery] Level request)
        {
            Level response = await this.LevelApplication.Get(request);

            return response;
        }

        /// <summary>
        /// GetList provides API to fetch and returns queried list of items.
        /// API Path:  api/level/getlist
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        [Authorize(Policy = "ORG_HR_GL")]
        public async Task<List<Level>> GetList([FromQuery] Level request)
        {
            List<Level> response = await this.LevelApplication.GetList(request);

            return response;
        }

        /// <summary>
        /// Update provides API to update existing object in database and returns true if action was successfull.
        /// API Path:  api/level/update
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
       
        [HttpPut("update")]
        [Authorize(Policy = "ORG_HR_UP")]
        public async Task<bool> Update([FromBody] Level request)
        {
            var isUpdated = await this.LevelApplication.Update(request);

            return isUpdated;
        }
        #endregion
    }
}
