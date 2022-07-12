using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TestTask.Controllers
{
    [Authorize (Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminStatisticRepository statisticsRepository;

        public AdminController( IAdminStatisticRepository statisticsRepository)
        {
            this.statisticsRepository = statisticsRepository;
        }

        [HttpGet("GetUsersForAdmin")]
        public async Task<IActionResult> GetUsersForAdmin()
        {
            var usersList = await statisticsRepository.GetUsersForAdmin();
            return Ok(usersList);
        }

        [HttpGet("GetUserHistory/{email}")]
        public async Task<IActionResult> GetUserHistory([FromRoute] string email)
        {
            var usersList = await statisticsRepository.GetUserHistory(email);
            return Ok(usersList);
        }

        [HttpGet("GetHistory")]
        public async Task<IActionResult> GetHistory()
        {
            var usersList = await statisticsRepository.GetHistory();
            return Ok(usersList);
        }

    }
}
