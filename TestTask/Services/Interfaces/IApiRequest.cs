namespace TestTask.Services.Interfaces
{
    public interface IApiRequest
    {
        Task<string> Request(string[] param);
    }
}
