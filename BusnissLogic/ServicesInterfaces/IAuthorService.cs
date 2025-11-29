using LibrarySystem.Domain.Entities;

namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> AddAuthorAsync(Author book);
        Task<Author> UpdateAuthorAsync(int id ,Author book);
        Task<Author> DeleteAuthorAsync(int id);
        Task<List<Author>> SearchAuthorsByNameAsync(string keyword);
        Task<IEnumerable<Author>> GetAuthorWithBooksByIdAsync(string keyword);


    }
}
