using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.Emails.Dtos
{
    public class ResetPasswordModel
    {
        [Required]
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "The Pasword and confirmation password dont match")]
        public string ConfirmPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
