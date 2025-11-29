using LibrarySystem.Domain.Entities;
using  LibrarySystem.BusnissLogic.Dtos.LibrarianDtos;


namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
    public interface  ILibrarianService
    {
        public Task<LibrarianReadDto?> GetByIdAsync(string id);

        public Task<IEnumerable<LibrarianReadDto>> GetAllAsync();
        public Task CreateAsync(LibrarianCreateDto Username, string Password);
        public Task AddToRoleAsync(string id , string role);

    }
}

