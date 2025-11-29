using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos; // مكان الدتوز بتاعتك
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.BusnissLogic.Dtos.MemberDtos; // مكان الدتوز بتاعتك

    namespace LibrarySystem.DataAccessLayer.Repositories
    {
        public class MemberRepository : IMemberRepository
        {
            private readonly UserManager<User> userManager;

            public MemberRepository(UserManager<User> _userManager)
            {
                userManager = _userManager;
            }

            public async Task<User?> GetByIdAsync(string id)
            {
                var User = await userManager.FindByIdAsync(id);
                return User;
            }

            public async Task<IEnumerable<Member>> GetAllMembersAsync()
            {
                return await userManager.Users.OfType<Member>().ToListAsync();
            }

            public async Task<List<Member>> GetActiveMembersAsync()
            {
                var activeMember = await userManager.Users
                    .OfType<Member>()
                    .Where(m => m.IsActive == true)
                    .ToListAsync();

                return activeMember;
            }

            public async Task UpdateFineAsync(string id, decimal amount)
            {
                var member = await userManager.Users
                    .OfType<Member>()
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (member == null)
                {
                    throw new Exception("Member not found");
                }
            if (amount <= 0)
                throw new Exception("Fine amount must be positive");


            member.FineBalance += amount;
                await userManager.UpdateAsync(member);
            }

            public async Task CreateAsync(User Username, string Password)
            {
                var result = await userManager.CreateAsync(Username, Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create user: {errors}");
                }
            }

            public async Task AddToRoleAsync(User user, string role)
            {
                var result = await userManager.AddToRoleAsync(user, role);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to add user to role: {errors}");
                }
            }
        }
    }

