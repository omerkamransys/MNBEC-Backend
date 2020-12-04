using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using MNBEC.API.Core.Extensions;
using MNBEC.Application.Extensions;
using MNBEC.ApplicationInterface;
using MNBEC.Cache.Extensions;
using MNBEC.CacheInterface;
using MNBEC.Domain;
using MNBEC.Infrastructure.Extensions;
using MNBEC.ServiceConnector.Extensions;
using MNBEC.ServiceProvider;
namespace MNBEC.API.Account
{
    /// <summary>
    /// Startup class is main startup structure for Web Application.
    /// </summary>
    public class Startup
    {
        #region Constructor
        /// <summary>
        /// Startup initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        #endregion

        #region Properties and Data Members
        public IConfiguration Configuration { get; }
        #endregion

        #region Methods
        /// <summary>
        /// ConfigureServices method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //TODO: Place to be externaize.
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddDefaultTokenProviders();

            services.AddCors();

            #region Custom Service Registration
            services.RegisterSwagger(this.GetType().Namespace);
            services.RegisterCommonService();
            services.AddSingleton(c => this.Configuration);



            services.RegisterCommonServiceProvider();
            services.RegisterCommonServiceConnector();

            services.RegisterAccountApplication();
            services.RegisterEmailApplication();
            services.RegisterAccountInfrastructure();


            //BuildServiceProvider to Get DI Service for multiple applications middlewares:
            var serviceProvider = services.BuildServiceProvider();

            var claimApplication = serviceProvider.GetService<IClaimApplication>();

            var logger = serviceProvider.GetService<ILogger<Startup>>();

            var roleClaimCache = serviceProvider.GetService<IRoleClaimCache>();
            services.RegisterJWTAuthenticationService(roleClaimCache, Configuration);

            services.RegisterPolicies(claimApplication);


            #endregion

        }

        /// <summary>
        /// Configure method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            #region Custom Middleware Registration
            app.UseSwaggerMiddleware(this.GetType().Namespace);

            app.UseCorsMiddleware();

            app.UseExceptionHandlingMiddleware();

            app.UseHeaderValueMiddleware();

            app.UseTokenValidatorMiddleware();

            #endregion

            app.UseMvc();
        }
        #endregion
    }
}
