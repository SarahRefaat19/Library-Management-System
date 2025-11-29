using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.BusnissLogic.Dtos.BookDtos
{
    public class CreateBookDto
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public string Title { get; set; } = "";
        [Required]

        public string? ISBN { get; set; } 

        public string? Author { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } = "";
        public IFormFile? ImageFile { get; set; }


    }
}
