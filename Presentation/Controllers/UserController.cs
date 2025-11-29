using LibrarySystem.BusinessLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.Dtos.UserDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace LibrarySystem.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]


    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }
        [HttpGet("All")]

        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllAsync()
        {
            var Users = await _UserService.GetAllUsersAsync();
            if (Users == null || !Users.Any())
                return NotFound("No users found.");
            return Ok(Users);
        }


        [HttpGet("by-id/{id:int}")]



        public async Task<ActionResult<UserReadDto>> GetUserByIdAsync(string id )
        {
            var User = await _UserService.GetUserByIdAsync(id);
            if (User == null )
                return NotFound("No user found.");
            return Ok(User);
        }








        [HttpGet("by-email/{email}")]


        public async Task<ActionResult<UserReadDto>> GetByEmailAsync(string email)
        {
            var User = await _UserService.GetByEmailAsync(email);
            if (User == null)
                return NotFound("No user found.");
            return Ok(User);
        }




        [HttpPost("Create")]


        public async Task<ActionResult<UserReadDto>> AddUserAsync([FromBody] UserCreateDto UserCreateDto)
        {

            var User = await _UserService.AddUserAsync(UserCreateDto);

            return CreatedAtAction(nameof(GetByEmailAsync), new { email = User.Email }, User);

        }







        [HttpPut("Update")]

        public async Task<ActionResult<UserReadDto>> UpdateUserAsync(UserUpdateDto UserUpdateDto)
        {
            var User = await _UserService.UpdateUserAsync(UserUpdateDto);
            if (User == null)
                return NotFound("No user found.");
            
            return Ok(User);
        }

    }
}
