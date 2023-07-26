using Microsoft.AspNetCore.Mvc;
using PairOfEmployeesWorkingTogether.Services.EmployeesService;

namespace PairOfEmployeesWorkingTogether.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        [HttpPost]
        [Route("get")]
        public IActionResult GetProjectEmployees([FromForm] IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                return BadRequest("No file selected.");
            }

            var projectsEmployees = employeesService.GetProjectsEmployees(csvFile);

            return Ok(projectsEmployees);
        }
    }
}
