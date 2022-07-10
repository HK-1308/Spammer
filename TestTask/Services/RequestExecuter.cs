using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class RequestExecuter
    {
        public async Task<string> ExecuteRequset(IApiRequest apiRequest, string[] param)
        {
            if (apiRequest != null)
            {
                return await apiRequest.Request(param);
            }
            return "Bad request";
        }
    }
}
