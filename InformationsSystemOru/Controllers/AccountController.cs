using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationsSystemOru.Models;

namespace InformationsSystemOru.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(CreateUserModel model)
        {
        //    users user = new users
        //    {
        //        firstname = cModel.Firstname,
        //        lastname = cModel.Lastname,
        //        email=cModel.Email

        //     };
        //    userRepository.AddUser(user);
            return View();
        }
        
    }
}