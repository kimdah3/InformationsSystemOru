﻿using InformationsSystemOru.Models;
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



        public ActionResult Index()
        {       
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            var accountRepository = new AccountRepository();
            if (!accountRepository.Exists(model.Username, model.Password))
            {

                return View(model);
            }
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Profile", "Profile", new RouteValueDictionary(new { username = model.Username }));
            }

        }

    }
}