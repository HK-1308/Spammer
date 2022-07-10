using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.RegularExpressions;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RapidApisController:ControllerBase
    {

        [HttpGet("GetNumberFact")]
        public async Task<IActionResult> NumberApi()
        {
            //var min = param[0];
            //var max = param[1];
            //var type = param[2];
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://numbersapi.p.rapidapi.com/random/math?min=10&max=20&fragment=true&json=true"),
                Headers =
                {
                    { "X-RapidAPI-Key", "ec412fc7e3msh53bd9ee94e8b89cp1b895ajsn8f45b12741aa" },
                    { "X-RapidAPI-Host", "numbersapi.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                DataTable dataTable = JsonStringToTable(body);
                dataTable.Columns.Remove("found");
                var csv = DataTableToCSV(dataTable, ",");
                return Ok(body);
            }
        }

        [HttpGet("GetWeather")]
        public async Task<IActionResult> WeatherApi()
        {
            //var city = param[0];
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/current.json?q=Minsk"),
                Headers =
                {
                    { "X-RapidAPI-Key", "30104c4ceemsh680b055a0ffb117p14631ejsnae5c6f527928" },
                    { "X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                DataTable dataTable = JsonStringToTable(body);
                dataTable.Columns.Remove("icon");
                dataTable.Columns.Remove("is_day");
                dataTable.Columns.Remove("location");
                dataTable.Columns.Remove("lat");
                dataTable.Columns.Remove("lon");
                dataTable.Columns.Remove("tz_id");
                dataTable.Columns.Remove("localtime_epoch");
                dataTable.Columns.Remove("current");
                dataTable.Columns.Remove("code");
                var csv = DataTableToCSV(dataTable, ",");
                return Ok(body);
            }
        }

        [HttpGet("GetCovidStatistic")]
        public async Task<IActionResult> CovidApi()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://covid-193.p.rapidapi.com/statistics?country=Belarus"),
                Headers =
                {
                    { "X-RapidAPI-Key", "ec412fc7e3msh53bd9ee94e8b89cp1b895ajsn8f45b12741aa" },
                    { "X-RapidAPI-Host", "covid-193.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                DataTable dataTable = JsonStringToTable(body);
                dataTable.Columns.RemoveAt(0);
                dataTable.Columns.RemoveAt(0);
                dataTable.Columns.RemoveAt(0);
                dataTable.Columns.RemoveAt(0);
                dataTable.Columns.RemoveAt(0);
                var csv = DataTableToCSV(dataTable, ",");
                return Ok(body);
            }
        }
        public static string DataTableToCSV(DataTable dataTable, string delimiter)
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

        public static DataTable JsonStringToTable(string JSONData)
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
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx).Replace("\"", "").Trim();
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
                        string RowColumns = rowData.Substring(0, idx).Replace("\"", "").Trim();
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "").Trim();
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
