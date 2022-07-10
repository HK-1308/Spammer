using TestTask.Services.Interfaces;

namespace TestTask.Services.ApiRequests
{
    public class WeatherForecastRequest : IApiRequest
    {
        public async Task<string> Request(string[] param)
        {
            var city = param[0];
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/current.json?q={city}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "30104c4ceemsh680b055a0ffb117p14631ejsnae5c6f527928" },
                    { "X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return body;
                }               
               return "Bad Request";
            }

        }
    }
}
