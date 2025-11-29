using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.AuthorDtos
{
    public class AuthorReadWithBooksDto
    {
        [Key]

        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = "";
        public string? AuthorDescription { get; set; }
        public List<ListBookDto> Books { get; set; } = new();
    }
}
