using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace E_CommerceProject.Business.Contactus
{
    public class ContactUs
    {



        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Fashion Store", toEmail));
            message.To.Add(new MailboxAddress("Recipient Name", "nadinenabil0000@gmail.com"));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };


            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("nadinekhalil192@gmail.com", "qyhw uivc ough vwgk");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }


    }
}
