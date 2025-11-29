using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.BookDtos
{
    public class UpdateBookDto
    {
        [Key]

        public int BookDtoId { get; set; }

        public string Title { get; set; } = "";
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } = " ";
        public IFormFile? ImageFile { get; set; }


    }
}
