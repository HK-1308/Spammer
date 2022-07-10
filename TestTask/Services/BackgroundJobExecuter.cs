using TestTask.Data.Interfaces;
using TestTask.Services;
using TestTask.Services.ApiRequests;
using TestTask.Services.Data;
using TestTask.Services.DataTable_Formatters;
using TestTask.Services.Interfaces;

namespace TestTask.Data.Services
{
    public class BackgroundJobExecuter : IBackgroundJobExecuter
    {
        private readonly IJobsRepository jobsRepository;
        private readonly IEmailSender emailSender;
        private readonly RequestExecuter requestExecuter;
        private readonly JsonConverter jsonConverter;
        private readonly object locker = new object();
        public BackgroundJobExecuter(IJobsRepository jobsRepository, IEmailSender emailSender, RequestExecuter requestExecuter, JsonConverter jsonConverter)
        {
            this.jobsRepository = jobsRepository;
            this.emailSender = emailSender;
            this.requestExecuter = requestExecuter;
            this.jsonConverter = jsonConverter;
        }
        public async Task DoWork()
        {
          var jobs = await jobsRepository.GetExpiredJobs();

            Parallel.ForEach(jobs, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async job => 
            {
                job.NextExecutionDate = DateTime.Now.AddHours(job.Period);
                job.NextExecutionDate = new DateTime(job.NextExecutionDate.Value.Year, job.NextExecutionDate.Value.Month,
                    job.NextExecutionDate.Value.Day, job.NextExecutionDate.Value.Hour, job.NextExecutionDate.Value.Minute, 0);
                job.LastExecutionDate = DateTime.Now;
                job.LastExecutionDate = new DateTime(job.LastExecutionDate.Value.Year, job.LastExecutionDate.Value.Month,
                    job.LastExecutionDate.Value.Day, job.LastExecutionDate.Value.Hour, job.LastExecutionDate.Value.Minute, 0);
                lock (locker)
                {
                     jobsRepository.UpdateJob(job);
                }

                string csvString = "";
                bool isBadRequest = true;
                if (job.ApiUrlForJob == "WeatherApi")
                {
                    var requestResult = await requestExecuter.ExecuteRequset(new WeatherForecastRequest(), job.Params.Split(','));
                    if (requestResult != "Bad Request")
                    {
                        csvString = jsonConverter.ConverJsonToCsv(requestResult, new FormatterForWeatherRequest());
                        isBadRequest = false;
                    }
                }
                if (job.ApiUrlForJob == "CovidApi")
                {
                    var requestResult = await requestExecuter.ExecuteRequset(new CovidStatisticRequest(), job.Params.Split(','));
                    if (requestResult != "Bad Request")
                    {
                        csvString = jsonConverter.ConverJsonToCsv(requestResult, new FormatterForCovidRequest());
                        isBadRequest = false;
                    }
                }
                if (job.ApiUrlForJob == "NumbersApi")
                {
                    var requestResult = await requestExecuter.ExecuteRequset(new FactsAboutNumbersRequest(), job.Params.Split(','));
                    if (requestResult != "Bad Request")
                    {
                        csvString = jsonConverter.ConverJsonToCsv(requestResult, new FormatterForNumberRequest());
                        isBadRequest = false;
                    }
                }
                Message message;
                if (!isBadRequest)
                {
                    message = new Message(new string[] { $"{job.UserEmail}" }, "Message from SpaMMMer", "Some text btw", csvString);
                }
                else
                {
                    message = new Message(new string[] { $"{job.UserEmail}" }, "Message from SpaMMMer", $"We have a problems with {job.ApiUrlForJob}", $"Please correct your params for task {job.Name}");
                }
                await emailSender.SendEmailAsync(message);
            } 
            );
        }

    }
}
