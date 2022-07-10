using System.Data;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class FormatterForWeatherRequest : IDataTableFormatter
    {
        public DataTable FormatTable(DataTable dataTable)
        {
            dataTable.Columns.Remove("icon");
            dataTable.Columns.Remove("is_day");
            dataTable.Columns.Remove("location");
            dataTable.Columns.Remove("lat");
            dataTable.Columns.Remove("lon");
            dataTable.Columns.Remove("tz_id");
            dataTable.Columns.Remove("localtime_epoch");
            dataTable.Columns.Remove("current");
            dataTable.Columns.Remove("code");
            return dataTable;
        }
    }
}
