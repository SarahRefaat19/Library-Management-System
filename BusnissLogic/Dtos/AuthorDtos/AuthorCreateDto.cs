using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.AuthorDtos
{
    public class AuthorCreateDto
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string Description { get; set; } = "";

    }
}
