using System;
using System.Collections.Generic;
using System.Text;
using SampleDotNetCoreApiBusiness.Entities;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace SampleDotNetCoreApiBusiness.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string DbConnectionStr = "Data Source=(local);Initial Catalog=SampleDB;Integrated Security=SSPI;Max Pool size = 100;";

        private readonly ILogger _logger;

        public EmployeeRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<int> AddEmployee(Employee emp)
        {
            using (var con = new SqlConnection(DbConnectionStr))
            {
                con.Open();
                var success = await con.ExecuteAsync("Insert into Employee (FirstName, LastName, Email) values (@FirstName,@LastName,@Email)", emp, commandType: CommandType.Text);
                return success;
            }
        }

        public async Task<Employee> GetEmployee(int empId)
        {
            using (var con = new SqlConnection(DbConnectionStr))
            {
                con.Open();

                var data = (await con.QueryAsync<dynamic>("Select * from Employee where Id=@Id", new { Id = empId }, commandType: CommandType.Text)).Select(x => new Employee
                {
                    EmpId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                }).FirstOrDefault();
                return data;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (var con = new SqlConnection(DbConnectionStr))
            {
                con.Open();
                IEnumerable<Employee> data = null;
                try
                {
                    data = (await con.QueryAsync<dynamic>("Select * from Employee", commandType: CommandType.Text)).Select(x => new Employee
                    {
                        EmpId = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error("Error : {ex}", ex);
                }
                return data.ToList();
            }
        }
    }
}
