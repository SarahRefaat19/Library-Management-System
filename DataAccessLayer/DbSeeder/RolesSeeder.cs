using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace LibrarySystem.DataAccessLayer.DbSeeder
{
    public class RolesSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesSeeder(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            


            if (!await _roleManager.RoleExistsAsync("Librarian"))
            
                await _roleManager.CreateAsync(new IdentityRole("Librarian"));


            if (!await _roleManager.RoleExistsAsync("Member"))
                await _roleManager.CreateAsync(new IdentityRole("Member"));



            if (await _userManager.FindByEmailAsync("sararefaat653@gmail.com") == null)
            {
                var Admin = new User
                {
                    UserName = "sararefaat",
                    Email = "sararefaat653@gmail.com",
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(Admin, "Sararefaat@123");

                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

                await _userManager.AddToRoleAsync(Admin, "Admin");

            }

                if (await _userManager.FindByEmailAsync("arwarefaat123@gmail.com") == null)
                {
                var Librarian = new User
                {
                    UserName = "arwarefaat",
                    Email = "arwarefaat123@gmail.com",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(Librarian, "Arwarefaat@123");

                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));


                await _userManager.AddToRoleAsync(Librarian, "Librarian");
                }



            if (await _userManager.FindByEmailAsync("member@example.com") == null)
            {
                var Member = new User
                {
                    UserName = "member1",
                    Email = "member@example.com",
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(Member, "Member@123");
                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                await _userManager.AddToRoleAsync(Member, "Member");
            }
        }
    }
}

    

