using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;

namespace MNBEC.ApplicationInterface
{
    /// <summary>
    /// IEmployeeApplication inherits IBaseApplication interface to provide interface for Employee related Application.
    /// </summary>
    public interface IEmployeeApplication : IBaseApplication<Employee>
    {
        //Task<uint> Add(Employee employee);
        //Task<bool> Activate(Employee employee);
        //Task<Employee> Get(Employee employee);
        //Task<AllResponse<Employee>> GetAll(AllRequest<Employee> employee);
        //Task<List<Employee>> GetList(Employee employee);
        //Task<bool> Update(Employee employee);
    }
}
