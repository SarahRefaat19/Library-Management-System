using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.Extensions.Caching.Memory;



namespace LibrarySystem.BusinessLogic.ServicesClasses
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitofWorks _unitOfWork;
        private readonly IMemoryCache _cache;

        public CategoryService(IUnitofWorks unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            const string cacheKey = "All_Categories";

            if (!_cache.TryGetValue(cacheKey, out List<Category>? categories))
            {
                categories = await _unitOfWork.Categories.GetAllAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, categories, cacheOptions);
            }

            return categories!;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetbyIdAsync(id);
            if (category == null)
                throw new Exception($"Category with ID {id} not found.");

            return category;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            if (await IsCategoryNameExistAsync(category.Name))
                throw new Exception("Category name already exists.");

            var added = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            _cache.Remove("All_Categories");
            return added;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var updated = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();

            _cache.Remove("All_Categories");
            return updated;
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetbyIdAsync(id);
            if (category == null)
                throw new Exception("Category not found.");

            var deleted = await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.CompleteAsync();

            _cache.Remove("All_Categories");
            return deleted;
        }

        public async Task<List<Category>> SearchCategoriesByNameAsync(string keyword)
        {
            var allCategories = await GetAllCategoriesAsync();
            return allCategories
                .Where(c => c.Name.StartsWith(keyword, StringComparison.OrdinalIgnoreCase))
                .Take(10)
                .ToList();
        }
        public async Task<List<Category>> GetCategoryWithBooksAsync(string keyword)
        {
            var allCategories = await _unitOfWork._Categories.GetCategoryWithBooksAsync(keyword);
            return allCategories;                
        }



        public async Task<bool> IsCategoryNameExistAsync(string name)
        {
            var allCategories = await GetAllCategoriesAsync();
            return allCategories.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<int> GetCategoriesCountAsync()
        {
            var categories = await GetAllCategoriesAsync();
            return categories.Count;
        }
    }
}
