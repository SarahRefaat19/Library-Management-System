using LibrarySystem.BusnissLogic.Dtos.RegisterDtos;
using LibrarySystem.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
namespace LibrarySystem.BusnissLogic.ServicesClasses
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<User> userManager, IConfiguration _configuration)
        {
            _userManager = userManager;
            configuration = _configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[] {
             new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.UserName),
             new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new Claim(ClaimTypes.NameIdentifier, user.Id)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: configuration["Jwt:issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(configuration["Jwt:DurationInMinutes"])),
                signingCredentials: cred);


            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public async Task<IdentityResult> RegisterUserAsync(Register register, string role)
        {
            var User = new User { UserName = register.UserName, Email = register.Email };

            var creation = await _userManager.CreateAsync(User, register.Password);

            if (creation.Succeeded)
            {
                await _userManager.AddToRoleAsync(User, role);
            }
            return creation;

        }


        public async Task<string?> LoginUserAsync(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {

                return GenerateJwtToken(user);
            }

            return null;
        }
    }
}