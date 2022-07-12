using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class JsonConverter
    {
        public string ConverJsonToCsv(string jsonString,IDataTableFormatter tableFormatter)
        {
            DataTable dataTable = JsonStringToTable(jsonString);
            if (tableFormatter != null)
            {
                dataTable = tableFormatter.FormatTable(dataTable);
            }
            string csvString = DataTableToCsv(dataTable, ";");
            return csvString;
        }

        private static string DataTableToCsv(DataTable dataTable, string delimiter)
        {
            StringWriter csvString = new StringWriter();
            using (var csv = new CsvWriter(csvString, new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture) { Delimiter = delimiter }))
            {

                foreach (DataColumn column in dataTable.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                foreach (DataRow row in dataTable.Rows)
                {
                    for (var i = 0; i < dataTable.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }
            return csvString.ToString();
        }

        private static DataTable JsonStringToTable(string JSONData)
        {
            DataTable dtUsingMethodReturn = new DataTable();
            string[] jsonStringArray = Regex.Split(JSONData.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string strJSONarr in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(strJSONarr.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx).Replace("\"", "").Trim().Trim('\n');
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dtUsingMethodReturn.Columns.Add(AddColumnName);
            }
            foreach (string strJSONarr in jsonStringArray)
            {
                string[] RowData = Regex.Split(strJSONarr.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dtUsingMethodReturn.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx).Replace("\"", "").Trim().Trim('\n');
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "").Trim().Trim('\n');
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dtUsingMethodReturn.Rows.Add(nr);
            }
            return dtUsingMethodReturn;
        }
    }
}
