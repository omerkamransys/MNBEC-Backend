using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MNBEC.API.Core.Common
{
    /// <summary>
    /// BaseTimerBackgroundTask inherites IHostedService and implement IDisposable. This class is used as base class for Timer Background Tasks.
    /// </summary>
    public abstract class BaseTimerBackgroundTask : IHostedService, IDisposable
    {
        #region Constructor
        /// <summary>
        /// BaseBackgroundTask initializes new instance of BaseBackgroundTask.
        /// </summary>
        /// <param name="logger"></param>
        public BaseTimerBackgroundTask(IConfiguration configuration, ILogger logger)
        {
            this.Logger = logger;
            this.Configuration = configuration;
        }
        #endregion

        #region Properties and Data Members
        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        private Timer Timer { get; set; }
        private bool Processing { get; set; }
        protected int Frequency { get; set; }
        protected TimerCallback ProcessAction { get; set; }
        #endregion

        #region IHostedService implementation
        /// <summary>
        /// StartAsync Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.Logger?.LogInformation($"{this.GetType().FullName} Service is starting.");

            this.Timer = new Timer(this.TimerCallbackHandler, null, 0, this.Frequency);

            return Task.CompletedTask;
        }

        /// <summary>
        /// StopAsync Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.Logger?.LogInformation($"{this.GetType().FullName} Service is stopping.");

            this.Timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Timer?.Dispose();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// TimerCallbackHandler calls AuctionApplication layer for Status updates.
        /// </summary>
        /// <param name="state"></param>
        protected void TimerCallbackHandler(object state)
        {
            try
            {
                if (this.Processing == false)
                {
                    this.Processing = true;

                    this.ProcessAction(state);
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex.Message);
            }
            finally
            {
                this.Processing = false;
            }

        }
        #endregion

        #region Factory Methods

        public void LogMessage(string className, string methodName, string message)
        {
            var logMessage = $"LOG: {message} in {className}.{methodName}. ";

            Logger.LogError(logMessage);
        }

        #endregion

        #region SignalR Group Names
        public const string DealerGroup = "DealerGroup";
        public const string AdminGroup = "AdminGroup";

        #endregion
    }
}
