using LibrarySystem.BusnissLogic.Dtos.MemberDtos;
using LibrarySystem.Domain.Entities;


namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberReadDto>> GetAllMembersAsync();
        Task<List<MemberReadDto>> GetActiveMembersAsync();
        Task UpdateFineAsync(string id, decimal amount);
        Task CreateAsync(MemberCreateDto Username, string Password);
        Task AddToRoleAsync(string id , string role);

    }
}
