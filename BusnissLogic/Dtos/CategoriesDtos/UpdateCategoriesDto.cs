using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.CategoriesDtos
{
    public class UpdateCategoriesDto
    {
        [Key]

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = "";
        public string? CategoryDescription { get; set; }

    }
}
