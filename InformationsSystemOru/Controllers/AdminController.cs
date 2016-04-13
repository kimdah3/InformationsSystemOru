using InformationsSystemOru.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationsSystemOru.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult Admin()
        {
            if (!User.Identity.IsAdmin())
                return RedirectToAction("Index", "Home");
            return View();
        }
    }
}