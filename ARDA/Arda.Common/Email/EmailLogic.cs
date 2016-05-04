using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace Arda.Common.Email
{
    public class EmailLogic
    {
        public async Task SendEmailAsync(string FromName, string FromEmail, string ToName, string ToEmail, string Subject, string Body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(FromName, FromEmail));
            message.To.Add(new MailboxAddress(ToName, ToEmail));
            message.Subject = Subject;

            var Builder = new BodyBuilder();

            //message.Body = new TextPart("plain")
            //{
            //    Text = Body
            //};

            Builder.HtmlBody = Body;
            message.Body = Builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.sendgrid.net", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("azure_4ef2ae00a532aaf67c72e8a105e71f8c@azure.com", "arda0987");
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
