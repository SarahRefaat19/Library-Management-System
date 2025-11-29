using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.LibrarianDtos
{
    public class LibrarianReadDto
    {
        public string id { get; set; } = "";
        [Required]

        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = "";
        public string Role { get; set; } = "Admin";
    }
}
