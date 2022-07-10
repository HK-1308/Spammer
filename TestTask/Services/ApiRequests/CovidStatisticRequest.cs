using TestTask.Services.Interfaces;

namespace TestTask.Services.ApiRequests
{
    public class CovidStatisticRequest : IApiRequest
    {
        public async Task<string> Request(string[] param)
        {
            var country = param[0];
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://covid-193.p.rapidapi.com/statistics?country={country}"),
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
                return body;
            }
        }
    }
}
