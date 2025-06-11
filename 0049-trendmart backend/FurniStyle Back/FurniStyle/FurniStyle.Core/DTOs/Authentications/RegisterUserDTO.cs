using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Authentications
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public UserDTO ToUserDTO() => new UserDTO
        {
            Email = Email,
            DisplayName = DisplayName,
            PhoneNumber = PhoneNumber,
            Role = Role
        };
    }
}
