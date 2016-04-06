using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationsSystemOru.Controllers
{
    public class HandleAccessController : Controller
    {
        private AccessRepository accessRep = new AccessRepository();
        private UserRepository userRep = new UserRepository();
        // GET: HandleAccess
        public ActionResult HandleAccess()
        {
            var ids = accessRep.GetUserIdsWithAccess();
            var users = userRep.GetAllUsers();
            var aModel = new HandleAccessModel();

            aModel.Users = users.Where(x => !ids.Contains(x.Id)).ToList();

            return View(aModel);
        }
    }
}