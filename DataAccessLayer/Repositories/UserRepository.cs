using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace LibrarySystem.DataAccessLayer.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly UserManager <User> userManager;
        public UserRepository(UserManager<User> _userManager) {
            userManager = _userManager;
        }

       public async  Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var allUsers = await userManager.Users.ToListAsync();
            return allUsers;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email); 
            return User;
        }
        public async Task<User?> GetByIdAsync(string id)
        {
            var User = await userManager.FindByIdAsync(id); 
            return User;
        }
        public async Task CreateAsync(User Username, string Password)
        {
              await userManager.CreateAsync( Username ,Password);

        }
        public async Task AddToRoleAsync(User user, string role)
        {
           await userManager.AddToRoleAsync(user, role); 

        }


    }
}
