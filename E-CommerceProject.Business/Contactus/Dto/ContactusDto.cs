using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.Contactus.Dto
{
    public  class ContactusDto
    {
        public string ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }

    }
}
