using System.Data;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class FormatterForCovidRequest : IDataTableFormatter
    {
        public DataTable FormatTable(DataTable dataTable)
        {
            dataTable.Columns.RemoveAt(0);
            dataTable.Columns.RemoveAt(0);
            dataTable.Columns.RemoveAt(0);
            dataTable.Columns.RemoveAt(0);
            dataTable.Columns.RemoveAt(0);
            return dataTable;
        }
    }
}
