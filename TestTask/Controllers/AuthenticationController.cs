using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Interfaces;
using TestTask.Data.DataTransferObject;
using TestTask.JwtFeatures;
using System.IdentityModel.Tokens.Jwt;
using TestTask.Data.Models;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtHandler jwtHandler;
        private readonly IUserRepository userRepository;

        public AuthenticationController(JwtHandler jwtHandler, IUserRepository userRepository)
        {
            this.jwtHandler = jwtHandler;
            this.userRepository = userRepository;   
        }

        [HttpPost ("registration")]
        public async Task<IActionResult> Registration([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            User user = new User() {Email = userForRegistration.Email, Password = userForRegistration.Password };
            var result = await userRepository.CreateUser(user);
            if (!result)
            {
                List<string> errors = new List<string>();
                errors.Add("Error with registration");
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
            return StatusCode(201);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await userRepository.GetUserForLogin(userForAuthentication.Email, userForAuthentication.Password);
            if (user == null)
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = await jwtHandler.GetClaims(user);
            var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }
}