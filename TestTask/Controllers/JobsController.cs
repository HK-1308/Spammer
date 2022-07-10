using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTask.Data.DataTransferObject;
using TestTask.Data.Interfaces;
using TestTask.Data.Models;
using TestTask.Data.Services;
using TestTask.Models;

namespace TestTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IJobsRepository jobsRepository;
        private readonly UserManager<User> userManager;
        public JobsController( IJobsRepository jobsRepository, IMapper mapper, UserManager<User> userManager)
        {
            this.jobsRepository = jobsRepository;
            this.mapper = mapper;
            this.userManager = userManager; 
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob([FromBody]JobDto jobDto)
        {
            if (jobDto == null || !ModelState.IsValid)
                return BadRequest();
            var job = mapper.Map<Job>(jobDto);
            job.Id = Guid.NewGuid().ToString();
            job.UserEmail = HttpContext.User.Identity?.Name;
            var user = await userManager.FindByEmailAsync(job.UserEmail);
            job.User = user;
            job.UserId = user.Id;
            await jobsRepository.AddJob(job);
            return StatusCode(201);
        }

        [HttpDelete("RemoveJob/{id}")]
        public async Task<IActionResult> RemoveJob([FromRoute] string id)
        {
            var job = await jobsRepository.GetJobAsync(id);
            await jobsRepository.RemoveJob(job);
            return StatusCode(201);
        }

        [HttpPut("UpdateJob")]
        public async Task<IActionResult> UpdateJob(JobForListDto jobDto)
        {
            if (jobDto == null || !ModelState.IsValid)
                return BadRequest();
            var job = await jobsRepository.GetJobAsync(jobDto.Id);
            job.Name = jobDto.Name;
            job.Description = jobDto.Description;
            job.Period = jobDto.Period;
            job.NextExecutionDate = jobDto.NextExecutionDate;
            job.ApiUrlForJob = jobDto.ApiUrlForJob;
            job.Params = jobDto.Params;
            await jobsRepository.UpdateJob(job);
            return StatusCode(201);
        }

        [HttpGet("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobsList = await jobsRepository.GetAllJobsAsync();
            return Ok(jobsList);
        }

        [HttpGet("GetUsersJobs")]
        public async Task<IActionResult> GetUsersJobs()
        {
            var email = HttpContext.User.Identity?.Name;
            var jobsList = await jobsRepository.GetAllUsersJobsAsync(email);

            return Ok(jobsList);
        }

        [HttpGet("GetJob/{id}")]
        public async Task<IActionResult> GetUsersJobs([FromRoute] string id)
        {
            var job = await jobsRepository.GetJobAsync(id);
            return Ok(job);
        }

    }
}
