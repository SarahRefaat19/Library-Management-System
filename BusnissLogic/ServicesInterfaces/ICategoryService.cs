using LibrarySystem.Domain.Entities;

namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
    public interface ICategoryService
    {

        Task<List<Category>> GetAllCategoriesAsync();

        // 🔹 Get Category by Id
        Task<Category?> GetCategoryByIdAsync(int id);

        // 🔹 Add new Category
        Task<Category> AddCategoryAsync(Category category);

        // 🔹 Update existing Category
        Task<Category> UpdateCategoryAsync(int id ,Category category);

        // 🔹 Delete Category by Id
        Task<Category> DeleteCategoryAsync(int id);

        // 🔹 Search for categories by name or keyword
        Task<List<Category>> SearchCategoriesByNameAsync(string keyword);

        // 🔹 Check if category exists by name (used before add)
        Task<bool> IsCategoryNameExistAsync(string name);

        // 🔹 Get categories with paging
        Task<List<Category>> GetCategoryWithBooksAsync(string keyword);

        Task<int> GetCategoriesCountAsync();

    }
}
