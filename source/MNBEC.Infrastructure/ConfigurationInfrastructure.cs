using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// ConfigurationInfrastructure inherits from BaseDataAccess and implements IConfigurationInfrastructure. It performs all required CRUD operations on Configuration Entity on database.
    /// </summary>
    public class ConfigurationInfrastructure : BaseInfrastructure, IConfigurationInfrastructure
    {
        #region Constructor
        /// <summary>
        /// Configurationfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ConfigurationInfrastructure(IConfiguration configuration, ILogger<ConfigurationInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "ConfigurationAdd";
        private const string ActivateStoredProcedureName = "ConfigurationActivate";
        private const string GetStoredProcedureName = "ConfigurationGet";
        private const string GetAllStoredProcedureName = "ConfigurationGetAll";
        private const string GetListStoredProcedureName = "ConfigurationGetList";
        private const string UpdateStoredProcedureName = "ConfigurationUpdate";

        private const string ConfigurationIdColumnName = "ConfigurationId";
        private const string ConfigurationKeyColumnName = "ConfigurationKey";
        private const string ConfigurationValueColumnName = "ConfigurationValue";
        private const string ConfigurationValueTranslationColumnName = "ConfigurationValueTranslation";

        private const string ConfigurationIdParameterName = "PConfigurationId";
        private const string ConfigurationKeyParameterName = "PConfigurationKey";
        private const string ConfigurationValueParameterName = "PConfigurationValue";
        #endregion

        #region Interface IConfigurationfrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<int> Add(Configuration configuration)
        {
            var configurationIdParamter = base.GetParameterOut(ConfigurationInfrastructure.ConfigurationIdColumnName, SqlDbType.Int, configuration.ConfigurationId);
            var parameters = new List<DbParameter>
            {
                configurationIdParamter,
                base.GetParameter(ConfigurationInfrastructure.ConfigurationKeyParameterName, configuration.ConfigurationKey),
                base.GetParameter(ConfigurationInfrastructure.ConfigurationValueParameterName, configuration.ConfigurationValue),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, configuration.CreatedById)
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, ConfigurationInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            configuration.ConfigurationId = Convert.ToInt32(configurationIdParamter.Value);

            return configuration.ConfigurationId;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Configuration configuration)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ConfigurationInfrastructure.ConfigurationIdParameterName, configuration.ConfigurationId),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, configuration.CreatedById)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, ConfigurationInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<Configuration> Get(Configuration configuration)
        {
            Configuration ConfigurationItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ConfigurationInfrastructure.ConfigurationIdParameterName, configuration.ConfigurationId),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, configuration.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ConfigurationInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        ConfigurationItem = new Configuration
                        {
                            ConfigurationId = dataReader.GetUnsignedIntegerValue(ConfigurationInfrastructure.ConfigurationIdColumnName),
                            ConfigurationKey = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationKeyColumnName),
                            ConfigurationValue = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationValueColumnName),
                            ConfigurationValueTranslation = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationValueTranslationColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValueNullable(BaseInfrastructure.CreatedByIdColumnName),
                            CreatedByName = dataReader.GetStringValue(BaseInfrastructure.CreatedByNameColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValueNullable(BaseInfrastructure.ModifiedByIdColumnName),
                            ModifiedByName = dataReader.GetStringValue(BaseInfrastructure.ModifiedByNameColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseInfrastructure.ModifiedDateColumnName)
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return ConfigurationItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<AllResponse<Configuration>> GetAll(AllRequest<Configuration> configuration)
        {
            var result = new AllResponse<Configuration>
            {
                Data = new List<Configuration>(),
                Offset = configuration.Offset,
                PageSize = configuration.PageSize,
                SortColumn = configuration.SortColumn,
                SortAscending = configuration.SortAscending
            };

            Configuration configurationItem = null;
            var totalRecordParamter = base.GetParameterOut(BaseInfrastructure.TotalRecordParameterName, SqlDbType.Int, result.TotalRecord);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(BaseInfrastructure.OffsetParameterName, configuration.Offset),
                base.GetParameter(BaseInfrastructure.PageSizeParameterName, configuration.PageSize),
                base.GetParameter(BaseInfrastructure.SortColumnParameterName, configuration.SortColumn),
                base.GetParameter(BaseInfrastructure.SortAscendingParameterName, configuration.SortAscending),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, configuration.Data.CreatedById)
            };
            //TODO: Add other parameters.

            using (var dataReader = await base.ExecuteReader(parameters, ConfigurationInfrastructure.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        configurationItem = new Configuration
                        {
                            ConfigurationId = dataReader.GetUnsignedIntegerValue(ConfigurationInfrastructure.ConfigurationIdColumnName),
                            ConfigurationKey = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationKeyColumnName),
                            ConfigurationValue = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationValueColumnName)
                        };
                        //TODO: Add other Columns.

                        result.Data.Add(configurationItem);
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
        /// GetAll fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<List<Configuration>> GetList(Configuration configuration)
        {
            var configurations = new List<Configuration>();
            Configuration configurationItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, configuration.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ConfigurationInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        configurationItem = new Configuration
                        {
                            ConfigurationId = dataReader.GetUnsignedIntegerValue(ConfigurationInfrastructure.ConfigurationIdColumnName),
                            ConfigurationKey = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationKeyColumnName),
                            ConfigurationValue = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationValueColumnName),
                            ConfigurationValueTranslation = dataReader.GetStringValue(ConfigurationInfrastructure.ConfigurationValueTranslationColumnName)
                        };

                        configurations.Add(configurationItem);
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return configurations;
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<bool> Update(Configuration configuration)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ConfigurationInfrastructure.ConfigurationIdParameterName, configuration.ConfigurationId),
                base.GetParameter(ConfigurationInfrastructure.ConfigurationKeyParameterName, configuration.ConfigurationKey),
                base.GetParameter(ConfigurationInfrastructure.ConfigurationValueParameterName, configuration.ConfigurationValue),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, configuration.CreatedById)
            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, ConfigurationInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        #endregion
    }
}
