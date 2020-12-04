using MNBEC.Core.Extensions;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MNBEC.Domain;

using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// MakeInfrastructure inherits from BaseInfrastructure and implements IMakefrastructure. It performs all required CRUD operations on Make Entity on database.
    /// </summary>
    public class UserActivityInfrastructure : BaseInfrastructure, IUserActivityInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  UserActivityfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UserActivityInfrastructure(IConfiguration configuration, ILogger<UserActivityInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "UserActivityAdd";
        private const string ActivateStoredProcedureName = "UserActivityActivate";
        private const string GetStoredProcedureName = "UserActivityGet";
        private const string GetAllStoredProcedureName = "UserActivityGetAll";
        private const string GetListStoredProcedureName = "UserActivityGetList";        
        private const string UpdateStoredProcedureName = "UserActivityUpdate";


        private const string UserActivityIdColumnName = "UserActivityId";

        private const string UserIdColumnName = "UserId";
        private const string UserNameColumnName = "UserName";
        private const string UserActivityNameColumnName = "UserActivityName";
        private const string UserActivityDescriptionColumnName = "UserActivityDescription";
       


        private const string UserActivitydParameterName = "PUserActivityId";

        private const string UserIdParameterName = "PUserId";
        private const string TotalRecordsParameterName = "PTotalRecords";
        private const string UserActivityNameParameterName = "PUserActivityName";
        private const string UserActivityDescriptionParameterName = "PUserActivityDescription";



        #endregion

        #region Interface IUserActivityfrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="UserActivity"></param>
        /// <returns></returns>
        public async Task<uint> Add(UserActivity UserActivity)
        {
           
            var parameters = new List<DbParameter>
            {                
                base.GetParameter(UserActivityInfrastructure.UserIdParameterName, UserActivity.UserId),
                base.GetParameter(UserActivityInfrastructure.UserActivityNameParameterName, UserActivity.UserActivityName),
                base.GetParameter(UserActivityInfrastructure.UserActivityDescriptionParameterName, UserActivity.UserActivityDescription)              
               
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, UserActivityInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

          

            return 1;
        }


        public async Task<bool> Activate(UserActivity UserActivity)
        {
            return true;
        }        
        public async Task<UserActivity> Get(UserActivity UserActivity)
        {
            UserActivity UserActivitytem = null;
            

            return UserActivitytem;
        }
        public async Task<AllResponse<UserActivity>> GetAll(AllRequest<UserActivity> UserActivity)
        {
            var result = new AllResponse<UserActivity>
            {
                Data = new List<UserActivity>(),
                Offset = UserActivity.Offset,
                PageSize = UserActivity.PageSize,
                SortColumn = UserActivity.SortColumn,
                SortAscending = UserActivity.SortAscending
            }; 

            return result;
        }
        public async Task<List<UserActivity>> GetList(UserActivity UserActivity)
        {
            var UserActivitys = new List<UserActivity>();
            var item = new UserActivity();

            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserActivityInfrastructure.UserIdParameterName, UserActivity.UserId),
                base.GetParameter(UserActivityInfrastructure.TotalRecordsParameterName, UserActivity.TotalRecords),
              
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserActivityInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        item = new UserActivity
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserActivityInfrastructure.UserIdColumnName),
                            UserActivityName = dataReader.GetStringValue(UserActivityInfrastructure.UserActivityNameColumnName),
                            UserActivityDescription = dataReader.GetStringValue(UserActivityInfrastructure.UserActivityDescriptionColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(UserActivityInfrastructure.CreatedDateColumnName),
                            UserName = dataReader.GetStringValue(UserActivityInfrastructure.UserNameColumnName)
                           
                        };

                        UserActivitys.Add(item);
                    }
                }
            }
            return UserActivitys;
        }
        /// <summary>

        public async Task<bool> Update(UserActivity UserActivity)
        {
            return true;
        }
        #endregion
    }
}
