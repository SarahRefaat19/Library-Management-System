using LibrarySystem.BusnissLogic.Dtos.MemberDtos;
using LibrarySystem.BusnissLogic.Dtos.UserDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace LibrarySystem.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    [ApiController]
    [Route("api/[controller]")]

    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IUserService _UserService;

        public MemberController(IMemberService MemberService, IUserService userService)
        {
            _memberService = MemberService;
            _UserService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberReadDto>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<MemberReadDto>>> GetActiveMembers()
        {
            var activeMembers = await _memberService.GetActiveMembersAsync();
            return Ok(activeMembers);
        }
        [HttpPost]
        public async Task<ActionResult> CreateMember([FromBody] MemberCreateDto memberDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string password = memberDto.Password; 
            await _memberService.CreateAsync(memberDto, password);

            return Ok("Member created successfully");
        }

        [HttpPut("{id}/fine")]
        public async Task<ActionResult> UpdateFine(string id, [FromBody] decimal amount)
        {
            await _memberService.UpdateFineAsync(id, amount);
            return Ok($"Fine updated for member {id}");
        }

        [HttpPost("{id}/role")]
        public async Task<ActionResult> AddToRole(string id, [FromBody] string role)
        {
            var member  = await _UserService.GetUserByIdAsync(id);
            if (member == null)
                return NotFound($"No member found with ID {id}");

            await _memberService.AddToRoleAsync(id, role);
            return Ok($"Role '{role}' added to member {id}");
        }

    }
}