using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.CategoriesDtos
{
    public class ReadCategoriesDto
    {
        [Key]

        public int CategoryId { get; set; }
        [Required]

        public string CategoryName { get; set; } = "";
        [MaxLength(1000)]
        public string? CategoryDescription { get; set; }
    }
}
