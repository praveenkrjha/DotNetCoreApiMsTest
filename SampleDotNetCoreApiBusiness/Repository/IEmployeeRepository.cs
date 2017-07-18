using SampleDotNetCoreApiBusiness.Entities;
using System.Collections.Generic;

namespace SampleDotNetCoreApiBusiness.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();

        Employee GetEmployee(int empId);

        bool AddEmployee(Employee emp);
    }
}
