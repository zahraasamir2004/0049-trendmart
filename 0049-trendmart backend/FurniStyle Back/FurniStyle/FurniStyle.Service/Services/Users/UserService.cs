using Microsoft.AspNetCore.Identity;
using FurniStyle.Core.DTOs.Authentications;
using FurniStyle.Core.Entities.Identity;
using FurniStyle.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurniStyle.Core.IServices.Toekn;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FurniStyle.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager,
                           RoleManager<IdentityRole> roleManager,
                           ITokenService tokenService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return null;

            var roles = await _userManager.GetRolesAsync(user); // assumes roles are used to differentiate user/admin

            return new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault() ?? "user", // fallback if no role assigned
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            // Check if email already exists
            if (await CheckEmailExistAsync(registerDTO.Email)) return null;

            // Create new user
            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email.Split("@")[0]
            };

            // Register the user
            var registedUser = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!registedUser.Succeeded) return null;

            // Assign "user" role to the new user
            var roleExist = await _roleManager.RoleExistsAsync("user");
            if (roleExist)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }

            // Return user details along with the token
            return new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }


        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();

            var userList = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userList.Add(new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    PhoneNumber = user.PhoneNumber,
                    Role = roles.FirstOrDefault() ?? "user"
                });
            }

            return userList;
        }

        public async Task<UserDTO> AddUserAsync(UserDTO userDTO, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                DisplayName = userDTO.DisplayName,
                PhoneNumber = userDTO.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            // Assign role
            await _userManager.AddToRoleAsync(user, userDTO.Role ?? "user");

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Role = userDTO.Role
            };
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id);
            if (user == null) return null;

            user.DisplayName = userDTO.DisplayName;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.Email = userDTO.Email;
            user.UserName = userDTO.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return null;

            // Update role
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any()) await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, userDTO.Role ?? "user");

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Role = userDTO.Role
            };
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<int> DeleteAllNonAdminsAsync()
        {
            var users = _userManager.Users.ToList();
            int deletedCount = 0;

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("admin", StringComparer.OrdinalIgnoreCase))
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded) deletedCount++;
                }
            }

            return deletedCount;
        }

    }
}
