using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MNBEC.Core.Extensions;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
//using System.IdentityModel.Tokens.Jwt;

namespace MNBEC.API.Core.Middleware
{
    /// <summary>
    /// TokenValidatorMiddleware is used to parse token values before requested controller has been invoked.
    /// </summary>
    public class TokenValidatorMiddleware : BaseMiddleware
    {
        #region Constructor
        /// <summary>
        /// TokenValidatorMiddleware initializes class object.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public TokenValidatorMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<HeaderValueMiddleware> logger, IUserActivityApplication UserActivityApplication) : base(next, configuration, logger)
        {
            this.UserActivityApplication = UserActivityApplication;
        }
        #endregion

        #region Properties and Data Members
        IUserActivityApplication UserActivityApplication { get; }
        public object Request { get; private set; }

        public const string UserId = "UserId";

        public const string ApplicationJsonDataType = "application/json";

        #endregion

        #region Methods
        /// <summary>
        /// Invoke method is called when middleware has been called.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"];
            if (authorizationHeader.Count != 0)
            {
                var userId = this.GetUserIdFromToken(authorizationHeader[0]);
                var request = context.Request;

                if (userId != 0)
                {
                    if (request.Method == "GET")
                    {
                        request.QueryString = request.QueryString.Add("CurrentUserId", userId.ToString());
                    }
                    else
                    {

                        try
                        {
                            var stream = request.Body;
                            var originalBody = new StreamReader(stream).ReadToEnd();

                            var dataSource = JsonConvert.DeserializeObject<dynamic>(originalBody);

                            if (dataSource != null)
                            {
                                dataSource.CurrentUserId = userId;
                                var dataSourcejson = JsonConvert.SerializeObject(dataSource);
                                var requestContent = new StringContent(dataSourcejson, Encoding.UTF8, ApplicationJsonDataType);
                                //Modified stream
                                stream = await requestContent.ReadAsStreamAsync();
                            }
                            request.Body = stream;
                        }
                        catch
                        {

                        }
                        // Holds the original stream   

                    }

                    await Task.Factory.StartNew(() => TrackActivity(context, userId));

                }
            }
            await this.Next(context);
        }

        private async Task TrackActivity(HttpContext context, uint userId)
        {
            var data = context.Request.Path.ToString();

            string[] words = data.Split("/");
            if (words != null || words.Count() > 0)
            {
                UserActivity userActivity = new UserActivity();
                userActivity.UserId = userId;
                userActivity.UserActivityName = words[2] + "-" + words[3];
                userActivity.UserActivityDescription = "You performed " + words[3] + " " + "operation in " + words[2];
                await this.UserActivityApplication.Add(userActivity);
            }
        }

        /// <summary>
        /// GetUserIdFromToken returns the User Id from the token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private uint GetUserIdFromToken(string token)
        {
            var userId = string.Empty;

            try
            {

                token = token.Replace("bearer", "").Replace("Bearer", "").Replace(" ", "");

                if (!token.IsValidJWT())
                    return 0;

                var decodedToken = new JwtSecurityToken(token);

                userId = decodedToken.Claims.FirstOrDefault(claim => claim.Type == UserId).Value;

                #region old code -- left for quick fixes || performance improvement
                
                //var key = Encoding.ASCII.GetBytes(Configuration["JWT:Encryptionkey"]);
                //var handler = new JwtSecurityTokenHandler();
                //var validations = new TokenValidationParameters
                //{
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = new SymmetricSecurityKey(key),
                //    ValidateIssuer = false,
                //    ValidateAudience = false
                //};
                //SecurityToken securityToken;
                //ClaimsPrincipal Claims;
                //var userId = string.Empty;

                //Claims = handler.ValidateToken(token.Trim(), validations, out securityToken);
                //userId = Claims.FindFirst(claim => claim.Type == UserId).Value;
                
                #endregion
            }
            catch (Exception ex)
            {
                // exception is logged when token is expired and we are unable to extract user id 
                // this is not a problem
                base.Logger?.LogError(ex, "custom Exception Message: [Failed in TokenValidatorMiddleware.GetUserIdFromToken] " + "Exception Message: " + ex.Message);
                return 0;
            }
            return Convert.ToUInt32(userId);
        }

        #endregion
    }
}