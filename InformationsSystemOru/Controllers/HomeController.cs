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
            return View(new LoginModel());

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
            FormsAuthentication.SetAuthCookie(model.Username, true);
            return RedirectToAction("Profile", "Profile", new RouteValueDictionary(new { username = model.Username }));

        }

    }
}