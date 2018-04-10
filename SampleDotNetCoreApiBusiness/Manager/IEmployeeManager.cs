using SampleDotNetCoreApiBusiness.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleDotNetCoreApiBusiness.Manager
{
    public interface IEmployeeManager
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int empId);

        Task<int> AddEmployee(Employee emp);
    }
}
