namespace TestTask.Services.Interfaces
{
    public interface IBackgroundJobExecuter
    {
        Task DoWork();
    }
}
