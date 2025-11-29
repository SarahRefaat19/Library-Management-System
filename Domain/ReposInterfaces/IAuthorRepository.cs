using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAuthorWithBooksByIdAsync(string keyword);
        Task<List<Author>> SearchByNameAsync(string name);


    }
}
