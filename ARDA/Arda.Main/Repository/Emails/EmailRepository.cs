using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common;
using Arda.Common.Email;
using System.Text;

namespace Arda.Main.Repository.Emails
{
    public class EmailRepository
    {
        public static bool SendEmailRequestNewAccount(string Name, string Email, string Phone, string Message)
        {
            // Mounting parameters and message.
            string FromName = "Arda Team";
            string FromEmail = "arda@microsoft.com";
            string ToName = "Arda Administrator";
            string ToEmail = "fabsanc@microsoft.com";
            string Subject = "[ARDA] New system account request";

            StringBuilder StructureModified = new StringBuilder();
            StructureModified = EmailMessages.GetEmailMessageStructure();

            // Replacing the generic title by the customized.
            StructureModified.Replace("[MessageTitle]", "Hi " + ToName + ", someone requested access to Arda");

            // Replacing the generic subtitle by the customized.
            StructureModified.Replace("[MessageSubtitle]", "Who did the request was <strong>" + Name + "</strong>. Some details about him follows.");

            // Replacing the generic message body by the customized.
            StructureModified.Replace("[MessageBody]", "Details about the request: </br></br><ul><li>Name: " + Name + "</li><li>Email: " + Email + "</li><li>Phone number: " + Phone + "</li><li>Justification: " + Message + "</li></ul>");

            // Replacing the generic callout box.
            StructureModified.Replace("[MessageCallout]", "For more details about de request, send a message to <strong>arda@microsoft.com</strong>.");

            // Creating a object that will send the message.
            EmailLogic EmailObject = new EmailLogic();

            try
            {
                var EmailTask = EmailObject.SendEmailAsync(FromName, FromEmail, ToName, ToEmail, Subject, StructureModified.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
