using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
using Synel.Domain.Entities.Employees;
using Synel.Domain.Interfaces.Repositories;
using Synel.Infrastructure.Services;
using Synel.Web.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Synel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICsvParserService _csvParserService;
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(ICsvParserService csvParserService,
            IEmployeeRepository employeeRepository)
        {
            _csvParserService = csvParserService;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Import CSV File
        [HttpPost]
        public async Task<IActionResult> ImportCSVAsync(IFormFile file)
        {
            if (file.FileName.EndsWith(".csv"))
            {
                var newEmployeesList = _csvParserService.ReadCSVFile(file);
                foreach (var employee in newEmployeesList)
                {
                    await _employeeRepository.AddEmployee(employee);
                }
            }
            return Redirect("/Home/Index");
        }

        //Get Data from DB & send to DataGrid
        public async Task<IActionResult> UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = await _employeeRepository.GetEmployees();
            var operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search); //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0)                  
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted); //Sorting 
            }
            int count = DataSource.Cast<Employee>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip); //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? new JsonResult(new { result = DataSource, count = count }) : new JsonResult(DataSource);
        }

        //Update specific Employee
        public async Task<ActionResult> Update([FromBody] ICRUDModel<Employee> value)
        {
            var ord = value.value;
            await _employeeRepository.UpdateEmployee(ord);

            return Json(value.value);
        }

        //Delete specific Employee
        public async Task<ActionResult> Delete([FromBody] ICRUDModel<Employee> value)
        {
            await _employeeRepository.DeleteEmployeeAsync(value.key.ToString());
            return Json(value);
        }

        public class ICRUDModel<T> where T : class
        {
            public string action { get; set; }

            public string table { get; set; }

            public string keyColumn { get; set; }

            public object key { get; set; }

            public T value { get; set; }

            public List<T> added { get; set; }

            public List<T> changed { get; set; }

            public List<T> deleted { get; set; }

            public IDictionary<string, object> @params { get; set; }
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
    }
}
