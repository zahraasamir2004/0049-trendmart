using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using FurniStyle.Core.IServices.Toekn;
using FurniStyle.Core.Entities.Identity;

namespace FurniStyle.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var authCliams = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
            };
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var userRoles =await userManager.GetRolesAsync(user);
            foreach (var role in userRoles) 
            {
                authCliams.Add(new Claim(ClaimTypes.Role, role)); 
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpireDateInDays"])),
                claims: authCliams,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)

             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
