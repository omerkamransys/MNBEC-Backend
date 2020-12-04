﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using MNBEC.Core.ApplicationException;
using MNBEC.Core.Extensions;
using MNBEC.Domain;
using MNBEC.Infrastructure.Extensions;
using System.Reflection;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// BaseInfrastructure is base class for all Infrastructure classes.
    /// </summary>
    public abstract class BaseInfrastructure
    {
        #region Constructor
        /// <summary>
        /// BaseInfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        protected BaseInfrastructure(IConfiguration configuration, ILogger logger)
        {
            this.Configuration = configuration;
            this.ConnectionString = this.Configuration.GetConnectionString("DefaultConnection");
            this.Logger = logger;

            this.Logger?.LogEnterConstructor(this.GetType());
        }
        #endregion

        #region Properties and Data Members
        protected const string CreatedByIdColumnName = "CreatedById";
        protected const string CreatedByNameColumnName = "CreatedByName";
        protected const string CreatedDateColumnName = "CreatedDate";
        protected const string ModifiedByIdColumnName = "ModifiedById";
        protected const string ModifiedByNameColumnName = "ModifiedByName";
        protected const string ModifiedDateColumnName = "ModifiedDate";
        protected const string ActiveColumnName = "Active";
        protected const string ActiveForCustomerColumnName = "ActiveForCustomer";
        protected const string UnReadCountColumnName = "UnReadCount";

        protected const string CurrentUserIdParameterName = "PCurrentUserId";
        protected const string AgentIdParameterName = "PAgentId";

        protected const string IsActiveParameterName = "PActive";
        protected const string OffsetParameterName = "POffset";
        protected const string PageSizeParameterName = "PPageSize";
        protected const string TotalRecordParameterName = "PTotalRecord";
        protected const string SortColumnParameterName = "PSortColumn";
        protected const string SortAscendingParameterName = "PSortAscending";
        protected const string FilterColumnIdParameterName = "PFilterColumnId";
        protected const string ActiveParameterName = "PActive";
        protected const string ActiveForCustomerParameterName = "PActiveForCustomer";
        protected const string NotificationCodeParamaterName = "PNotificationCode";
        protected const string SearchTextParameterName = "PSearchText";
        protected const string CheckSFCFlag = "PCheckSFCFlag";




        /// <summary>
        /// ConnectionString holds connection string for database.
        /// </summary>
        public string ConnectionString { get; }
        /// <summary>
        /// Configuration holds configurations and settings.
        /// </summary>
        protected IConfiguration Configuration { get; }
        public ILogger Logger { get; }
        #endregion

        #region Private Methods
        /// <summary>
        /// GetException format exception details.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="exception"></param>
        /// <param name="parameters"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        private DatabaseException GetException(string className, string methodName, Exception exception, List<DbParameter> parameters, string commandText, CommandType commandType)
        {
            var message = $"Failed in {className}.{methodName}. {exception.Message}";
            var dbException = new DatabaseException(message, exception, parameters, commandText, commandType);

            return dbException;
        }
        #endregion

        #region Database Operations
        /// <summary>
        /// GetConnection initializes and return connection fro given connection string.
        /// </summary>
        /// <returns></returns>
        private DbConnection GetConnection()
        {
            DbConnection connection = new MySqlConnection(this.ConnectionString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        /// <summary>
        /// GetCommand initializes command object for given connection.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType, List<DbParameter> parameters)
        {
            var command = connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null && parameters.Count > 0)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            return command;
        }

        /// <summary>
        /// GetParameter initializes parameter for provided data.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        protected DbParameter GetParameter(string parameterName, object parameterValue)
        {
            DbParameter parameterObject = new MySqlParameter(parameterName, parameterValue ?? DBNull.Value);

            parameterObject.Direction = ParameterDirection.Input;

            return parameterObject;
        }

        /// <summary>
        /// GetParameter initializes parameter of out type for provided data.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="type"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterDirection"></param>
        /// <returns></returns>
        protected DbParameter GetParameterOut(string parameterName, SqlDbType type, object parameterValue = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            DbParameter parameterObject = new MySqlParameter(parameterName, type);

            if (type == SqlDbType.NVarChar ||
                type == SqlDbType.VarChar ||
                type == SqlDbType.NText ||
                type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            parameterObject.Value = parameterValue ?? DBNull.Value;

            return parameterObject;
        }
        #endregion

        #region Data Operations
        /// <summary>
        /// Executes a SQL statement against a connection object and returns the number of rows affected.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected async Task<int> ExecuteNonQuery(List<DbParameter> parameters, string commandText, CommandType commandType = CommandType.StoredProcedure)
        {
            var returnValue = -1;

            try
            {
                using (var connection = this.GetConnection())
                {
                    var cmd = this.GetCommand(connection, commandText, commandType, parameters);

                    returnValue = await cmd.ExecuteNonQueryAsync();

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var dbException = this.GetException(this.GetType().FullName, "ExecuteNonQuery", ex, parameters, commandText, commandType);
                //LogException(dbException);
                throw dbException;
            }

            return returnValue;
        }

        protected async Task<List<uint>> BulkInsertSQL(string query, List<string> values)
        {

            List<uint> InsertedIds = new List<uint>();

            BulkInsertResponse bulkInsertResponse  = new BulkInsertResponse();

            StringBuilder SQLquery = new StringBuilder(query);

            using (MySqlConnection mConnection = new MySqlConnection(this.ConnectionString))
            {
                

                SQLquery.Append(string.Join(",", values));
                SQLquery.Append("ON DUPLICATE KEY UPDATE Value = VALUES(Value),  ModifiedDate = utc_timestamp() ; COMMIT; ");


                //Create a data reader and Execute the command
                //MySqlDataReader dataReader = myCmd.ExecuteReader();

                mConnection.Open();
                using (MySqlCommand cmd = mConnection.CreateCommand())
                {
                    cmd.CommandText = SQLquery.ToString();
                    MySqlDataReader dataReader = null;
                    try
                    {
                        dataReader = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (mConnection.State == ConnectionState.Open)
                        {
                            mConnection.Close();
                            mConnection.Dispose();
                        }
                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }
                    }

                }

            }

            if (bulkInsertResponse != null)
            {
                for (int i = 0; i < bulkInsertResponse.RowCount; i++)
                {
                    InsertedIds.Add(bulkInsertResponse.FirstId);
                    bulkInsertResponse.FirstId++;
                }
            }

            return InsertedIds;
        }

        protected async Task<List<uint>> BulkInsertMySQL(StringBuilder SQLquery, List<string> values)
        {

            List<uint> InsertedIds = new List<uint>();

            BulkInsertResponse bulkInsertResponse = new BulkInsertResponse();            

            using (MySqlConnection mConnection = new MySqlConnection(this.ConnectionString))
            {
                //Create a data reader and Execute the command
                //MySqlDataReader dataReader = myCmd.ExecuteReader();

                mConnection.Open();
                using (MySqlCommand cmd = mConnection.CreateCommand())
                {
                    cmd.CommandText = SQLquery.ToString();
                    MySqlDataReader dataReader = null;
                    try
                    {
                        dataReader = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (mConnection.State == ConnectionState.Open)
                        {
                            mConnection.Close();
                            mConnection.Dispose();
                        }
                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }
                    }

                }

            }

            if (bulkInsertResponse != null)
            {
                for (int i = 0; i < bulkInsertResponse.RowCount; i++)
                {
                    InsertedIds.Add(bulkInsertResponse.FirstId);
                    bulkInsertResponse.FirstId++;
                }
            }

            return InsertedIds;
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query. All other
        /// columns and rows are ignored. And returns the first column of the first row in the result set.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected async Task<object> ExecuteScalar(List<DbParameter> parameters, string commandText, CommandType commandType = CommandType.StoredProcedure)
        {
            object returnValue = null;

            try
            {
                using (var connection = this.GetConnection())
                {
                    var cmd = this.GetCommand(connection, commandText, commandType, parameters);

                    returnValue = await cmd.ExecuteScalarAsync();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var dbException = this.GetException(this.GetType().FullName, "ExecuteScalar", ex, parameters, commandText, commandType);
                //LogException(dbException);
                throw dbException;
            }

            return returnValue;
        }

        /// <summary>
        /// Executes the System.Data.Common.DbCommand.CommandText against the System.Data.Common.DbCommand.Connection, and
        /// returns an System.Data.Common.DbDataReader using one of the System.Data.CommandBehavior values.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected async Task<DbDataReader> ExecuteReader(List<DbParameter> parameters, string commandText, CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds;

            try
            {
                //using (var connection = this.GetConnection())
                //{

                var connection = this.GetConnection();
                var cmd = this.GetCommand(connection, commandText, commandType, parameters);
                ds = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                //}
            }
            catch (Exception ex)
            {
                var dbException = this.GetException(this.GetType().FullName, "ExecuteReader", ex, parameters, commandText, commandType);
                //LogException(dbException);
                throw dbException;
            }

            return ds;
        }


        protected async Task<List<uint>> BulkInsertSQLGeneric(string query, List<string> values)
        {

            List<uint> InsertedIds = new List<uint>();

            BulkInsertResponse bulkInsertResponse = new BulkInsertResponse();

            StringBuilder SQLquery = new StringBuilder(query);

            using (MySqlConnection mConnection = new MySqlConnection(this.ConnectionString))
            {
                //makes a comma seprated string our of List of Strings
                SQLquery.Append(string.Join(",", values));
                SQLquery.Append(";");
                var returnValue = -1;

                mConnection.Open();
                using (MySqlCommand cmd = mConnection.CreateCommand())
                {
                    cmd.CommandText = SQLquery.ToString();
                    try
                    {
                        returnValue = await cmd.ExecuteNonQueryAsync();
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (mConnection.State == ConnectionState.Open)
                        {
                            mConnection.Close();
                            mConnection.Dispose();
                        }                       
                    }

                }

            }

            

            return InsertedIds;
        }

        protected async Task<bool> BulkUpdateSQLGeneric(string query, List<string> values)
        {

            List<uint> InsertedIds = new List<uint>();

            BulkInsertResponse bulkInsertResponse = new BulkInsertResponse();

            StringBuilder SQLquery = new StringBuilder(query);
            var returnValue = -1;
            using (MySqlConnection mConnection = new MySqlConnection(this.ConnectionString))
            {
                //makes a comma seprated string our of List of Strings
                SQLquery.Append(string.Join("  ", values));
                
               

                //MySqlDataReader dataReader;

                //Create a data reader and Execute the command
                //MySqlDataReader dataReader = myCmd.ExecuteReader();

                mConnection.Open();
                using (MySqlCommand cmd = mConnection.CreateCommand())
                {
                    cmd.CommandText = SQLquery.ToString();
                    MySqlDataReader dataReader = null;
                    try
                    {
                        returnValue = await cmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (mConnection.State == ConnectionState.Open)
                        {
                            mConnection.Close();
                            mConnection.Dispose();
                        }                       
                    }

                }

            }

           

            return true;
        }

        #endregion
    }
}