using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.RegisterDtos
{
    public class Login
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = "";
        [Required]
        [MaxLength(50)]
        [MinLength(6)]

        public string Password { get; set; } = "";
        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = "";
    }
}
