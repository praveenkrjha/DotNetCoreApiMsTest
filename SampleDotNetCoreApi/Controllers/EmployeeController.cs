using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleDotNetCoreApiBusiness.Manager;
using SampleDotNetCoreApiBusiness.Entities;
using System.Net;
using Serilog;

namespace SampleDotNetCoreApi.Controllers
{
    /// <summary>
    /// SampleDotNetCoreApi.Controllers.EmployeeController class for employee controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeManager _employeeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="employeeManager">The employee manager.</param>
        public EmployeeController(ILogger logger, IEmployeeManager employeeManager)
        {
            _logger = logger;
            _employeeManager = employeeManager;
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Employees")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), (int)HttpStatusCode.OK)]
        public IActionResult GetEmployees()
        {
            var employees =  _employeeManager.GetEmployees().Result;
            return Ok(employees);
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="empId">The emp identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Employee")]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public IActionResult GetEmployee([FromQuery] int empId)
        {
            var employee = _employeeManager.GetEmployee(empId).Result;
            return Ok(employee);
        }


        /// <summary>
        /// Posts the specified emp.
        /// </summary>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Employee")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult CreateEmployee([FromBody]Employee emp)
        {
            var status = _employeeManager.AddEmployee(emp).Result;
            return Ok(status);
        }
    }
}
