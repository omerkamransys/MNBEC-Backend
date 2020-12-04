using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using MNBEC.Core.Extensions;

namespace MNBEC.ServiceProvider
{
    /// <summary>
    /// BaseServiceProvider class serves as base class for all Service Connectors.
    /// </summary>
    public class BaseServiceProvider
    {
        #region Constructor
        /// <summary>
        /// APIBaseController initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public BaseServiceProvider(IConfiguration configuration, ILogger logger)
        {
            this.Configuration = configuration;
            this.Logger = logger;

            this.Logger?.LogEnterConstructor(this.GetType());
        }

        #endregion

        #region Properties and Data Members
        public IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        #endregion

        /// <summary>
        /// GetException returns the excpetion message with fully qualified className, methodName and exception message
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public Exception GetException(string className, string methodName, Exception exception)
        {
            var message = $"Failed in {className}.{methodName}. {exception.Message}";
            var baseException = new Exception(message);
            return baseException;
        }

    }
}