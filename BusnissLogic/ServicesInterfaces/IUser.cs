using LibrarySystem.BusnissLogic.Dtos.LoanDtos;
using LibrarySystem.BusnissLogic.Dtos.UserDtos;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(string id);
        Task<UserReadDto> AddUserAsync(UserCreateDto userCreateDto);
        Task<UserReadDto?> UpdateUserAsync(UserUpdateDto UserUpdateDto);
        Task<UserReadDto?> GetByEmailAsync(string email);
    }
}
