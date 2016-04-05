using InformationsSystemOru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace InformationsSystemOru.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]

        public ActionResult Index(LoginModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            var userRepository = new blblablalRepository();
            if (!userRepository.Exists(model.Username, model.Password))
            {

                return View(model);
            }
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("blablablaView", "Blablabla", new RouteValueDictionary(new { username = model.Username }));
            }

        }

    }
}