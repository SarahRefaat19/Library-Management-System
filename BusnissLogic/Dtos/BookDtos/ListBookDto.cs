using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.BookDtos
{
    public class ListBookDto
    {
        [Key]

        public int BookId { get; set; } 
        public string Title { get; set; } = " ";
        public string ImageUrl { get; set; } = " ";
    }
}
