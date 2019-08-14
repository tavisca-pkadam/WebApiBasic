using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("user")]
        [HttpGet]
        public ActionResult<User> GetUser()
        {
            var user = new User();
            user.firstName = "Pk";
            user.secondName = "afsd";
            user.age = 10;
            user.mobileNumber = "1000";
            return user;
        }

        [HttpPost]
        public ActionResult<User> PostUser([FromBody] User user)
        {
            Debug.WriteLine(ModelState);
            Debug.WriteLine(user.ToString());
            user.secondName = ModelState.ToString();
            user.firstName = "changed";
            return user;
        }

        //[HttpPut]
        //public ActionResult<User> PostUser([FromBody] User user)
        //{
        //    Debug.WriteLine(ModelState);
        //    Debug.WriteLine(user.ToString());
        //    user.secondName = ModelState.ToString();
        //    user.firstName = "changed";
        //    return user;
        //}
    }
}