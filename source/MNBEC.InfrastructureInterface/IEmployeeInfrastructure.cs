using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IEmployeeInfrastructure inherites IBaseInfrastructure and provides the interface for Employee operations in Databasse.
    /// </summary>
    public interface IEmployeeInfrastructure : IBaseInfrastructure<Employee>
    {
        //Task<int> Add(Employee employee);
        //Task<bool> Activate(Employee employee);
        //Task<Employee> Get(Employee employee);
        //Task<AllResponse<Employee>> GetAll(AllRequest<Employee> employee);
        //Task<List<Employee>> GetList(Employee employee);
        //Task<bool> Update(Employee employee);







    }
}