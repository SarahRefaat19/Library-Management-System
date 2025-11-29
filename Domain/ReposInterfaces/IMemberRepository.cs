using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.ReposInterfaces
{
    public interface IMemberRepository
    {

        Task <IEnumerable<Member>> GetAllMembersAsync ();
        Task<User?> GetByIdAsync(string id);

        Task<List<Member>> GetActiveMembersAsync ();
        Task UpdateFineAsync (string id , decimal amount);
        Task CreateAsync(User Username, string Password);
        Task AddToRoleAsync(User user, string role);


    } 
}
