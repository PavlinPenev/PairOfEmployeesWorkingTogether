using PairOfEmployeesWorkingTogether.Services.Models;

namespace PairOfEmployeesWorkingTogether.Services.EmployeesService
{
    public interface IEmployeesService
    {
        /// <summary>
        /// Gets all the employees that work on a common project
        /// </summary>
        /// <param name="file">A csv file to get the employees' data</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="EmployeeProjectTableData"/></returns>
        List<EmployeeProjectTableData> GetProjectsEmployees(IFormFile file);
    }
}
