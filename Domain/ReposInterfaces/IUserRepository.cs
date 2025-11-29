using Azure.Identity;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IUserRepository
    {
         Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync( string id);
        Task CreateAsync( User Username , string Password);
        Task AddToRoleAsync(User user, string role);


    }
}
