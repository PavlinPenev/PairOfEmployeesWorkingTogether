using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace PairOfEmployeesWorkingTogether.Services.Csv_Configuration
{
    public class DateTimeConverter : ITypeConverter
    {
        private  string[] dateTimeFormats = new string[]
        {
            "yyyy-MM-dd",  
            "dd/MM/yyyy",   
            "MM/dd/yyyy",   
            "dd-MMM-yyyy",   
            "dd MMM yyyy",   
            "yyyy/MM/dd",    
            "yyyy-MM-ddTHH:mm:ss", 
            "yyyy-MM-ddTHH:mm:ssZ", 
            "yyyy-MM-ddTHH:mm:ss.fffZ", 
            "yyyy-MM-ddTHH:mm:ss.fffzzz"
        };

        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            DateTime parsedDate;

            if (DateTime.TryParseExact(text, dateTimeFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }

            return null;
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }
    }
}
