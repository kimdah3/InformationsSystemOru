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
         
            var x = new UserRepository();
            var y = new AccessRepository();
            var user = x.GetUserFromId(1);
            var user2 = x.GetUserFromId(2);

            var t = y.IsAdmin(user);
            var m = y.IsAdmin(user2);
            Console.WriteLine(t);
            Console.WriteLine(m);

       
            return View();
        }
        //[HttpPost]
        //public ActionResult Index(LoginModel model)
        //{

        //    if (!ModelState.IsValid)
        //        return View(model);
        //    var userRepository = new blblablalRepository();
        //    if (!userRepository.Exists(model.Username, model.Password))
        //    {

        //        return View(model);
        //    }
        //    {
        //        FormsAuthentication.SetAuthCookie(model.Username, false);
        //        return RedirectToAction("blablablaView", "Blablabla", new RouteValueDictionary(new { username = model.Username }));
        //    }

        //}

    }
}