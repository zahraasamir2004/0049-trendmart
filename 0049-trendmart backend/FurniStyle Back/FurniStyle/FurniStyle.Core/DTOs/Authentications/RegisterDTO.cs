using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Authentications
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email is Required .. !!")]
        [EmailAddress]
        public string Email { get; set; }

        public string DisplayName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
