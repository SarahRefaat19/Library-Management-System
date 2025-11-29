using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DataAccessLayer.Repositories
{
    public class LibrarianRepository :ILibrarianRepository
    {
        private readonly UserManager<User> _userManager;
        public LibrarianRepository(UserManager<User> userManager) 
        {
            _userManager= userManager ;
        }

      
        public async Task CreateAsync(User Username, string Password)
        {
            await _userManager.CreateAsync(Username, Password); 

        }
        public async Task AddToRoleAsync(User user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);

        }

        public async  Task<Librarian?> GetByIdAsync(string id)
        {
            return await _userManager.Users.OfType<Librarian>().FirstOrDefaultAsync(l => l.Id == id);
            
        }

        public async Task<IEnumerable<Librarian>> GetAllAsync()
        {
            return await _userManager.Users.OfType<Librarian>().ToListAsync();

        }


    }
}
