using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.API.Core.Controllers;
using MNBEC.ApplicationInterface;
using MNBEC.Core.Interface;
using MNBEC.Domain;
using MNBEC.ViewModel.Account;
using MNBEC.ViewModel.QuestionnaireTemplate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.API.WEBAPI.Controllers
{
    /// <summary>
    /// QuestionnaireTemplateController is controller class and inherits APIBaseController. It provides APIs for QuestionnaireTemplate.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuestionnaireTemplateController : APIBaseController
    {
        #region Constructor
        /// <summary>
        /// QuestionnaireTemplate initializes class object.
        /// </summary>
        /// <param name="questionnaireTemplateApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public QuestionnaireTemplateController(IQuestionnaireTemplateApplication questionnaireTemplateApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<QuestionnaireTemplate> logger) : base(headerValue, configuration, logger)
        {
            this.QuestionnaireTemplateApplication = questionnaireTemplateApplication;
        }
        #endregion

        #region Properties and Data Members
        public IQuestionnaireTemplateApplication QuestionnaireTemplateApplication { get; }
        #endregion

        #region API Methods

        /// <summary>
        /// Add provides API to add new object in database and returns provided ObjectId.
        /// API Path: api/QuestionnaireTemplate/add
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<int> Add([FromBody] QuestionnaireTemplateRequestVM requestVM)
        {
            QuestionnaireTemplate request = new QuestionnaireTemplate
            {
                Title = requestVM.Title,
                CurrentUserId = requestVM.CurrentUserId
            };

            var Id = await this.QuestionnaireTemplateApplication.Add(request);

            return Id;
        }


        /// <summary>
        /// Delete provides API to delete record and returns true if action was successfull.
        /// API Path: api/QuestionnaireTemplate/activate
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("activate")]
        [AllowAnonymous]
        public async Task<bool> Activate([FromBody] QuestionnaireTemplate requestVM)
        {
            
            var response = await this.QuestionnaireTemplateApplication.Activate(requestVM);

            return response;
        }

        /// <summary>
        /// Get provides API to fetch and returns queried item.
        /// API Path:  api/QuestionnaireTemplate/get
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [AllowAnonymous]
        public async Task<QuestionnaireTemplate> Get([FromQuery] QuestionnaireTemplate requestVM)
        {

            QuestionnaireTemplate response = await this.QuestionnaireTemplateApplication.Get(requestVM);

            return response;
        }



        /// <summary>
        /// GetAll provides API to fetch and returns queried list of items.
        /// API Path:  api/QuestionnaireTemplate/getlist
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        [AllowAnonymous]
        public async Task<List<QuestionnaireTemplate>> GetList([FromQuery] QuestionnaireTemplate requestVM)
        { 
            List<QuestionnaireTemplate> response = await this.QuestionnaireTemplateApplication.GetList(requestVM);

            return response;
        }

        /// <summary>
        /// Update provides API to update existing object in database and returns true if action was successfull.
        /// API Path:  api/QuestionnaireTemplate/update
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AllowAnonymous]
        public async Task<bool> Update([FromBody] QuestionnaireTemplateRequestVM requestVM)
        {
            QuestionnaireTemplate request = new QuestionnaireTemplate
            {
                Id = requestVM.Id,
                Title = requestVM.Title,
                CurrentUserId = requestVM.CurrentUserId
            };

            var isUpdated = await this.QuestionnaireTemplateApplication.Update(request);

            return isUpdated;
        }
        #endregion

    }
}