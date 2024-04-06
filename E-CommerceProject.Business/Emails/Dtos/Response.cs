using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.Emails.Dtos
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string Token { get; set; }

        public string Email { get; set; }
    }
}
