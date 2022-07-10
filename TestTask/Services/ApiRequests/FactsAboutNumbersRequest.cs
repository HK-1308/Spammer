using TestTask.Services.Interfaces;

namespace TestTask.Services.ApiRequests
{
    public class FactsAboutNumbersRequest : IApiRequest
    {
        public async Task<string> Request(string[] param)
        {
            var min = param[0];
            var max = param[1];
            var type = param[2];
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://numbersapi.p.rapidapi.com/random/{type}?min={min}&max={max}&fragment=true&json=true"),
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
                return body;
            }
        }
    }
}
