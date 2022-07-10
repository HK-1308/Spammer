using System.Data;

namespace TestTask.Services.Interfaces
{
    public interface IDataTableFormatter
    {
        public DataTable FormatTable(DataTable dataTable); 
    }
}
