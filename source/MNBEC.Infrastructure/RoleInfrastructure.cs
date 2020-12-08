using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// RoleInfrastructure inherits from BaseDataAccess and implements IRoleInfrastructure. It performs all required CRUD operations on applicationrole Entity on database.
    /// </summary>
    public class RoleInfrastructure : BaseSQLInfrastructure, IRoleInfrastructure, IRoleStore<ApplicationRole>
    {
        #region Constructor
        /// <summary>
        ///  RoleInfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public RoleInfrastructure(IConfiguration configuration, ILogger<RoleInfrastructure> logger) : base(configuration, logger)
        {
        }
        #endregion

        #region Constants
        private const string AddStoredProcedureName = "ApplicationRoleAdd";
        private const string ActivateStoredProcedureName = "ApplicationRoleActivate";
        private const string GetStoredProcedureName = "ApplicationRoleGet";
        private const string GetAllStoredProcedureName = "ApplicationRoleGetAll";
        private const string GetListStoredProcedureName = "ApplicationRoleGetList";
        private const string UpdateStoredProcedureName = "ApplicationRoleUpdate";
        private const string RoleClaimUpdateStoredProcedureName = "ApplicationRoleClaimUpdate";
        private const string GetbyRoleNameStoredProcedureName = "ApplicationRoleGetbyName";
        //private const string GetUserByIdStoredProcedureName = "GetUserById";

        internal const string RoleIdColumnName = "RoleId";
        internal const string RoleNameColumnName = "RoleName";
        internal const string RoleNameTranslationColumnName = "RoleNameTranslation";
        internal const string RoleNameCodeColumnName = "RoleNameCode";
        internal const string NormalizedNameRoleNameColumnName = "NormalizedNameRoleName";

        internal const string RoleIdParameterName = "PRoleId";
        internal const string RoleNameParameterName = "PRoleName";
        internal const string RoleNameTranslationParameterName = "PRoleNameTranslation";
        internal const string RoleCodeParameterName = "PRoleCode";


        #endregion

        #region Interface IRoleStore Implementation
        public async Task<IdentityResult> CreateAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var returnValue = await this.Add(applicationRole);

            return returnValue > 0 ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var returnValue = await this.Update(applicationRole);

            return returnValue ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var returnValue = await this.Activate(role);

            return returnValue ? IdentityResult.Success : IdentityResult.Failed();
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleId.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleName);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.RoleName = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.RoleName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await this.Get(new ApplicationRole { RoleId = Convert.ToUInt32(roleId) });
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationRole modelItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(RoleInfrastructure.RoleNameParameterName, normalizedRoleName),
                //base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, model.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, RoleInfrastructure.GetbyRoleNameStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        modelItem = new ApplicationRole
                        {
                            RoleId = dataReader.GetUnsignedIntegerValue(RoleInfrastructure.RoleIdColumnName),
                            RoleName = dataReader.GetStringValue(RoleInfrastructure.RoleNameColumnName),
                            RoleNameTranslation = dataReader.GetStringValue(RoleInfrastructure.RoleNameTranslationColumnName),
                            //RoleNameCode = dataReader.GetStringValue(RoleInfrastructure.RoleNameCodeColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName)
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return modelItem;
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
        #endregion

        #region Interface Iapplicationrolefrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        /// 
        public async Task<uint> Add(ApplicationRole applicationRole)
        {
            var applicatioRoleIdParamter = base.GetParameterOut(RoleInfrastructure.RoleIdParameterName, SqlDbType.Int, applicationRole.RoleId);
            var parameters = new List<DbParameter>
            {
                applicatioRoleIdParamter,
                base.GetParameter(RoleInfrastructure.RoleNameParameterName, applicationRole.RoleName),
                base.GetParameter(RoleInfrastructure.RoleNameTranslationParameterName, applicationRole.RoleNameTranslation),
                base.GetParameter(RoleInfrastructure.RoleCodeParameterName, applicationRole.RoleNameCode),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CreatedById),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, applicationRole.Active)
            };

            await base.ExecuteNonQuery(parameters, RoleInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            applicationRole.RoleId = Convert.ToUInt32(applicatioRoleIdParamter.Value);

            return applicationRole.RoleId;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<bool> Activate(ApplicationRole applicationRole)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(RoleInfrastructure.RoleIdParameterName, applicationRole.RoleId),
                base.GetParameter(RoleInfrastructure.ActiveParameterName, applicationRole.Active),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CreatedById)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, RoleInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<ApplicationRole> Get(ApplicationRole applicationRole)
        {
            ApplicationRole applicationRoleItem = null;
            ApplicationClaimGroup applicationClaimGroupItem = null;
            ApplicationClaimGroup currentGroup = null;
            ApplicationClaim applicationClaimItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(RoleInfrastructure.RoleIdParameterName, applicationRole.RoleId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, RoleInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        applicationRoleItem = new ApplicationRole
                        {
                            RoleId = dataReader.GetUnsignedIntegerValue(RoleInfrastructure.RoleIdColumnName),
                            RoleName = dataReader.GetStringValue(RoleInfrastructure.RoleNameColumnName),
                            RoleNameTranslation = dataReader.GetStringValue(RoleInfrastructure.RoleNameTranslationColumnName),
                            RoleNameCode = dataReader.GetStringValue(RoleInfrastructure.RoleNameCodeColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName),
                            ClaimGroups = new List<ApplicationClaimGroup>()
                        };

                        if (dataReader.NextResult())
                        {
                            while (dataReader.Read())
                            {
                                applicationClaimGroupItem = new ApplicationClaimGroup
                                {
                                    ClaimGroupId = dataReader.GetUnsignedIntegerValue(ClaimGroupInfrastructure.ClaimGroupIdColumnName),
                                    ClaimGroupLabel = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupLabelColumnName),
                                    ClaimGroupLabelTranslation = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupLabelTranslationColumnName),
                                    ClaimGroupCode = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupCodeColumnName),
                                    Claims = new List<ApplicationClaim>()
                                };

                                applicationRoleItem.ClaimGroups.Add(applicationClaimGroupItem);
                            }

                            if (dataReader.NextResult())
                            {
                                while (dataReader.Read())
                                {
                                    applicationClaimItem = new ApplicationClaim
                                    {
                                        ClaimId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimIdColumnName),
                                        ClaimGroupId = dataReader.GetUnsignedIntegerValue(ClaimInfrastructure.ClaimGroupIdColumnName),
                                        ClaimType = dataReader.GetStringValue(ClaimInfrastructure.ClaimTypeColumnName),
                                        ClaimLabel = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelColumnName),
                                        ClaimLabelTranslation = dataReader.GetStringValue(ClaimInfrastructure.ClaimLabelTranslationColumnName),
                                        ClaimCode = dataReader.GetStringValue(ClaimInfrastructure.ClaimCodeColumnName),
                                        Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName)
                                    };

                                    if (currentGroup == null || currentGroup.ClaimGroupId != applicationClaimItem.ClaimGroupId)
                                    {
                                        currentGroup = applicationRoleItem.ClaimGroups.FirstOrDefault(c => c.ClaimGroupId == applicationClaimItem.ClaimGroupId);
                                    }

                                    currentGroup?.Claims.Add(applicationClaimItem);
                                }
                            }
                        }
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }

                return applicationRoleItem;
            }
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationrole"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationRole>> GetAll(AllRequest<ApplicationRole> applicationrole)
        {
            var result = new AllResponse<ApplicationRole>
            {
                Data = new List<ApplicationRole>(),
                Offset = applicationrole.Offset,
                PageSize = applicationrole.PageSize,
                SortColumn = applicationrole.SortColumn,
                SortAscending = applicationrole.SortAscending
            };

            ApplicationRole applicationroleItem = null;
            var totalRecordParamter = base.GetParameterOut(BaseSQLInfrastructure.TotalRecordParameterName, SqlDbType.Int, result.TotalRecord);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(BaseSQLInfrastructure.OffsetParameterName, applicationrole.Offset),
                base.GetParameter(BaseSQLInfrastructure.PageSizeParameterName, applicationrole.PageSize),
                base.GetParameter(BaseSQLInfrastructure.SortColumnParameterName, applicationrole.SortColumn),
                base.GetParameter(BaseSQLInfrastructure.SortAscendingParameterName, applicationrole.SortAscending),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationrole.Data.CreatedById)
            };
            //TODO: Add other parameters.

            using (var dataReader = await base.ExecuteReader(parameters, RoleInfrastructure.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        applicationroleItem = new ApplicationRole
                        {
                            RoleId = dataReader.GetUnsignedIntegerValue(RoleInfrastructure.RoleIdColumnName),
                            RoleName = dataReader.GetStringValue(RoleInfrastructure.RoleNameColumnName),
                            RoleNameTranslation = dataReader.GetStringValue(RoleInfrastructure.RoleNameTranslationColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName)
                        };
                        //TODO: Add other Columns.

                        result.Data.Add(applicationroleItem);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                    result.TotalRecord = Convert.ToUInt32(totalRecordParamter.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// GetList fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<List<ApplicationRole>> GetList(ApplicationRole applicationRole)
        {
            var roles = new List<ApplicationRole>();
            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, RoleInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        var modelItem = new ApplicationRole
                        {
                            RoleId = dataReader.GetUnsignedIntegerValue(RoleInfrastructure.RoleIdColumnName),
                            RoleName = dataReader.GetStringValue(RoleInfrastructure.RoleNameColumnName),
                            RoleNameTranslation = dataReader.GetStringValue(RoleInfrastructure.RoleNameTranslationColumnName),
                            RoleNameCode = dataReader.GetStringValue(RoleInfrastructure.RoleNameCodeColumnName)

                            //,NormalizedRoleName = dataReader.GetStringValue(RoleInfrastructure.NormalizedNameRoleNameColumnName)
                        };

                        roles.Add(modelItem);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return roles;
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationrole"></param>
        /// <returns></returns>
        public async Task<bool> Update(ApplicationRole applicationRole)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(RoleInfrastructure.RoleIdParameterName, applicationRole.RoleId),
                base.GetParameter(RoleInfrastructure.RoleNameParameterName, applicationRole.RoleName),
                base.GetParameter(RoleInfrastructure.RoleCodeParameterName, applicationRole.RoleNameCode),
                base.GetParameter(RoleInfrastructure.RoleNameTranslationParameterName, applicationRole.RoleNameTranslation),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, applicationRole.Active),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CreatedById)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, RoleInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            // todo: fix with bulk insert
            foreach (var claimGroup in applicationRole.ClaimGroups)
            {
                foreach (var claim in claimGroup.Claims)
                {
                    var parametersRoleClaim = new List<DbParameter>
                    {
                        base.GetParameter(RoleInfrastructure.RoleIdParameterName, applicationRole.RoleId),
                        base.GetParameter(ClaimInfrastructure.ClaimIdParameterName, claim.ClaimId),
                        base.GetParameter(ClaimInfrastructure.ClaimTypeParameterName, claim.ClaimType),
                        base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, claim.Active),
                        base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationRole.CreatedById)
                    };

                    var returnValueClaim = await base.ExecuteNonQuery(parametersRoleClaim, RoleInfrastructure.RoleClaimUpdateStoredProcedureName, CommandType.StoredProcedure);
                }
            }

            return returnValue > 0;
        }


        #endregion
    }
}
