using LibrarySystem.BusnissLogic.Dtos.LibrarianDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrarySystem.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    [ApiController]
    [Route("api/[controller]")]
    public class LibrarianController : ControllerBase
    {
        private readonly ILibrarianService _librarianService;

        public LibrarianController(ILibrarianService librarianService)
        {
            _librarianService = librarianService;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibrarianReadDto>>> GetAllLibrarians()
        {
            var librarians = await _librarianService.GetAllAsync();
            return Ok(librarians);
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<LibrarianReadDto>> GetLibrarianById(string id)
        {
            var librarian = await _librarianService.GetByIdAsync(id);
            if (librarian == null)
                return NotFound($"No librarian found with ID {id}");

            return Ok(librarian);
        }





        [HttpPost]
        public async Task<ActionResult> CreateLibrarian([FromBody] LibrarianCreateDto librarianDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string password = librarianDto.Password; 
            await _librarianService.CreateAsync(librarianDto, password);

            return Ok(new { message = "Librarian created successfully" });
        }




        [HttpPost("{id}/role")]
        public async Task<ActionResult> AddToRole(string id, [FromBody] string role)
        {
            await _librarianService.AddToRoleAsync(id, role);

            return Ok(new { message = $"Role '{role}' added to librarian {id}" });
        }
    }
}
