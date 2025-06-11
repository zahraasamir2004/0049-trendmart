using FurniStyle.Core.DTOs.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IServices
{
    public interface IUserService
    {
       Task<UserDTO> LoginAsync(LoginDTO loginDTO);
       Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
       Task<bool> CheckEmailExistAsync(string email);
       Task<List<UserDTO>> GetAllUsersAsync();
       Task<UserDTO> AddUserAsync(UserDTO userDTO, string password);
       Task<UserDTO> UpdateUserAsync(UserDTO userDTO);
       Task<bool> DeleteUserAsync(string userId);
       Task<int> DeleteAllNonAdminsAsync();

    }
}
