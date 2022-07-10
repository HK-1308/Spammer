using Microsoft.AspNetCore.Identity;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Data.Services
{
    public class ApplicationBackgroundService: IHostedService
    {
        private readonly IServiceProvider _service;
        public ApplicationBackgroundService(IServiceProvider service)
        {
            _service = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(60000);
                    await DoWork();
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            using (var scope = _service.CreateScope())
            {
                var backgroundJobExecuter = scope.ServiceProvider.GetRequiredService<IBackgroundJobExecuter>();
                await backgroundJobExecuter.DoWork();
            }
        }
    }
}
