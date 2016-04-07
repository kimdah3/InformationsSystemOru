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

        // GET: Profile
        public new ActionResult Profile()
        {
            return View();
        }

        public ProfileController()
        {
            postrepository = new PostRepository();

        }

        public ActionResult SaveThePost(BlogModel model)
        {
            var post = new Post()
            {
                Category = model.Category,
                Date = model.DatePosted,
                Titel = model.Title,
                Text = model.Text,
                PostingUserID = model.UserId
                

            };
            int type = model.Type;
            postrepository.SavePost(post, type);

            return View(model);


        }
    }
}