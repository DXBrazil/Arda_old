using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Authentication.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net;

namespace Arda.Authentication.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        //ILogger<UsersController> _logger;

        //public UsersController(ILogger<UsersController> logger)
        //{
        //    this._logger = logger;
        //}

//<<<<<<< HEAD
//        [HttpGet("trylogin")]
//        public UserProfile TryLogin()
//        {
//            _logger.LogInformation("Anonymous login");

//            return new UserProfile()
//            {
//                UserId = "anom",
//                Name = "Anonymous User"
//            };
//        }

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
//        [HttpPost("login")]
//        public UserProfile Login([FromForm]string user, [FromForm]string password)
//        {
//            if(user == null)
//            {
//                // TODO: should return a valid type instead
//                return null;
//            }

//            return new UserProfile()
//            {
//                UserId = "user",
//                Name = "Authenticated user"
//            };
//        }
//=======
        //[HttpGet("trylogin")]
        //public User TryLogin()
        //{
        //    _logger.LogInformation("Anonymous login");

        //    return new User()
        //    {
        //        UserID = Guid.NewGuid(),
        //        Name = "Anonymous User"
        //    };
        //}

        //[HttpPost("login")]
        //public User Login([FromForm]string Email, [FromForm]string Password)
        //{
        //    if (Email == null)
        //    {
        //        return null;
        //    }

        //    return new User()
        //    {
        //        UserID = Guid.NewGuid(),
        //        Name = "Authenticated user"
        //    };
        //}

        private AuthenticationContext _Context;

        public UsersController(AuthenticationContext Context)
        {
            _Context = Context;
        }

        [Route("verifyifemailexists")]
        public bool VerifyIfEmailExists(string Email)
        {
            // Verifying if requested email exists in the database.
            bool SearchResult = _Context.Users.Any(u => u.Email == Email);
            return SearchResult;
        }
    }
}
