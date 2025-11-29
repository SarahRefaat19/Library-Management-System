using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.LibrarianDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.BusnissLogic.ServicesClasses
{
    public class LibrarianService :ILibrarianService
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IMapper _mapper;

        public LibrarianService(ILibrarianRepository librarianRepository, IMapper mapper)
        {
            _librarianRepository = librarianRepository;
            _mapper = mapper;
        }

        public async Task<LibrarianReadDto?> GetByIdAsync(string id)
        {
            var user = await _librarianRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            var librarianDto = _mapper.Map<LibrarianReadDto>(user);
            return librarianDto;
        }

        public async Task<IEnumerable<LibrarianReadDto>> GetAllAsync()
        {
            var librarians = await _librarianRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LibrarianReadDto>>(librarians);
        }

        public async Task CreateAsync(LibrarianCreateDto librarianDto, string password)
        {
            var user = _mapper.Map<User>(librarianDto); 
            await _librarianRepository.CreateAsync(user, password);
        }

        public async Task AddToRoleAsync(string id , string role)
        {
            var user = _mapper.Map<User>(id );
            await _librarianRepository.AddToRoleAsync(user, role);
        }
    }
}

   