using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SampleDotNetCoreApiBusiness;
using SampleDotNetCoreApiBusiness.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SampleDotNetCoreApiTest
{
    [TestClass]
    public class TestEmployee
    { 

        [TestMethod]
        public void TestGetAllEmployees()
        {
            var mockEmps = new List<Employee>();
            mockEmps.Add(new Employee { EmpId = 1, FirstName = "F1", LastName = "L1", Email = "F1.L1@tt.com" });
            mockEmps.Add(new Employee { EmpId = 2, FirstName = "F2", LastName = "L2", Email = "F2.L2@tt.com" });
            mockEmps.Add(new Employee { EmpId = 3, FirstName = "F3", LastName = "L3", Email = "F3.L3@tt.com" });

            var employeeRepositoryMock = TestInitializer.MockEmployeeRepository;
            employeeRepositoryMock.Setup(x => x.GetEmployees()).Returns(Task.FromResult(mockEmps));

            var response = TestInitializer.TestHttpClient.GetAsync("api/Employees").Result;
            
            var responseStr = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<List<Employee>>(responseStr);
            Assert.AreEqual(3, responseData.Count);
            Assert.AreEqual(mockEmps[0].EmpId, responseData[0].EmpId);
        }
    }
}
