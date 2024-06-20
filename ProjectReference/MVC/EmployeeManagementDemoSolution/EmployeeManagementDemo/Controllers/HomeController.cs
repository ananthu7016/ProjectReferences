using System.Data.SqlClient;
using System.Diagnostics;
using EmployeeManagementDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        // 2nd way to configure connection string 
        // first way was we need to go to the Programme.cs and 
        private readonly IConfiguration _configuration;

        private readonly string connectionString;
        public HomeController(ILogger<HomeController> logger,IConfiguration configuration)
        {
            _logger = logger;
            connectionString = configuration.GetConnectionString("ConnectionStringMVC");
        }

        public IActionResult Index()
        {

            // calling Text Connection Method 
            var isConnected = TestConnection();
            return View((object)isConnected.ToString());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // a method to test the connection
        public bool TestConnection()
        {

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
