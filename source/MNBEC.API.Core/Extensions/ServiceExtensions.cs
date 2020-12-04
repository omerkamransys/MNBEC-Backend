﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MNBEC.API.Core.Middleware;
using MNBEC.ApplicationInterface;
using MNBEC.Core.Interface;
using MNBEC.Core.Model;
using MNBEC.Domain;
using MNBEC.CacheInterface;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;


namespace MNBEC.API.Core.Extensions
{
    /// <summary>
    /// ServiceExtensions class provides implementation for extension methods.
    /// </summary>
    public static class ServiceExtensions
    {
        private const string SwaggerPath = "/swagger/v1/swagger.json";

        /// <summary>
        /// RegisterCommonService registers common components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommonService(this IServiceCollection services)
        {
            services.AddScoped<IHeaderValue, HeaderValue>();
            services.AddScoped<IUser, User>();

            return services;
        }


        /// <summary>
        /// RegisterCommonService registers common components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterPolicies(this IServiceCollection services, IClaimApplication claimApplication)
        {

            var applicationClaim = new ApplicationClaim();

            services.AddAuthorization(async options =>
            {
                var ClaimList = await claimApplication.GetList(applicationClaim);
                foreach (var item in ClaimList)
                {
                    //Commented this code for Testing in development env:

                    //if (item.ClaimType == "Claim.GetList")
                    //{
                        options.AddPolicy(item.ClaimCode, policy => policy.RequireClaim(item.ClaimCode));

                      
                    //}
                    //else
                    //{
                    //    options.AddPolicy(item.ClaimCode, policy => policy.RequireClaim(item.ClaimCode));
                    //}
                }
            });

            return services;
        }

        /// <summary>
        /// RegisterNotificationTemplateCache registers notification templates in Redis Cache.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
      
      
       


        /// <summary>
        /// RegisterJWTAuthenticationService registers common Authentication for JWT.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterJWTAuthenticationService(this IServiceCollection services, IRoleClaimCache roleClaimCache, IConfiguration configuration)
        {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(cfg =>
             {
                 cfg.RequireHttpsMetadata = false;
                 cfg.SaveToken = true;
                 cfg.TokenValidationParameters = new TokenValidationParameters()
                 {
                     //ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Encryptionkey"])),
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidIssuer = configuration["Jwt:Issuer"],
                     //ValidAudience = Configuration["Jwt:Audience"],
                     //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                 };
                 cfg.Events = new JwtBearerEvents
                 {
                     OnTokenValidated = async ctx =>
                     {
                         //Stopwatch watch = new Stopwatch();
                         //watch.Start();

                         var currentUserRoles = ctx.Principal.Claims.Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).ToList();

                         try
                         {
                             if (currentUserRoles != null && currentUserRoles.Count > 0 )
                             {
                                 HashSet<string> userClaims = new HashSet<string>();

                                 //get all claims with role object here.
                                 Dictionary<string, HashSet<string>> roleClaimsDictionary = await roleClaimCache.GetBulk("RoleClaims");

                                 foreach (var role in currentUserRoles)
                                 {
                                     if (roleClaimsDictionary.ContainsKey(role.Value))
                                     {
                                         userClaims.UnionWith(roleClaimsDictionary[role.Value]);
                                     }
                                 }

                                 var newclaims = new List<Claim>();
                                 foreach (var userClaim in userClaims)
                                 {
                                     newclaims.Add(new Claim(userClaim, string.Empty, ClaimValueTypes.String, issuer: configuration["Jwt:Issuer"], originalIssuer: configuration["Jwt:Issuer"], subject: ctx.Principal.Identities.First()));
                                 }

                                 var appIdentity = new ClaimsIdentity(newclaims);

                                 ctx.Principal.AddIdentity(appIdentity);

                                 //watch.Stop();
                                 //var time = watch.ElapsedMilliseconds;
                             }
                         }
                         catch (Exception ex)
                         {
                             throw new Exception("[Failed in OnTokenValidated for RegisterJWTAuthenticationService] - ex.Message:[" + ex.Message + "] - " + ex.StackTrace);
                         }
                     }
                 };
             });

            return services;
        }

        /// <summary>
        /// UseHeaderValueMiddleware registers HeaderValueMiddleware.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHeaderValueMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HeaderValueMiddleware>();
        }

        /// <summary>
        /// UseExceptionHandlingMiddleware registers ExceptionHandlingMiddleware.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        /// <summary>
        /// Adds a delegate for configuring the provided Microsoft.Extensions.Logging.ILoggingBuilder. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The Microsoft.AspNetCore.Hosting.IWebHostBuilder to configure.</param>
        /// <returns>The Microsoft.AspNetCore.Hosting.IWebHostBuilder.</returns>
        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder hostBuilder)
        {
            //Default settings
            //hostBuilder.ConfigureLogging((hostingContext, logging) =>
            // {
            //     logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            //     logging.AddConsole();
            //     logging.AddDebug();
            // });

            hostBuilder.ConfigureLogging(logging =>
             {
                 logging.ClearProviders();
                 logging.SetMinimumLevel(LogLevel.Error);
             });
            //.UseNLog();

            return hostBuilder;
        }


        /// <summary>
        /// UseCorsMiddleware registers UseCors.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
        {
            return app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
        }

        /// <summary>
        /// UseTokenValidatorMiddleware registers Token Parser
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseTokenValidatorMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenValidatorMiddleware>();
        }

        /// <summary>
        /// UseSwaggerMiddleware registers Swagger Middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, string applicationName)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            return app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(ServiceExtensions.SwaggerPath, applicationName);
            });
        }

        /// <summary>
        /// RegisterSwagger registers Swagger components.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSwagger(this IServiceCollection services, string applicationName)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = applicationName, Version = "v1" });
            });

            return services;
        }
    }
}