using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;


namespace LibrarySystem.Domain.ReposInterfaces
{
    public interface ICategoryRepository
    {
       Task<List<Category>> GetCategoryWithBooksAsync(string keyword);

    }
}
