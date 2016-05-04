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
        public UserProfile TryLogin()
        {
            _logger.LogInformation("Anonymous login");

            return new UserProfile()
            {
                UserId = "anom",
                Name = "Anonymous User"
            };
        }

        //public User TryLogin()
        //{
        //    _logger.LogInformation("Anonymous login");

        //    return new User()
        //    {
        //        UserID = Guid.NewGuid(),
        //        Name = "Anonymous User"
        //    };
        //}

        // POST api/values
        [HttpPost("login")]
        public UserProfile Login([FromForm]string user, [FromForm]string password)
        {
            if(user == null)
            {
                // TODO: should return a valid type instead
                return null;
            }

            return new UserProfile()
            {
                UserId = "user",
                Name = "Authenticated user"
            };
        }
    }
}
