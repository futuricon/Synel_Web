using Synel.Domain.Entities.Employees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synel.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task DeleteEmployeeAsync(string id);
    }
}
