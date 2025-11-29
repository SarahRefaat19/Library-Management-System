using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.RegisterDtos
{
    public class Register
    {
        [Required]
        public string UserName { get; set; } = "";

        [Required]
        [MinLength(6)]

        public string Password { get; set; } = "";
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = "";
    }
}
