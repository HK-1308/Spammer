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
        private readonly IUserRepository usersRepository;

        public AdminController( IUserRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpGet("GetUsersForAdmin")]
        public async Task<IActionResult> GetUsersForAdmin()
        {
            var usersList = await usersRepository.GetUsersForAdmin();
            return Ok(usersList);
        }

    }
}
