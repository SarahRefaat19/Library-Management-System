using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.CategoriesDtos
{
    public class DeleteCategoryDto
    {
        [Key]
        public int  Id { get; set; }

        public string? Name { get; set; }

    }
}
