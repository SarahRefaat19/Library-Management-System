using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.AuthorDtos
{
    public class AuthorDeleteDto
    {
        [Key]

        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
