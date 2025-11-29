using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.LibrarianDtos
{
    public class LibrarianCreateDto
    {
        [Required]

        public string Name { get; set; } = "";
        [Required]

        public string Email { get; set; } = "";
        [Required]

        public string Password { get; set; } = "";
        [Required]

        public string Role { get; set; } = "Librarian";

    }
}
