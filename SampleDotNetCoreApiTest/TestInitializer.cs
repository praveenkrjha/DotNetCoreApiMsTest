using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleDotNetCoreApiBusiness.Repository;
using System.Net.Http;

namespace SampleDotNetCoreApiTest
{
    [TestClass]
    public class TestInitializer
    {
        public static HttpClient TestHttpClient;
        public static Mock<IEmployeeRepository> MockEmployeeRepository;
        
        [AssemblyInitialize]
        public static void InitializeTestServer(TestContext testContext)
        {
            var testServer = new TestServer(new WebHostBuilder()
               .UseStartup<TestStartup>()
               // this would cause it to use StartupIntegrationTest class or ConfigureServicesIntegrationTest / ConfigureIntegrationTest methods (if existing)
               // rather than Startup, ConfigureServices and Configure 
               .UseEnvironment("IntegrationTest"));

            TestHttpClient = testServer.CreateClient();
        }

        /// <summary>
        /// Register your mock repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterMockRepositories(IServiceCollection services)
        {
            MockEmployeeRepository = (new Mock<IEmployeeRepository>());
            services.AddSingleton(MockEmployeeRepository.Object);
            
            //add more mock repositories below
        }
    }
}
