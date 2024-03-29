using E_CommerceProject.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Country { get; set; }
        public string PhoneNumber { get; set; }


    }
}
