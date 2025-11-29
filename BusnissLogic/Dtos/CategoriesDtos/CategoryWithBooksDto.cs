using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.CategoriesDtos
{
    public class CategoryWithBooksDto
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]

        public string CategoryName { get; set; } = "";
        [MaxLength(1000)]
        public string? CategoryDescription { get; set; }
        public List<ListBookDto> Books { get; set; } = new();

    }
}
