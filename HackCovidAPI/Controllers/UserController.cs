using HackCovidAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HackCovidAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        UserDataContext userDataContext;
        public UserController()
        {
            userDataContext = new UserDataContext();
        }

        [HttpGet]
        [Route("covid/user/GetUserData")]
        public UserModel GetUserData(string email)
        {
            return userDataContext.GetUserData(email);
        }
    }
}