using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.AuthorDtos
{
    public class AuthorReadDto
    {
        [Key]

        public int AuthorId { get; set; }
        public string Name { get; set; } = "";
        public int NumberOfBooks { get; set; }
    }
}
