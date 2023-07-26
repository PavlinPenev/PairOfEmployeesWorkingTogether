using CsvHelper.Configuration;
using PairOfEmployeesWorkingTogether.Services.Models;

namespace PairOfEmployeesWorkingTogether.Services.Csv_Configuration
{
    public class ProjectEmployeeMap : ClassMap<ProjectEmployee>
    {
        public ProjectEmployeeMap()
        {
            Map(m => m.EmpID).Name("EmpID");
            Map(m => m.ProjectID).Name("ProjectID");

            Map(m => m.DateFrom).Name("DateFrom").TypeConverter<DateTimeConverter>();
            Map(m => m.DateTo).Name("DateTo").Optional().TypeConverter<DateTimeConverter>();
        }
    }
}
