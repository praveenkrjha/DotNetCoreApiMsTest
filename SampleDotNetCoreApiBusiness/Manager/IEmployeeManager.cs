using SampleDotNetCoreApiBusiness.Entities;
using System.Collections.Generic;

namespace SampleDotNetCoreApiBusiness.Manager
{
    public interface IEmployeeManager
    {
        List<Employee> GetEmployees();

        Employee GetEmployee(int empId);

        bool AddEmployee(Employee emp);
    }
}
