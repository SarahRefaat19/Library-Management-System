using LibrarySystem.BusnissLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.Dtos.RegisterDtos;

using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;

        }
        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var result = await _authService.RegisterUserAsync(register, "Member");

            if (result.Succeeded) {
                return Ok("Registered Successfully");
            }
            return BadRequest("Wrong Input ");


        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login  login )
        {
            var token  = await _authService.LoginUserAsync(login);

            if (token==null)
            {
                return Unauthorized("Invalid Password Or UserName ");
            }
            return Ok(new { token } );


        }



    }
}
