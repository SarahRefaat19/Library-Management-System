using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IbookRepository: IGenericRepository<Book>
    {

        Task<List<Book>> SearchBooksAsync(string keyword);
        Task<bool> IsAvailableBookAsync(int id);
    }
}
