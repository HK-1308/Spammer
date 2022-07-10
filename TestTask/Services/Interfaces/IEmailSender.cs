using TestTask.Services.Data;

namespace TestTask.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
