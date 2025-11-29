using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.MemberDtos
{
    public class MemberCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = "";
        [Required]
        [MaxLength(50)]
        [MinLength(6)]

        public string Password { get; set; } = "";

        public string Role { get; set; } = "Member";

    }
}
