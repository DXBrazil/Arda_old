using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Authentication.Models;
using Microsoft.Extensions.Logging;

namespace Arda.Authentication.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("trylogin")]
        public User TryLogin()
        {
            _logger.LogInformation("Anonymous login");

            return new User()
            {
                UserID = Guid.NewGuid(),
                Name = "Anonymous User"
            };
        }

        [HttpPost("login")]
        public User Login([FromForm]string Email, [FromForm]string Password)
        {
            if (Email == null)
            {
                return null;
            }

            return new User()
            {
                UserID = Guid.NewGuid(),
                Name = "Authenticated user"
            };
        }

    }
}
