using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using MailKit;
using System.Text.RegularExpressions;
using Arda.Athentication.Repository.Emails;
using Arda.Authentication.ViewModels.AccountOperations;
using Arda.Authentication.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Authentication.Controllers
{
    [Route("api/accountoperations")]
    public class AccountOperationsController : Controller
    {
        private IEmailRepository _emailRepository;

        public AccountOperationsController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpPost]
        [Route("requestnewaccount")]
        public IActionResult RequestNewAccount(RequestNewAccountViewModel viewModel)
        {
            // Call the post from message.
            if (_emailRepository.SendEmailRequestNewAccount(viewModel.Name, viewModel.Email, viewModel.Phone, viewModel.Justification))
            {
                return Json(new { Status = "Ok" });
            }
            else
            {
                return Json(new { Status = "Fail" });
            }
        }

        [HttpPost]
        [Route("requesthelp")]
        public IActionResult RequestHelp(string RequestType)
        {
            if (_emailRepository.SendHelpRequest(RequestType))
            {
                return Json(new { Status = "Ok" });
            }
            else
            {
                return Json(new { Status = "Fail" });
            }
        }
    }
}
