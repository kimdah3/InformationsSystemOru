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
        private AccountRepository accountRep = new AccountRepository();

        // GET: HandleAccess
        public ActionResult HandleAccess()
        {
            var namn = User.Identity.Name;
            var loggedInUser = userRep.GetUserFromId(accountRep.GetIdFromUsername(User.Identity.Name));
            var users = userRep.GetAllUsers();
            var aModel = new HandleAccessModel();

            if (accessRep.IsInformaticsAdmin(loggedInUser))
            {
                var ids = accessRep.GetUserIdsWithInformaticsAccess();
                aModel.UsersWithAccess = users.Where(x => ids.Contains(x.Id)).ToList();
                aModel.UsersWithoutAccess = users.Where(x => !ids.Contains(x.Id)).ToList();
            }
            else
            {
                var ids = accessRep.GetUserIdsWithResearchAccess();
                aModel.UsersWithAccess = users.Where(x => ids.Contains(x.Id)).ToList();
                aModel.UsersWithoutAccess = users.Where(x => !ids.Contains(x.Id)).ToList();
            }

            return View(aModel);
        }
    }
}