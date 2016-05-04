using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common;
using Arda.Common.Email;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using Arda.Authentication.Models;
using Arda.Authentication.Interfaces;

namespace Arda.Athentication.Repository.Emails
{
    public class EmailRepository : IEmailRepository
    {
        private AuthenticationContext _context;

        public EmailRepository(AuthenticationContext context)
        {
            _context = context;
        }

        public bool SendEmailRequestNewAccount(string Name, string Email, string Phone, string Message)
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
            StructureModified.Replace("[MessageTitle]", "Hi " + ToName + ", someone requested access to the system");

            // Replacing the generic subtitle by the customized.
            StructureModified.Replace("[MessageSubtitle]", "Who did the request was <strong>" + Name + "</strong>. Some details about him follows.");

            // Replacing the generic message body by the customized.
            StructureModified.Replace("[MessageBody]", "Details about the request: </br></br><ul><li>Name: " + Name + "</li><li>Email: " + Email + "</li><li>Phone number: " + Phone + "</li><li>Justification: " + Message + "</li></ul>");

            // Replacing the generic callout box.
            StructureModified.Replace("[MessageCallout]", "For more details about the request, send a message to <strong>arda@microsoft.com</strong>.");

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

        public bool SendHelpRequest(string RequestType)
        {
            StringBuilder StructureModified = new StringBuilder();
            StructureModified = EmailMessages.GetEmailMessageStructure();

            // Creating a object that will send the message.
            EmailLogic EmailObject = new EmailLogic();

            string FromName;
            string FromEmail;
            string ToName;
            string ToEmail;
            string Subject;

            if (!VerifyIfIsEmail(RequestType))
            {
                // Mounting parameters and message.
                FromName = "Arda Team";
                FromEmail = "arda@microsoft.com";
                ToName = "Arda Administrator";
                ToEmail = "fabsanc@microsoft.com";
                Subject = "[ARDA] New help request";

                // Replacing the generic title by the customized.
                StructureModified.Replace("[MessageTitle]", "Hi " + ToName + ", someone requested help to access the system");

                // Replacing the generic subtitle by the customized.
                StructureModified.Replace("[MessageSubtitle]", "How system admin, you must check the request and manually solve the problem");

                // Replacing the generic message body by the customized.
                StructureModified.Replace("[MessageBody]", "Details about the request: <br /><br />" + RequestType);

                // Replacing the generic callout box.
                StructureModified.Replace("[MessageCallout]", "<strong>Observation</strong><br /><ul><li>Name<br />If you are seeing a Name above, it means that user has a problem with his email. In this case, you must manually verify what is happening and solve the problem.</li><li>Description<br />If the message above shows a descriptive text, you must treat the problem correctly.</li></ul>");

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
            else
            {
                //If email is registred...
                if (VerifyIfEmailExists(RequestType))
                {
                    // Mounting parameters and message.
                    FromName = "Arda Team";
                    FromEmail = "arda@microsoft.com";
                    ToName = RequestType;
                    ToEmail = RequestType;
                    Subject = "[ARDA] Password reset";

                    // Replacing the generic title by the customized.
                    StructureModified.Replace("[MessageTitle]", "Hi " + ToName + ", you have been requested a password reset, right?");

                    // Replacing the generic subtitle by the customized.
                    StructureModified.Replace("[MessageSubtitle]", "Your request to reset password is here");

                    // Replacing the generic message body by the customized.
                    StructureModified.Replace("[MessageBody]", "To reset your password, please click on the link below: <br /><a href='/ClientAuthentication/ResetPassword/" + RequestType + "'>Reset passoword</a>");

                    // Replacing the generic callout box.
                    StructureModified.Replace("[MessageCallout]", "For more details about the request, send a message to <strong>arda@microsoft.com</strong>.");

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
                else
                {
                    return false;
                }
            } 
        }

        private bool VerifyIfIsEmail(string RequestType)
        {
            bool isEmail = Regex.IsMatch(RequestType, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }

        private bool VerifyIfEmailExists(string Email)
        {
            // Verifying if requested email exists in the database.
            bool SearchResult = _context.Users.Any(u => u.Email == Email);
            return SearchResult;
        }
    }
}
