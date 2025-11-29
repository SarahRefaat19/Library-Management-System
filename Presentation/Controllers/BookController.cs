

using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using LibrarySystem.BusnissLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;


namespace LibrarySystem.Presentation.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookServicecs;
        private readonly IMapper _mapper;
        private readonly  FileUploadService _fileUploadService;

        public BookController(IBookService bookService, IMapper mapper, FileUploadService fileUploadService) // عشان ال DI
        {
            _bookServicecs = bookService;
            _mapper = mapper;
            _fileUploadService=  fileUploadService;
        }





        [HttpGet("AllBooks")]

        public async Task<ActionResult<List<ListBookDto>>> GetAllAsync()
        {
            var books = await _bookServicecs.GetAllBooksAsync();

            var Listdto = _mapper.Map<List<ListBookDto>>(books);
            return Ok(Listdto);
        }





        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedBooks()
        {
            var books = await _bookServicecs.GetAllBooksAsync();
            var featured = books.Take(10); 
            return Ok(featured);
        }






        [HttpGet("{id}")]

        public async  Task<ActionResult<ReadBookDto>> GetBookByIdAsync(int id)
        {
            var book =  await _bookServicecs.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var Readdto = _mapper.Map<ReadBookDto>(book);
            return Ok(Readdto); 
       }





        [Authorize("Admin,Librarian")]

        [HttpPost]

        public async Task<ActionResult<CreateBookDto>> AddBookAsync(CreateBookDto book)
        {
            var _book = _mapper.Map<Book>(book);

            if (book.ImageFile != null)
            {
                var imagePath = await _fileUploadService.UploadImageAsync(book.ImageFile);
                book.ImageUrl = imagePath;
            }

            
            var AddedBook =await _bookServicecs.AddBookAsync(_book);


            var AddedBookDto = _mapper.Map<CreateBookDto>(AddedBook);

            return CreatedAtAction(nameof(GetBookByIdAsync), new { id = AddedBookDto.Id }, AddedBookDto); //give me a new book url and his details



        }


        [Authorize("Admin ,Librarian")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromForm] Book book)
        {
            var updated = await _bookServicecs.UpdateBookAsync(id,book);

            if (updated == null)
                return NotFound(new { message = "Book not found." });

            return Ok(new { message = "Book updated successfully" });
        }





        [Authorize("Admin ,Librarian")]



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id )
        {
            try
            {
                var deletedBook = await _bookServicecs.DeleteBookAsync(id);

                if (deletedBook == null)
                    return NotFound( "Book with This ID  not found.");

                return Ok(new { message = "Book Deleted Successfully" }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpGet("search")]
        public async Task<ActionResult<List<Book>>> SearchBooksAsync([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Please enter a search keyword.");

            var books = await _bookServicecs.SearchBooksAsync(keyword);

            return Ok(books);
        }







    }
}

