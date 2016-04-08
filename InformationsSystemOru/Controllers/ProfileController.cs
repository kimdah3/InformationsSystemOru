using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationsSystemOru.Controllers
{
    public class ProfileController : Controller
    {
        private PostRepository postrepository;
        private AccountRepository accountRep = new AccountRepository();

        // GET: Profile
        public new ActionResult Profile()
        {
            return View();
        }

        public ProfileController()
        {
            postrepository = new PostRepository();

        }

        [HttpPost]
        public ActionResult Profile(BlogModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var postingUserId = accountRep.GetIdFromUsername(User.Identity.Name);
            var post = new Post()
            {
                Category = model.Category,
                Date = DateTime.Now,
                Titel = model.Title,
                Text = model.Text,
                PostingUserID = postingUserId

            };
            int type = model.Type;
            postrepository.SavePost(post, type);

            return View();


        }
    }
}