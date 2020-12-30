using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// ClaimInfrastructure inherits from BaseDataAccess and implements IClaimInfrastructure. It performs all required CRUD operations on ApplicationClaim Entity on database.
    /// </summary>
    public class ClaimInfrastructure : BaseSQLInfrastructure, IClaimInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  ApplicationClaimfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ClaimInfrastructure(IConfiguration configuration, ILogger<ClaimInfrastructure> logger) : base(configuration, logger)
        {
        }
        #endregion

        #region Constants
        private const string AddStoredProcedureName = "ApplicationClaimAdd";
        private const string ActivateStoredProcedureName = "ApplicationClaimActivate";
        private const string GetStoredProcedureName = "ApplicationClaimGet";
        private const string GetAllStoredProcedureName = "ApplicationClaimGetAll";
        private const string GetListStoredProcedureName = "ApplicationClaimGetList";
        private const string GetListByRoleStoredProcedureName = "ApplicationClaimGetListByRole";
        private const string GetAllClaimsWithRoleProcedureName = "ApplicationClaimGetAllClaimsWithRole";
        private const string GetListByUserStoredProcedureName = "ApplicationClaimGetListByUser";
        private const string UpdateStoredProcedureName = "ApplicationClaimUpdate";

        internal const string ClaimIdColumnName = "ClaimId";
        internal const string ClaimGroupIdColumnName = "ClaimGroupId";
        internal const string ClaimGroupLabelColumnName = "ClaimGroupLabel";
        internal const string ClaimTypeColumnName = "ClaimType";
        internal const string ClaimLabelTranslationColumnName = "ClaimLabelTranslation";

        internal const string RoleIdColumnName = "RoleId";
        internal const string RoleNameColumnName = "RoleName";
        internal const string RoleNameCodeColumnName = "RoleNameCode";

        internal const string ClaimLabelColumnName = "ClaimLabel";

        internal const string ClaimCodeColumnName = "ClaimCode";

        internal const string ClaimIdParameterName = "PClaimId";
        internal const string ClaimTypeParameterName = "PClaimType";
        internal const string ClaimLabelParameterName = "PClaimLabel";
        internal const string ClaimCodeParameterName = "PClaimCode";
        internal const string ClaimGroupIdParameterName = "PClaimGroupId";
        #endregion

        #region Interface IApplicationClaimfrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<int> Add(ApplicationClaim applicationClaim)
        {
            var claimIdParamter = base.GetParameterOut(ClaimInfrastructure.ClaimIdColumnName, SqlDbType.Int, applicationClaim.ClaimId);
            var parameters = new List<DbParameter>
            {
                claimIdParamter,
                base.GetParameter(ClaimInfrastructure.ClaimGroupIdParameterName, applicationClaim.ClaimGroupId),
                base.GetParameter(ClaimInfrastructure.ClaimTypeParameterName, applicationClaim.ClaimType),
                base.GetParameter(ClaimInfrastructure.ClaimLabelParameterName, applicationClaim.ClaimLabel),
                base.GetParameter(ClaimInfrastructure.ClaimCodeParameterName, applicationClaim.ClaimCode),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaim.CurrentUserId)
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, ClaimInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            applicationClaim.ClaimId = Convert.ToInt32(claimIdParamter.Value);

            return applicationClaim.ClaimId;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<bool> Activate(ApplicationClaim applicationClaim)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ClaimInfrastructure.ClaimIdParameterName, applicationClaim.ClaimId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaim.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, ClaimInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<ApplicationClaim> Get(ApplicationClaim applicationClaim)
        {
            ApplicationClaim ApplicationClaimItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ClaimInfrastructure.ClaimIdParameterName, applicationClaim.ClaimId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaim.CurrentUserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ClaimInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        ApplicationClaimItem = new ApplicationClaim
                        {
                            ClaimId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimIdColumnName),
                            ClaimType = dataReader.GetStringValue(ClaimInfrastructure.ClaimTypeColumnName),
                            ClaimLabelTranslation = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelTranslationColumnName),
                            ClaimCode = dataReader.GetStringValue(ClaimInfrastructure.ClaimCodeColumnName),
                            CurrentUserId = dataReader.GetUnsignedIntegerValueNullable(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedByName = dataReader.GetStringValue(BaseSQLInfrastructure.CreatedByNameColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValueNullable(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedByName = dataReader.GetStringValue(BaseSQLInfrastructure.ModifiedByNameColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName)
                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return ApplicationClaimItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationClaim>> GetAll(AllRequest<ApplicationClaim> applicationClaim)
        {
            var result = new AllResponse<ApplicationClaim>
            {
                Data = new List<ApplicationClaim>(),
                Offset = applicationClaim.Offset,
                PageSize = applicationClaim.PageSize,
                SortColumn = applicationClaim.SortColumn,
                SortAscending = applicationClaim.SortAscending
            };

            ApplicationClaim applicationClaimItem = null;
            var totalRecordParamter = base.GetParameterOut(BaseSQLInfrastructure.TotalRecordParameterName, SqlDbType.Int, result.TotalRecord);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(BaseSQLInfrastructure.OffsetParameterName, applicationClaim.Offset),
                base.GetParameter(BaseSQLInfrastructure.PageSizeParameterName, applicationClaim.PageSize),
                base.GetParameter(BaseSQLInfrastructure.SortColumnParameterName, applicationClaim.SortColumn),
                base.GetParameter(BaseSQLInfrastructure.SortAscendingParameterName, applicationClaim.SortAscending),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaim.Data.CurrentUserId)
            };
            //TODO: Add other parameters.

            using (var dataReader = await base.ExecuteReader(parameters, ClaimInfrastructure.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        applicationClaimItem = new ApplicationClaim
                        {
                            ClaimId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimIdColumnName),
                            ClaimType = dataReader.GetStringValue(ClaimInfrastructure.ClaimTypeColumnName),
                            ClaimLabel = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelColumnName),
                            ClaimLabelTranslation = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelTranslationColumnName)

                        };
                        //TODO: Add other Columns.

                        result.Data.Add(applicationClaimItem);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                    result.TotalRecord = Convert.ToInt32(totalRecordParamter.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// GetList fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaim>> GetList(ApplicationClaim applicationClaim)
        {
            var applicationClaims = new List<ApplicationClaim>();
            ApplicationClaim applicationClaimItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaim.CurrentUserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ClaimInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        applicationClaimItem = new ApplicationClaim
                        {
                            ClaimId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimIdColumnName),
                            //ClaimGroupId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimGroupIdColumnName),
                            ClaimType = dataReader.GetStringValue(ClaimInfrastructure.ClaimTypeColumnName),
                            //ClaimLabelTranslation = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelTranslationColumnName),
                            //ClaimLabel = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelColumnName),
                            ClaimCode = dataReader.GetStringValue(ClaimInfrastructure.ClaimCodeColumnName)
                        };

                        applicationClaims.Add(applicationClaimItem);
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationClaims;
        }

        /// <summary>
        /// GetListByRole fetch and returns queried list of items with specific fields from database by role.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaim>> GetListByRole(ApplicationRole applicationRole)
        {
            var applicationClaims = new List<ApplicationClaim>();
            ApplicationClaim applicationClaimItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(RoleInfrastructure.RoleIdParameterName, applicationRole.RoleId),
                base.GetParameter(RoleInfrastructure.RoleCodeParameterName, applicationRole.RoleNameCode),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CurrentUserId)

            };
            //var parameters = new List<DbParameter>
            //{
            //    base.GetParameter(RoleInfrastructure.RoleIdParameterName, applicationRole.RoleId),
            //    base.GetParameter(RoleInfrastructure.RoleCodeParameterName, applicationRole.RoleNameCode),
            //    base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CreatedById)

            //};

            using (var dataReader = await base.ExecuteReader(parameters, ClaimInfrastructure.GetListByRoleStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        applicationClaimItem = new ApplicationClaim
                        {
                            ClaimId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimIdColumnName),
                            //ClaimGroupId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimGroupIdColumnName),
                            ClaimLabelTranslation = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelTranslationColumnName),
                            ClaimLabel = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelColumnName),
                            ClaimCode = dataReader.GetStringValue(ClaimInfrastructure.ClaimCodeColumnName)
                        };

                        applicationClaims.Add(applicationClaimItem);
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationClaims;
        }


        /// <summary>
        /// GetAllClaimsWithRole fetch and returns queried list of items with specific fields from database by role.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<List<ApplicationRole>> GetAllClaimsWithRole()
        {
            var applicationRoles = new List<ApplicationRole>();
            ApplicationRole applicationRoleItem = null;

            ApplicationRole currentRole = null;

            ApplicationClaim applicationClaim = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, 1)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ClaimInfrastructure.GetAllClaimsWithRoleProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        applicationRoleItem = new ApplicationRole
                        {
                            RoleId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.RoleIdColumnName),
                            RoleName = dataReader.GetStringValue(ClaimInfrastructure.RoleNameColumnName),
                            RoleNameCode = dataReader.GetStringValue(ClaimInfrastructure.RoleNameCodeColumnName)
                        };

                        applicationRoles.Add(applicationRoleItem);
                    }
                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            applicationClaim = new ApplicationClaim();

                            int RoleId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.RoleIdColumnName);

                            applicationClaim.ClaimCode = dataReader.GetStringValue(ClaimInfrastructure.ClaimCodeColumnName);

                            if (currentRole == null || currentRole.RoleId != RoleId)
                            {
                                currentRole = applicationRoles.FirstOrDefault(c => c.RoleId == RoleId);
                            }
                            currentRole?.ApplicationClaims.Add(applicationClaim);
                        }
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationRoles;
        }

        /// <summary>
        /// GetListByUser fetch and returns queried list of items with specific fields from database by user.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaim>> GetListByUser(ApplicationUser applicationUser)
        {
            var applicationClaims = new List<ApplicationClaim>();
            ApplicationClaim applicationClaimItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationUser.UserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ClaimInfrastructure.GetListByUserStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        applicationClaimItem = new ApplicationClaim
                        {
                            ClaimId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimIdColumnName),
                            ClaimType = dataReader.GetStringValue(ClaimInfrastructure.ClaimTypeColumnName),
                            ClaimLabel = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelColumnName),
                            ClaimLabelTranslation = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelTranslationColumnName),
                            ClaimCode = dataReader.GetStringValue(ClaimInfrastructure.ClaimCodeColumnName)
                        };

                        applicationClaims.Add(applicationClaimItem);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationClaims;
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaim"></param>
        /// <returns></returns>
        public async Task<bool> Update(ApplicationClaim applicationClaim)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ClaimInfrastructure.ClaimIdParameterName, applicationClaim.ClaimId),
                base.GetParameter(ClaimInfrastructure.ClaimGroupIdParameterName, applicationClaim.ClaimGroupId),
                base.GetParameter(ClaimInfrastructure.ClaimTypeParameterName, applicationClaim.ClaimType),
                base.GetParameter(ClaimInfrastructure.ClaimLabelParameterName, applicationClaim.ClaimLabel),
                base.GetParameter(ClaimInfrastructure.ClaimCodeParameterName, applicationClaim.ClaimCode),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaim.CurrentUserId)
            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, ClaimInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        #endregion
    }
}
