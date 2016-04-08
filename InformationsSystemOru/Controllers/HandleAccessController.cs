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
            var admins = accessRep.GetAdminIds();
            var aModel = new HandleAccessModel();

            if (accessRep.IsInformaticsAdmin(loggedInUser))
            {
                var ids = accessRep.GetUserIdsWithInformaticsAccess();
                aModel.UsersWithAccess = users.Where(x => ids.Contains(x.Id)).ToList();
                aModel.UsersWithoutAccess = users.Where(x => !ids.Contains(x.Id) && !admins.Contains(x.Id)).ToList();
            }
            else
            {
                var ids = accessRep.GetUserIdsWithResearchAccess();
                aModel.UsersWithAccess = users.Where(x => ids.Contains(x.Id)).ToList();
                aModel.UsersWithoutAccess = users.Where(x => !ids.Contains(x.Id) && !admins.Contains(x.Id)).ToList();
            }

            return View(aModel);
        }

        [HttpPost]
        public ActionResult GiveUserNewswritingAccess(HandleAccessModel haModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("HandleAccess");

            //Få ut id ur SelectedUser strängen.
            int index = haModel.SelectedUser.LastIndexOf(".");
            var Id = int.Parse(haModel.SelectedUser.Substring(0, index));
            var loggedInUser = userRep.GetUserFromId(accountRep.GetIdFromUsername(User.Identity.Name));

            if (accessRep.IsInformaticsAdmin(loggedInUser))
                accessRep.AddAccess(Id, 3);

            else
                accessRep.AddAccess(Id, 4);


            return RedirectToAction("HandleAccess");
        }

        [HttpPost]
        public ActionResult RemoveUserNewswritingAccess(HandleAccessModel haModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("HandleAccess");

            int index = haModel.SelectedUser.LastIndexOf(".");
            var Id = int.Parse(haModel.SelectedUser.Substring(0, index));
            var loggedInUser = userRep.GetUserFromId(accountRep.GetIdFromUsername(User.Identity.Name));

            if (accessRep.IsInformaticsAdmin(loggedInUser))
                accessRep.RemoveAccess(Id, 3);

            else
                accessRep.RemoveAccess(Id, 4);

            return RedirectToAction("HandleAccess");
        }
    }
}