using System.Data;
using TestTask.Services.Interfaces;

namespace TestTask.Services.DataTable_Formatters
{
    public class FormatterForNumberRequest : IDataTableFormatter
    {
        public DataTable FormatTable(DataTable dataTable)
        {
            if (dataTable.Columns.Contains("found"))
            {
                dataTable.Columns.Remove("found");
            }
            return dataTable;
        }
    }
}
