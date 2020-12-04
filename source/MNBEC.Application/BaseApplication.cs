using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.Core.Extensions;

namespace MNBEC.Application
{
    /// <summary>
    /// BaseApplication is base class for all Application classes.
    /// </summary>
    public abstract class BaseApplication
    {
        #region Constructor
        /// <summary>
        /// BaseApplication initailizes object instance.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public BaseApplication(IConfiguration configuration, ILogger logger)
        {
            this.Configuration = configuration;
            this.Logger = logger;

            this.Logger?.LogEnterConstructor(this.GetType());
        }

        public IConfiguration Configuration { get; }
        public ILogger Logger { get; }
        #endregion
    }
}
