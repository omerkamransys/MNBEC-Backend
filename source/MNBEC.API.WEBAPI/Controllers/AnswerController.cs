using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.API.Core.Controllers;
using MNBEC.ApplicationInterface;
using MNBEC.Core.Interface;
using MNBEC.Domain;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;

namespace MNBEC.API.WEBAPI.Controllers
{
    /// <summary>
    ///AnswerController is controller class and inherits APIBaseController. It provides APIs for Question.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnswerController : APIBaseController
    {

        #region Constructor
        /// <summary>
        /// AnswerTemplate initializes class object.
        /// </summary>
        /// <param name="answerApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AnswerController(IAnswerApplication answerApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<Answer> logger) : base(headerValue, configuration, logger)
        {
            this.AnswerApplication = answerApplication;
        }
        #endregion

        #region Properties and Data Members
        public IAnswerApplication AnswerApplication { get; }
        #endregion

        #region API Methods

        /// <summary>
        /// Add provides API to add new object in database and returns provided ObjectId.
        /// API Path: api/Answer/add
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<int> Add([FromBody] Answer requestVM)
        {

            var Id = await this.AnswerApplication.Add(requestVM);

            return Id;
        }


        ///// <summary>
        ///// Delete provides API to delete record and returns true if action was successfull.
        ///// API Path: api/Question/activate
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpPut("activate")]
        //[Authorize(Policy = "QUES_ACT")]
        //public async Task<bool> Activate([FromBody] Question requestVM)
        //{

        //    var response = await this.QuestionApplication.Activate(requestVM);

        //    return response;
        //}

        ///// <summary>
        ///// Get provides API to fetch and returns queried item.
        ///// API Path:  api/Question/get
        ///// </summary>
        ///// <param name="requestVM"></param>
        ///// <returns></returns>
        //[HttpGet("get")]
        //[Authorize(Policy = "QUES_GET")]
        //public async Task<Question> Get([FromQuery] Question requestVM)
        //{

        //    Question response = await this.QuestionApplication.Get(requestVM);

        //    return response;
        //}


        /// <summary>
        /// Update provides API to update existing object in database and returns true if action was successfull.
        /// API Path:  api/Answer/update
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AllowAnonymous]
        public async Task<bool> Update([FromBody] Answer requestVM)
        {

            var isUpdated = await this.AnswerApplication.Update(requestVM);

            return isUpdated;
        }


        /// <summary>
        /// GetList provides API to fetch and returns queried list of items.
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("levelTypeLookUpGetList")]
        [AllowAnonymous]
        public async Task<List<LookUpVM>> LevelTypeLookUpGetList()
        {
            List<LookUpVM> response = await this.AnswerApplication.LevelTypeLookUpGetList();

            return response;
        }


        /// <summary>
        /// GetListByStakeholderId provides API to fetch and returns queried list of items.
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getListByStakeholderId")]
        [AllowAnonymous]
        public async Task<List<Answer>> GetListByStakeholderId([FromQuery] StakeholderAnswerRequest requestVM)
        {
            List<Answer> response = await this.AnswerApplication.GetListByStakeholderId(requestVM);
            return response;
        }


        #endregion

    }
}