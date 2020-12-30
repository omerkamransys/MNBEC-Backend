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
    public class ChannelInfrastructure : BaseInfrastructure, IChannelInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  Channelfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ChannelInfrastructure(IConfiguration configuration, ILogger<ChannelInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "ChannelAdd";
        private const string ActivateStoredProcedureName = "ChannelActivate";
        private const string GetStoredProcedureName = "ChannelGet";
        private const string GetAllStoredProcedureName = "ChannelGetAll";
        private const string GetListStoredProcedureName = "ChannelGetList";        
        private const string UpdateStoredProcedureName = "ChannelUpdate";


        private const string ChannelIdColumnName = "ChannelId";
        private const string ClientIdColumnName = "ClientId";
        private const string ChannelNameColumnName = "ChannelName";
        private const string DescriptionColumnName = "Description";
        private const string ExternalChannelIdColumnName = "ExternalChannelId";


        private const string ChannelIdParameterName = "PChannelId";
        private const string ChannelNameParameterName = "PChannelName";
        private const string DescriptionParameterName = "PDescription";
        private const string ClientIdParameterName = "PClientId";



        #endregion

        #region Interface IChannelfrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<int> Add(Channel Channel)
        {
            var ChannelIdParamter = base.GetParameterOut(ChannelInfrastructure.ChannelIdParameterName, SqlDbType.Int, Channel.ChannelId);
            var parameters = new List<DbParameter>
            {
                ChannelIdParamter,
                base.GetParameter(ChannelInfrastructure.ClientIdParameterName, Channel.ClientId),
                base.GetParameter(ChannelInfrastructure.ChannelNameParameterName, Channel.ChannelName),
                base.GetParameter(ChannelInfrastructure.DescriptionParameterName, Channel.Description),                
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, Channel.CurrentUserId),
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, ChannelInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            Channel.ChannelId = Convert.ToInt32(ChannelIdParamter.Value);

            return Channel.ChannelId;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Channel Channel)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ChannelInfrastructure.ChannelIdParameterName, Channel.ChannelId),
                base.GetParameter(BaseInfrastructure.ActiveParameterName, Channel.Active),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, Channel.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, ChannelInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        
        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<Channel> Get(Channel Channel)
        {
            Channel ChannelItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ChannelInfrastructure.ChannelIdParameterName, Channel.ChannelId),               
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, Channel.CurrentUserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ChannelInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        ChannelItem = new Channel
                        {
                            ChannelId = dataReader.GetUnsignedIntegerValue(ChannelInfrastructure.ChannelIdColumnName),
                            ChannelName = dataReader.GetStringValue(ChannelInfrastructure.ChannelNameColumnName),
                            Description = dataReader.GetStringValue(ChannelInfrastructure.DescriptionColumnName),                            
                            CurrentUserId = dataReader.GetUnsignedIntegerValueNullable(BaseInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValueNullable(BaseInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseInfrastructure.ModifiedDateColumnName),
                            Active = dataReader.GetBooleanValue(ChannelInfrastructure.ActiveColumnName)
                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return ChannelItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<AllResponse<Channel>> GetAll(AllRequest<Channel> Channel)
        {
            var result = new AllResponse<Channel>
            {
                Data = new List<Channel>(),
                Offset = Channel.Offset,
                PageSize = Channel.PageSize,
                SortColumn = Channel.SortColumn,
                SortAscending = Channel.SortAscending
            };
           

            var totalRecordParamter = base.GetParameterOut(BaseInfrastructure.TotalRecordParameterName, SqlDbType.Int, result.TotalRecord);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(ChannelInfrastructure.ClientIdParameterName, Channel.Data.ClientId),
                base.GetParameter(BaseInfrastructure.OffsetParameterName, Channel.Offset),
                base.GetParameter(BaseInfrastructure.PageSizeParameterName, Channel.PageSize),
                base.GetParameter(BaseInfrastructure.SortColumnParameterName, Channel.SortColumn),
                base.GetParameter(BaseInfrastructure.SortAscendingParameterName, Channel.SortAscending),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, Channel.Data.CurrentUserId)
            };


            using (var dataReader = await base.ExecuteReader(parameters, ChannelInfrastructure.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var ChannelItem = new Channel
                        {
                            ChannelId = dataReader.GetUnsignedIntegerValue(ChannelInfrastructure.ChannelIdColumnName),
                            ClientId = dataReader.GetUnsignedIntegerValue(ChannelInfrastructure.ClientIdColumnName),
                            ChannelName = dataReader.GetStringValue(ChannelInfrastructure.ChannelNameColumnName),
                            Description = dataReader.GetStringValue(ChannelInfrastructure.DescriptionColumnName),
                            Active = dataReader.GetBooleanValue(BaseInfrastructure.ActiveColumnName)                           
                        };


                        result.Data.Add(ChannelItem);
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
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<List<Channel>> GetList(Channel Channel)
        {
            var Channels = new List<Channel>();
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ChannelInfrastructure.ClientIdParameterName, Channel.ClientId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ChannelInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        var ChannelItem = new Channel
                        {
                            ChannelId = dataReader.GetUnsignedIntegerValue(ChannelInfrastructure.ChannelIdColumnName),
                            ClientId = dataReader.GetUnsignedIntegerValue(ChannelInfrastructure.ClientIdColumnName),
                            ChannelName = dataReader.GetStringValue(ChannelInfrastructure.ChannelNameColumnName),
                            Description = dataReader.GetStringValue(ChannelInfrastructure.DescriptionColumnName),
                            Active = dataReader.GetBooleanValue(BaseInfrastructure.ActiveColumnName)

                        };

                        Channels.Add(ChannelItem);
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return Channels;
        }
        /// <summary>
        /// Get Channel List base on All Acution , My Auction and My purchases
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public async Task<bool> Update(Channel Channel)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ChannelInfrastructure.ChannelIdParameterName, Channel.ChannelId),
                base.GetParameter(ChannelInfrastructure.ChannelNameParameterName, Channel.ChannelName),
                base.GetParameter(ChannelInfrastructure.DescriptionParameterName, Channel.Description),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, Channel.CurrentUserId),
                base.GetParameter(BaseInfrastructure.ActiveParameterName, Channel.Active)
            };


            var returnValue = await base.ExecuteNonQuery(parameters, ChannelInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        #endregion
    }
}
