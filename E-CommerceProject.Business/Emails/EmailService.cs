﻿using E_CommerceProject.Business.Emails.Dtos;
using E_CommerceProject.Business.Emails.Interfcaes;
using MimeKit;
using MailKit.Net.Smtp;


namespace E_CommerceProject.Business.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;

        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private void Send(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                client.Send(emailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
    }
}
