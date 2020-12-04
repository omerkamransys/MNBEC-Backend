using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MNBEC.API.Core.Extensions;

namespace MNBEC.API.Account
{
    /// <summary>
    /// Program class is starting point of application.
    /// </summary>
    public class Program
    {
        #region Methods
        /// <summary>
        /// Main is starting point for application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateWebHostBuilder configure and initialize Web Host.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging()
                .UseStartup<Startup>();

        #endregion
    }
}
