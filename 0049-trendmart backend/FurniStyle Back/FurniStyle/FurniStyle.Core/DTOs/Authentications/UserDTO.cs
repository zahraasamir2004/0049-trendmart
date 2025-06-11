using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Authentications
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } // or use Type if you prefer
        public string Token { get; set; }
    }
}
