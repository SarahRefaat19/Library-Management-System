using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DataAccessLayer.Repositories
{
    public class CategoriesRepository :IGenericRepository<Category>,ICategoryRepository
    {
        public LibraryDbContext _Context;
        public CategoriesRepository(LibraryDbContext context)   //Constructor for make system Apply Dependency Injection 
        {
            _Context = context;
        }



        public async Task<List<Category>> GetAllAsync() => await _Context.Categories.ToListAsync();


        public async Task<Category?> GetbyIdAsync(int id)
        {

            return await _Context.Categories.FindAsync(id);
        }


        public async Task<Category> AddAsync(Category category)
        {
            await _Context.Categories.AddAsync(category);
            await _Context.SaveChangesAsync();
            return category;
        }


        public async Task<Category> UpdateAsync(Category category)
        {
            _Context.Categories.Update(category);
            await _Context.SaveChangesAsync();
            return category;
        }



        public async Task<Category> DeleteAsync(Category category)
        {
            _Context.Categories.Remove(category);
            await _Context.SaveChangesAsync();
            return category;
        }

      public  async Task<List<Category>> GetCategoryWithBooksAsync(string keyword)
        {
           return await _Context.Categories 
                .Include(s=>s.Books)
                .Where(N=>N.Name.Contains(keyword))
                .ToListAsync();
        }

    }
}
