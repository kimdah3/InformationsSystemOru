using InformationsSystemOru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Data_Access_Layer.Repositories;

namespace InformationsSystemOru.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        public ActionResult Index()
        {

            var x = new UserRepository();
            var y = new AccessRepository();
            var user = x.GetUserFromId(1);
            var user2 = x.GetUserFromId(2);
            var p = new UserRepository();
            var t = y.IsAdmin(user);
            var m = y.IsAdmin(user2);
            Console.WriteLine(t);
            Console.WriteLine(m);


            return View(new LoginModel());
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            var accountRepository = new AccountRepository();
            if (!accountRepository.Exists(model.Username, model.Password))
            {
                model.ErrorMessage = "Wrong username or password";
                return View(model);
            }
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Profile", "Profile", new RouteValueDictionary(new { username = model.Username }));
            }

        }

    }
}