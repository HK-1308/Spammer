using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Data.DataTransferObject;
using TestTask.Data.Interfaces;
using TestTask.Data.Models;

namespace TestTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IJobsRepository jobsRepository;
        private readonly IUserRepository userRepository;
        public JobsController( IUserRepository userRepository,IJobsRepository jobsRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.jobsRepository = jobsRepository;
            this.mapper = mapper;
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob([FromBody]JobDto jobDto)
        {
            if (jobDto == null || !ModelState.IsValid)
                return BadRequest();
            var job = mapper.Map<Job>(jobDto);
            job.Id = Guid.NewGuid().ToString();
            job.UserEmail = HttpContext.User.Identity?.Name;          
            job.UserId = await userRepository.GetUserIdByEmail(job.UserEmail);
            await jobsRepository.AddJob(job);
            return StatusCode(201);
        }

        [HttpDelete("RemoveJob/{id}")]
        public async Task<IActionResult> RemoveJob([FromRoute] string id)
        {
            await jobsRepository.RemoveJob(id);
            return StatusCode(201);
        }

        [HttpPut("UpdateJob")]
        public async Task<IActionResult> UpdateJob(JobForListDto jobDto)
        {
            if (jobDto == null || !ModelState.IsValid)
                return BadRequest();
            await jobsRepository.UpdateJob(jobDto);
            return StatusCode(201);
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
