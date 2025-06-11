using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Authentications
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Email is Required .. !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required .. !!")]
        public string Password { get; set; }
    }
}
