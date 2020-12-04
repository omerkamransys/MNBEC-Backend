using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MNBEC.Core.Extensions;
using MNBEC.Core.Interface;
using MNBEC.ViewModel.Common;

namespace MNBEC.API.Core.Controllers
{
    /// <summary>
    /// APIBaseController class inhertits ControllerBase s serves as base class for all controllers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        #region Constructor
        /// <summary>
        /// APIBaseController initializes class object.
        /// </summary>
        /// <param name="headerValue"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public APIBaseController(IHeaderValue headerValue, IConfiguration configuration, ILogger logger)
        {
            this.HeaderValue = headerValue;
            this.Configuration = configuration;
            this.Logger = logger;

            this.Logger?.LogEnterConstructor(this.GetType(), this.HeaderValue);

            this.UseDefaultLanguage = this.HeaderValue != null && this.HeaderValue.AcceptLangauage == this.Configuration["General:SecondaryLanguage"] ? false : true;
        }
        #endregion

        #region Properties and Data Members
        protected IHeaderValue HeaderValue { get; }
        public IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        protected bool UseDefaultLanguage { get; }
        #endregion

        #region API Methods

        /// <summary>
        /// Heartbeatprovides API to return Test Message.
        /// API Path:  api/ControllerName/Heartbeat
        /// </summary>
        /// <param name="requestVm"></param>
        /// <returns></returns>
        [HttpGet("Heartbeat")]
        [AllowAnonymous]
        public string Heartbeat([FromQuery] BaseRequestVM requestVm)
        {
            return "Hello Test at: " + DateTime.Now;
        }

        #endregion

        #region Common Factory Methods
        protected void LogMessage(string className, string methodName, string message)
        {
             var logMessage = $"LOG: {message} in {className}.{methodName}. ";

            Logger.LogError(logMessage);
        }

        #endregion
       
        #region SignalR Group Names
        public const string DealerGroupName = "DealerGroup";
        public const string AdminGroupName = "AdminGroup";
        #endregion
    }
}