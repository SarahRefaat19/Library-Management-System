using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.UserDtos
{
    public class UserCreateDto
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = "";
        [Required]
        public string Role { get; set; } = "";

    }
}
