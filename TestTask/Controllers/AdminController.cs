using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Interfaces;
using TestTask.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await usersRepository.AllUsers();
            return Ok(usersList);
        }

        [HttpGet("GetUsersForAdmin")]
        public async Task<IActionResult> GetUsersForAdmin()
        {
            var usersList = await usersRepository.GetUsersForAdmin();
            return Ok(usersList);
        }

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await usersRepository.GetUser(email);
            return Ok(user);
        }
    }
}
