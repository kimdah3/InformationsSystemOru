using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer;
using InformationsSystemOru.Models;
using Data_Access_Layer.Repositories;

namespace InformationsSystemOru.Controllers
{
    public class BlogController : Controller
    {
        private PostRepository postrepository;
        // GET: Blog

        public BlogController()
        {
            postrepository = new PostRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InformalBlog()
        {

            return View();
        }

        public ActionResult SavePost(BlogModel model)
        {
            var post = new Post()
            {
                Category = model.Category,
                Date = model.DatePosted,
                Titel = model.Title,
                Text = model.Text,
                PostingUserID = model.UserId

            };
            postrepository.SavePost(post);

            var post_posttype = new Post_PostType()
            {
                PostType = model.asd,
                PostId = post.PostingUserID

            };
            postrepository.SavePost()
            return View(model);


        }

        //model.Category = post.Category;
        //model.DatePosted = post.Date;
        //model.Title = post.Titel;
        //model.Text = post.Text;
    }
}