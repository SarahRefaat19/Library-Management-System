using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.ReposInterfaces
{
    public interface ILibrarianRepository
    {
        Task<Librarian?> GetByIdAsync(string id);

        Task<IEnumerable<Librarian>> GetAllAsync();
        Task CreateAsync(User Username, string Password);
        Task AddToRoleAsync(User user, string role);


    }
}
