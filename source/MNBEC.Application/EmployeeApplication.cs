using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// EmployeeApplication inherits from BaseApplication and implements IEmployeeApplication. It provides the implementation for Employee related operations.
    /// </summary>
    public class EmployeeApplication : BaseApplication, IEmployeeApplication
    {
        #region Constructor
        /// <summary>
        /// EmployeeApplication initailizes object instance.
        /// </summary>
        /// <param name="employeeInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public EmployeeApplication(IEmployeeInfrastructure employeeInfrastructure, IConfiguration configuration, ILogger<EmployeeApplication> logger) : base(configuration, logger)
        {
            this.EmployeeInfrastructure = employeeInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// EmployeeInfrastructure holds the Infrastructure object.
        /// </summary>
        public IEmployeeInfrastructure EmployeeInfrastructure { get; }
        #endregion

        #region Interface IEmployeeApplication Implementation
        /// <summary>
        /// Add calls EmployeeInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<int> Add(Employee employee)
        {
            return await this.EmployeeInfrastructure.Add(employee);
        }

        /// <summary>
        /// Activate calls EmployeeInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Employee employee)
        {
            return await this.EmployeeInfrastructure.Activate(employee);
        }

        /// <summary>
        /// Get calls EmployeeInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Employee> Get(Employee employee)
        {
            return await this.EmployeeInfrastructure.Get(employee);
        }

        /// <summary>
        /// GetAll calls EmployeeInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<AllResponse<Employee>> GetAll(AllRequest<Employee> employee)
        {
            return await this.EmployeeInfrastructure.GetAll(employee);
        }

        /// <summary>
        /// GetAll calls EmployeeInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<List<Employee>> GetList(Employee employee)
        {
            return await this.EmployeeInfrastructure.GetList(employee);
        }

        /// <summary>
        /// Update calls EmployeeInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> Update(Employee employee)
        {
            return await this.EmployeeInfrastructure.Update(employee);
        }
        #endregion
    }
}