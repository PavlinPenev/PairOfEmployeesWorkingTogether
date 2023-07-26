using CsvHelper;
using PairOfEmployeesWorkingTogether.Services.Csv_Configuration;
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
                        csv.Context.RegisterClassMap<ProjectEmployeeMap>();
                        projectEmployees = csv.GetRecords<ProjectEmployee>().ToList();
                    }
                }
            }

            foreach (var projectEmployee in projectEmployees)
            {
                projectEmployee.DateTo = projectEmployee.DateTo.HasValue ? projectEmployee.DateTo : DateTime.UtcNow;
            }

            var groupedProjects = projectEmployees
                .GroupBy(project => project.ProjectID);

            var longestCommonProjects = new List<EmployeeProjectTableData>();

            foreach (var group in groupedProjects)
            {
                if (group.Count() <= 1)
                {
                    continue;
                }
                    
                var projects = group.ToList();

                var commonDaysWorked = 0;
                var emp1 = projects[0].EmpID;
                var emp2 = projects[1].EmpID;

                for (int i = 0; i < projects.Count; i++)
                {
                    var firstEmployee = projects[i];

                    for (int j = 0; j < projects.Count; j++)
                    {
                        if (j != i)
                        {
                            var secondEmployee = projects[j];

                            var workIntersectionStart = firstEmployee.DateFrom > secondEmployee.DateFrom ? firstEmployee.DateFrom : secondEmployee.DateFrom;
                            var workIntersectionEnd = firstEmployee.DateTo < secondEmployee.DateTo ? firstEmployee.DateTo : secondEmployee.DateTo;

                            if (workIntersectionEnd >= workIntersectionStart)
                            {
                                var currentCommonDays = Math.Abs((workIntersectionEnd.Value - workIntersectionStart).Days);

                                if (currentCommonDays > commonDaysWorked)
                                {
                                    commonDaysWorked = currentCommonDays;

                                    emp1 = firstEmployee.EmpID;
                                    emp2 = secondEmployee.EmpID;
                                }
                            }
                        }
                    }
                }

                var employeePair = new EmployeePair
                {
                    FirstEmployeeId = emp1,
                    SecondEmployeeId = emp2,
                    DaysWorkedTogether = commonDaysWorked
                };

                var employeeProjectTableData = new EmployeeProjectTableData
                {
                    ProjectId = group.Key,
                    Employees = employeePair
                };

                if (employeePair.DaysWorkedTogether > 0)
                {
                    longestCommonProjects.Add(employeeProjectTableData);
                }
            }

            return longestCommonProjects;
        }
    }
}
