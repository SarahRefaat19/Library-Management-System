using LibrarySystem.BusnissLogic.Dtos.UserDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;

using AutoMapper;

using LibrarySystem.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace LibrarySystem.BusnissLogic.ServicesClasses
{

    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _Mapper;

        public UserService(UserManager<User> userManager ,IMapper mapper )
        {
         _Mapper = mapper;
         _userManager = userManager;
        }
                


       public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
        var Users = await _userManager.Users.ToListAsync();
            return _Mapper.Map<IEnumerable<UserReadDto>>( Users );
           
        }
        public async Task<UserReadDto?> GetUserByIdAsync(string id)
        {
            var User = await _userManager.FindByIdAsync(id);
            return _Mapper.Map<UserReadDto>(User);

        }
        public async Task<UserReadDto> AddUserAsync(UserCreateDto userCreateDto)
        {
            var user = _Mapper.Map<User>(userCreateDto);

            var CreatedUser = await _userManager.CreateAsync(user, userCreateDto.Password);

            await _userManager.AddToRoleAsync(user, userCreateDto.Role);

           return _Mapper.Map<UserReadDto>(user);

        }
        public async Task<UserReadDto?> UpdateUserAsync(UserUpdateDto UserUpdateDto)
        {
            var user = _Mapper.Map<User>(UserUpdateDto); 

            var UpdateUser = await _userManager.CreateAsync(user, UserUpdateDto.Password);

            await _userManager.AddToRoleAsync(user, UserUpdateDto.Role);

            return _Mapper.Map<UserReadDto>(user);

        }
        public async Task<UserReadDto?> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _Mapper.Map<UserReadDto>(user);  

        }



















    }
}
