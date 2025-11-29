using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.AuthorDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace LibrarySystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper) // Dependency Injection
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorReadDto>>> GetAllAuthorsAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            var authorsDto = _mapper.Map<List<AuthorReadDto>>(authors);
            if (authorsDto == null)
            {
                return Ok("Authors is not found ");
            }
            return Ok(authorsDto);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDto>> GetAuthorByIdAsync(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound($"Author with ID {id} not found");

            var authorDto = _mapper.Map<AuthorReadDto>(author);
            return Ok(authorDto);
        }





        [Authorize("Admin ,Librarian")]

        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> AddAuthorAsync(AuthorCreateDto addAuthorDto)
        {
            var authorEntity = _mapper.Map<Author>(addAuthorDto);
            var addedAuthor = await _authorService.AddAuthorAsync(authorEntity);
            var addedAuthorDto = _mapper.Map<AuthorReadDto>(addedAuthor);

            return CreatedAtAction(nameof(GetAuthorByIdAsync), new { id = addedAuthorDto.AuthorId }, addedAuthorDto);
        }


        [Authorize("Admin ,Librarian")]

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorUpdateDto>> UpdateAuthorAsync(int id, AuthorUpdateDto updatedAuthorDto)
        {
            if (id != updatedAuthorDto.Id)
                return BadRequest("Author ID mismatch.");

            var authorEntity = _mapper.Map<Author>(updatedAuthorDto);
            var updatedAuthor = await _authorService.UpdateAuthorAsync(id, authorEntity);

            if (updatedAuthor == null)
                return NotFound($"Author with ID {id} not found");

            var dto = _mapper.Map<AuthorUpdateDto>(updatedAuthor);
            return Ok(dto);
        }

        [Authorize("Admin ,Librarian")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorReadDto>> DeleteAuthorAsync(int id)
        {
            var deletedAuthor = await _authorService.DeleteAuthorAsync(id);
            if (deletedAuthor == null)
                return NotFound($"Author with ID {id} not found.");

            var dto = _mapper.Map<AuthorReadDto>(deletedAuthor);
            return Ok(dto);
        }






        [HttpGet("search")]
        public async Task<ActionResult<List<AuthorReadDto>>> SearchAuthorsByNameAsync([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Please enter a keyword.");

            var authors = await _authorService.SearchAuthorsByNameAsync(keyword);
            var dto = _mapper.Map<List<AuthorReadDto>>(authors);

            return Ok(dto);
        }





        [HttpGet("with-books")]
        public async Task<ActionResult<List<AuthorReadWithBooksDto>>> GetAuthorsWithBooksAsync(string keyword)
        {
            var author = await _authorService.GetAuthorWithBooksByIdAsync(keyword);
            var dto = _mapper.Map<List<AuthorReadWithBooksDto>>(author);
            return Ok(dto);
        }
    }
}

