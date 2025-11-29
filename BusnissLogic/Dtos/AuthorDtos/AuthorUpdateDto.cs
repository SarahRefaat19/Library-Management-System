using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.AuthorDtos
{
    public class AuthorUpdateDto
    {
        [Key]

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }


    }
}
