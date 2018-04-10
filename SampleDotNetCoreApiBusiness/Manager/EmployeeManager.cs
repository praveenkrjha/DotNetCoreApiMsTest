using SampleDotNetCoreApiBusiness.Entities;
using SampleDotNetCoreApiBusiness.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleDotNetCoreApiBusiness.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> AddEmployee(Employee emp)
        {
            return await _employeeRepository.AddEmployee(emp);
        }

        public async Task<Employee> GetEmployee(int empId)
        {
            return await _employeeRepository.GetEmployee(empId);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }
    }
}
