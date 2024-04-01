using E_CommerceProject.Business.Emails.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.Emails.Interfcaes
{
    public  interface IEmailService
    {
        void SendEmail(Message message);
    }
}
