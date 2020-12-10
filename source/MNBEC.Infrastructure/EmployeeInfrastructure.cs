﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    /// EmployeeInfrastructure inherits from BaseDataAccess and implements IEmployeeInfrastructure. It performs all required CRUD operations on Employee Entity on database.
    /// </summary>
    public class EmployeeInfrastructure : BaseInfrastructure, IEmployeeInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  Employeefrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public EmployeeInfrastructure(IConfiguration configuration, ILogger<EmployeeInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "EmployeeAdd";
        private const string ActivateStoredProcedureName = "EmployeeActivate";
        private const string GetStoredProcedureName = "EmployeeGet";
        private const string GetAllStoredProcedureName = "EmployeeGetAll";
        private const string GetListStoredProcedureName = "EmployeeGetList";
        private const string UpdateStoredProcedureName = "EmployeeUpdate";

        private const string EmployeeIdColumnName = "EmployeeId";
        private const string UserTypeIdColumnName = "UserTypeId";

        //private const string EmployeeNameColumnName = "EmployeeName";
        //private const string EmployeeNameTranslationColumnName = "EmployeeNameTranslation";

        private const string EmployeeIdParameterName = "PEmployeeId";
        private const string UserTypeIdParameterName = "PUserTypeId";

        //private const string EmployeeNameParameterName = "PEmployeeName";
        // private const string EmployeeNameTranslationParameterName = "PEmployeeNameTranslation";
        #endregion

        #region Interface IEmployeefrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<int> Add(Employee employee)
        {
            var employeeIdParamter = base.GetParameterOut(EmployeeInfrastructure.EmployeeIdColumnName, SqlDbType.Int, employee.EmployeeId);
            var parameters = new List<DbParameter>
            {
                employeeIdParamter,
                base.GetParameter(EmployeeInfrastructure.EmployeeIdParameterName, employee.EmployeeId),
                base.GetParameter(EmployeeInfrastructure.UserTypeIdParameterName, employee.UserTypeId),
                base.GetParameter(BaseInfrastructure.ActiveParameterName, employee.Active),

                //base.GetParameter(EmployeeInfrastructure.EmployeeNameTranslationParameterName, employee.EmployeeName),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, employee.CreatedById)
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, EmployeeInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            employee.EmployeeId = (int)employeeIdParamter.Value;

            return employee.EmployeeId;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Employee employee)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(EmployeeInfrastructure.EmployeeIdParameterName, employee.EmployeeId),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, employee.CreatedById)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, EmployeeInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Employee> Get(Employee employee)
        {
            Employee EmployeeItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(EmployeeInfrastructure.EmployeeIdParameterName, employee.EmployeeId),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, employee.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, EmployeeInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        EmployeeItem = new Employee
                        {
                            EmployeeId = dataReader.GetUnsignedIntegerValue(EmployeeInfrastructure.EmployeeIdColumnName),
                            UserTypeId = dataReader.GetUnsignedIntegerValue(EmployeeInfrastructure.UserTypeIdColumnName),
                            // EmployeeName = dataReader.GetStringValue(EmployeeInfrastructure.EmployeeNameColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseInfrastructure.CreatedByIdColumnName),
                            CreatedByName = dataReader.GetStringValue(BaseInfrastructure.CreatedByNameColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseInfrastructure.ModifiedByIdColumnName),
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

            return EmployeeItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<AllResponse<Employee>> GetAll(AllRequest<Employee> employee)
        {
            var result = new AllResponse<Employee>
            {
                Data = new List<Employee>(),
                Offset = employee.Offset,
                PageSize = employee.PageSize,
                SortColumn = employee.SortColumn,
                SortAscending = employee.SortAscending
            };

            Employee employeeItem = null;
            var totalRecordParamter = base.GetParameterOut(BaseInfrastructure.TotalRecordParameterName, SqlDbType.Int, result.TotalRecord);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(BaseInfrastructure.OffsetParameterName, employee.Offset),
                base.GetParameter(BaseInfrastructure.PageSizeParameterName, employee.PageSize),
                base.GetParameter(BaseInfrastructure.SortColumnParameterName, employee.SortColumn),
                base.GetParameter(BaseInfrastructure.SortAscendingParameterName, employee.SortAscending),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, employee.Data.CreatedById)
            };
            //TODO: Add other parameters.

            using (var dataReader = await base.ExecuteReader(parameters, EmployeeInfrastructure.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        employeeItem = new Employee
                        {
                            EmployeeId = dataReader.GetUnsignedIntegerValue(EmployeeInfrastructure.EmployeeIdColumnName),
                            UserTypeId = dataReader.GetUnsignedIntegerValue(EmployeeInfrastructure.UserTypeIdColumnName)

                            //EmployeeName = dataReader.GetStringValue(EmployeeInfrastructure.EmployeeNameColumnName)
                        };
                        //TODO: Add other Columns.

                        result.Data.Add(employeeItem);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                    result.TotalRecord = (int)totalRecordParamter.Value;
                }
            }

            return result;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<List<Employee>> GetList(Employee employee)
        {
            var employees = new List<Employee>();
            Employee employeeItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, employee.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, EmployeeInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        employeeItem = new Employee
                        {
                            EmployeeId = dataReader.GetUnsignedIntegerValue(EmployeeInfrastructure.EmployeeIdColumnName),
                            UserTypeId = dataReader.GetUnsignedIntegerValue(EmployeeInfrastructure.UserTypeIdColumnName)

                            //EmployeeName = dataReader.GetStringValue(EmployeeInfrastructure.EmployeeNameColumnName)
                        };

                        employees.Add(employeeItem);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> Update(Employee employee)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(EmployeeInfrastructure.EmployeeIdParameterName, employee.EmployeeId),
                base.GetParameter(EmployeeInfrastructure.UserTypeIdParameterName, employee.UserTypeId),
                //base.GetParameter(EmployeeInfrastructure.EmployeeNameParameterName, employee.EmployeeName),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, employee.CreatedById)
            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, EmployeeInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        #endregion
    }
}
