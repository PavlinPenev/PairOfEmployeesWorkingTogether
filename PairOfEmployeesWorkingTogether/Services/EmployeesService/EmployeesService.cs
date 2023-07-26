using CsvHelper;
using PairOfEmployeesWorkingTogether.Services.Models;
using System.Globalization;

namespace PairOfEmployeesWorkingTogether.Services.EmployeesService
{
    public class EmployeesService : IEmployeesService
    {
        public List<EmployeeProjectTableData> GetProjectsEmployees(IFormFile file)
        {
            List<ProjectEmployee> projectEmployees;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;

                using (var reader = new StreamReader(memoryStream))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        projectEmployees = csv.GetRecords<ProjectEmployee>().ToList();
                    }
                }
            }

            var groupedProjects = projectEmployees
                .GroupBy(project => project.ProjectID);

            var longestCommonProjects = new List<EmployeeProjectTableData>();

            foreach (var group in groupedProjects)
            {
                var projects = group.ToList();

                projects.OrderBy(x => x.DateFrom);

                int maxDuration = 0;
                int emp1 = projects[0].EmployeeID;
                int emp2 = projects[1].EmployeeID;

                for (int i = 1; i < projects.Count; i++)
                {
                    int duration = (projects[i].DateTo ?? DateTime.UtcNow).Subtract(projects[i - 1].DateFrom).Days;

                    if (duration > maxDuration)
                    {
                        maxDuration = duration;
                        emp1 = projects[i - 1].EmployeeID;
                        emp2 = projects[i].EmployeeID;
                    }
                }

                var employeePair = new EmployeePair
                {
                    FirstEmployeeId = emp1,
                    SecondEmployeeId = emp2,
                    DaysWorked = maxDuration
                };

                var employeeProjectTableData = new EmployeeProjectTableData
                {
                    ProjectId = group.Key,
                    Employees = employeePair
                };

                longestCommonProjects.Add(employeeProjectTableData);
            }

            return longestCommonProjects;
        }
    }
}
