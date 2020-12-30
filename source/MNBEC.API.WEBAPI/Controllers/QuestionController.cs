using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.API.Core.Controllers;
using MNBEC.ApplicationInterface;
using MNBEC.Core.Interface;
using MNBEC.Domain;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Question;
using MNBEC.ViewModel.QuestionnaireTemplate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNBEC.API.WEBAPI.Controllers
{
    /// <summary>
    /// QuestionController is controller class and inherits APIBaseController. It provides APIs for Question.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuestionController : APIBaseController
    {
        #region Constructor
        /// <summary>
        /// QuestionnaireTemplate initializes class object.
        /// </summary>
        /// <param name="questionnaireTemplateApplication"></param>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public QuestionController(IQuestionApplication questionApplication, IHeaderValue headerValue, IConfiguration configuration, ILogger<Question> logger) : base(headerValue, configuration, logger)
        {
            this.QuestionApplication = questionApplication;
        }
        #endregion

        #region Properties and Data Members
        public IQuestionApplication QuestionApplication { get; }
        #endregion

        #region API Methods

        /// <summary>
        /// Add provides API to add new object in database and returns provided ObjectId.
        /// API Path: api/Question/add
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<int> Add([FromBody] Question requestVM)
        {

            var Id = await this.QuestionApplication.Add(requestVM);

            return Id;
        }


        /// <summary>
        /// Delete provides API to delete record and returns true if action was successfull.
        /// API Path: api/Question/activate
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("activate")]
        [AllowAnonymous]
        public async Task<bool> Activate([FromBody] Question requestVM)
        {

            var response = await this.QuestionApplication.Activate(requestVM);

            return response;
        }

        /// <summary>
        /// Get provides API to fetch and returns queried item.
        /// API Path:  api/Question/get
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [AllowAnonymous]
        public async Task<Question> Get([FromQuery] Question requestVM)
        {

            Question response = await this.QuestionApplication.Get(requestVM);

            return response;
        }



        /// <summary>
        /// Update provides API to update existing object in database and returns true if action was successfull.
        /// API Path:  api/Question/update
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AllowAnonymous]
        public async Task<bool> Update([FromBody] Question requestVM)
        {

            var isUpdated = await this.QuestionApplication.Update(requestVM);

            return isUpdated;
        }
        /// <summary>
        /// Update provides API to update Order Number existing object in database and returns true if action was successfull.
        /// API Path:  api/Question/updateOrder
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpPut("updateOrder")]
        [AllowAnonymous]
        public async Task<bool> UpdateOrder([FromBody] QuestionOrderRequestVM requestVM)
        {
            var response = await this.QuestionApplication.UpdateOrder(requestVM.questions);
            return response;
        }

        /// <summary>
        /// GetList provides API to fetch and returns queried list of items.
        /// API Path:  api/QuestionnaireTemplate/areaLookUpGetList
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("areaLookUpGetList")]
        [AllowAnonymous]
        public async Task<List<LookUpVM>> AreaLookUpGetList()
        {
            List<LookUpVM> response = await this.QuestionApplication.AreaLookUpGetList();

            return response;
        }

        /// <summary>
        /// GetList provides API to fetch and returns queried list of items.
        /// API Path:  api/QuestionnaireTemplate/fourPLookUpGetList
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("fourPLookUpGetList")]
        [AllowAnonymous]
        public async Task<List<LookUpVM>> FourPLookUpGetList()
        {
            List<LookUpVM> response = await this.QuestionApplication.FourPLookUpGetList();

            return response;
        }

        /// <summary>
        /// GetList provides API to fetch and returns queried list of items.
        /// API Path:  api/QuestionnaireTemplate/responsibleLookUpGetList
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("responsibleLookUpGetList")]
        [AllowAnonymous]
        public async Task<List<LookUpVM>> ResponsibleLookUpGetList()
        {
            List<LookUpVM> response = await this.QuestionApplication.ResponsibleLookUpGetList();

            return response;
        }

        /// <summary>
        /// GetList provides API to fetch and returns queried list of items.
        /// API Path:  api/QuestionnaireTemplate/levelLookUpGetList
        /// </summary>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        [HttpGet("levelLookUpGetList")]
        [AllowAnonymous]
        public async Task<List<LookUpVM>> LevelLookUpGetList()
        {
            List<LookUpVM> response = await this.QuestionApplication.LevelLookUpGetList();

            return response;
        }
        #endregion

    }
}