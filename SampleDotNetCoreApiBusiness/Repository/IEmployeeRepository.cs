using SampleDotNetCoreApiBusiness.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleDotNetCoreApiBusiness.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int empId);

        Task<int> AddEmployee(Employee emp);
    }
}
