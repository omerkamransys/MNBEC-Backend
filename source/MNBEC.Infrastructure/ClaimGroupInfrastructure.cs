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
    /// ClaimGroupInfrastructure inherits from BaseDataAccess and implements IClaimGroupInfrastructure. It performs all required CRUD operations on ClaimGroup Entity on database.
    /// </summary>
    public class ClaimGroupInfrastructure : BaseSQLInfrastructure, IClaimGroupInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  ClaimGroupfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ClaimGroupInfrastructure(IConfiguration configuration, ILogger<ClaimGroupInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "ApplicationClaimGroupAdd";
        private const string ActivateStoredProcedureName = "ApplicationClaimGroupActivate";
        private const string GetStoredProcedureName = "ApplicationClaimGroupGet";
        private const string GetAllStoredProcedureName = "ApplicationClaimGroupGetAll";
        private const string GetAllByUserStoredProcedureName = "ApplicationClaimGroupGetAllByUser";
        private const string GetListStoredProcedureName = "ApplicationClaimGroupGetList";
        private const string UpdateStoredProcedureName = "ApplicationClaimGroupUpdate";

        internal const string ClaimGroupIdColumnName = "ClaimGroupId";
        internal const string ClaimGroupLabelColumnName = "ClaimGroupLabel";
        internal const string ClaimGroupLabelTranslationColumnName = "ClaimGroupLabelTranslation";

        
        internal const string ClaimGroupCodeColumnName = "ClaimGroupCode";

        private const string ClaimGroupIdParameterName = "PClaimGroupId";
        private const string ClaimGroupLabelParameterName = "PClaimGroupLabel";
        private const string ClaimGroupCodeParameterName = "PClaimGroupCode";
        #endregion

        #region Interface IClaimGroupfrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<uint> Add(ApplicationClaimGroup applicationClaimGroup)
        {
            var applicationClaimGroupIdParamter = base.GetParameterOut(ClaimGroupInfrastructure.ClaimGroupIdColumnName, SqlDbType.Int, applicationClaimGroup.ClaimGroupId);
            var parameters = new List<DbParameter>
            {
                applicationClaimGroupIdParamter,
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupLabelParameterName, applicationClaimGroup.ClaimGroupLabel),
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupCodeParameterName, applicationClaimGroup.ClaimGroupLabel),
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupCodeParameterName, applicationClaimGroup.ClaimGroupCode),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaimGroup.CreatedById)
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, ClaimGroupInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            applicationClaimGroup.ClaimGroupId = Convert.ToUInt32(applicationClaimGroupIdParamter.Value);

            return applicationClaimGroup.ClaimGroupId;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<bool> Activate(ApplicationClaimGroup applicationClaimGroup)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupIdParameterName, applicationClaimGroup.ClaimGroupId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaimGroup.CreatedById)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, ClaimGroupInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<ApplicationClaimGroup> Get(ApplicationClaimGroup applicationClaimGroup)
        {
            ApplicationClaimGroup ApplicationClaimGroupItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupIdParameterName, applicationClaimGroup.ClaimGroupId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaimGroup.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ClaimGroupInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        ApplicationClaimGroupItem = new ApplicationClaimGroup
                        {
                            ClaimGroupId = dataReader.GetUnsignedIntegerValue(ClaimGroupInfrastructure.ClaimGroupIdColumnName),
                            ClaimGroupLabel = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupLabelColumnName),
                            ClaimGroupLabelTranslation = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupLabelTranslationColumnName),
                            ClaimGroupCode = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupCodeColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValueNullable(BaseSQLInfrastructure.CreatedByIdColumnName),
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

            return ApplicationClaimGroupItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationClaimGroup>> GetAll(AllRequest<ApplicationClaimGroup> applicationClaimGroup)
        {
            return await this.GetAll(applicationClaimGroup, ClaimGroupInfrastructure.GetAllStoredProcedureName);
        }

        /// <summary>
        /// GetAllByUser fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationClaimGroup>> GetAllByUser(AllRequest<ApplicationClaimGroup> applicationClaimGroup)
        {
            return await this.GetAll(applicationClaimGroup, ClaimGroupInfrastructure.GetAllByUserStoredProcedureName);
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<List<ApplicationClaimGroup>> GetList(ApplicationClaimGroup applicationClaimGroup)
        {
            var applicationClaimGroups = new List<ApplicationClaimGroup>();
            ApplicationClaimGroup applicationClaimGroupItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaimGroup.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ClaimGroupInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        applicationClaimGroupItem = new ApplicationClaimGroup
                        {
                            ClaimGroupId = dataReader.GetUnsignedIntegerValue(ClaimGroupInfrastructure.ClaimGroupIdColumnName),
                            ClaimGroupLabel = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupLabelColumnName),
                            ClaimGroupLabelTranslation = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupLabelTranslationColumnName),
                            ClaimGroupCode = dataReader.GetStringValue(ClaimGroupInfrastructure.ClaimGroupCodeColumnName)
                        };

                        applicationClaimGroups.Add(applicationClaimGroupItem);
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationClaimGroups;
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public async Task<bool> Update(ApplicationClaimGroup applicationClaimGroup)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupIdParameterName, applicationClaimGroup.ClaimGroupId),
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupLabelParameterName, applicationClaimGroup.ClaimGroupLabel),
                base.GetParameter(ClaimGroupInfrastructure.ClaimGroupCodeParameterName, applicationClaimGroup.ClaimGroupCode),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaimGroup.CreatedById)
            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, ClaimGroupInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// GetAll executes provided stored procedure with provided parameters.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        private async Task<AllResponse<ApplicationClaimGroup>> GetAll(AllRequest<ApplicationClaimGroup> applicationClaimGroup, string storedProcedureName)
        {

            var result = new AllResponse<ApplicationClaimGroup>
            {
                Data = new List<ApplicationClaimGroup>(),
                Offset = applicationClaimGroup.Offset,
                PageSize = applicationClaimGroup.PageSize,
                SortColumn = applicationClaimGroup.SortColumn,
                SortAscending = applicationClaimGroup.SortAscending
            };

            ApplicationClaimGroup applicationClaimGroupItem = null;
            ApplicationClaimGroup currentGroup = null;
            ApplicationClaim applicationClaimItem = null;
            var totalRecordParamter = base.GetParameterOut(BaseSQLInfrastructure.TotalRecordParameterName, SqlDbType.Int, result.TotalRecord);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(BaseSQLInfrastructure.OffsetParameterName, applicationClaimGroup.Offset),
                base.GetParameter(BaseSQLInfrastructure.PageSizeParameterName, applicationClaimGroup.PageSize),
                base.GetParameter(BaseSQLInfrastructure.SortColumnParameterName, applicationClaimGroup.SortColumn),
                base.GetParameter(BaseSQLInfrastructure.SortAscendingParameterName, applicationClaimGroup.SortAscending),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationClaimGroup.Data.CreatedById)
            };
            //TODO: Add other parameters.

            using (var dataReader = await base.ExecuteReader(parameters, storedProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
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
                        //TODO: Add other Columns.

                        result.Data.Add(applicationClaimGroupItem);
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
                                Active = dataReader.GetBooleanValue(ClaimInfrastructure.ActiveColumnName)
                            };

                            if (currentGroup == null || currentGroup.ClaimGroupId != applicationClaimItem.ClaimGroupId)
                            {
                                currentGroup = result.Data.FirstOrDefault(c => c.ClaimGroupId == applicationClaimItem.ClaimGroupId);
                            }

                            currentGroup?.Claims.Add(applicationClaimItem);
                        }
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
        #endregion
    }
}
