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
        private AccountRepository accountRep = new AccountRepository();
        private Post_PostTypeRespository postPostType = new Post_PostTypeRespository();
        private PostRepository postrepository = new PostRepository();
        
        // GET: Blog
        [Authorize]
        public ActionResult InformalBlog()
        {
            return View();
        }

        [Authorize]
        public ActionResult ScienceBlog()
        {
            return View();
        }

        [Authorize]
        public ActionResult EducationBlog()
        {
            var loggedInUser = accountRep.GetIdFromUsername(User.Identity.Name);
            //var posts = postrepository.GetProfileBlogPosts(loggedInUser, postPostType.GetAllPrivatePostIds());
            //return View(new BlogModel { AllPostsForUser = posts });
            return View();
        }

        [HttpPost]
        public ActionResult EducationBlog(BlogModel model)
        {
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
            postPostType.SavePosttype(post.Id, 3);
            return RedirectToAction("Educationblog");
        }

        //public ActionResult SavePost(BlogModel model)
        //{
        //    var post = new Post()
        //    {
        //        Category = model.Category,
        //        Date = model.DatePosted,
        //        Titel = model.Title,
        //        Text = model.Text,
        //        PostingUserID = model.UserId

        //    };
        //    postrepository.SavePost(post);

        //    var post_posttype = new Post_PostType()
        //    {
        //        PostType = model.asd,
        //        PostId = post.PostingUserID

        //    };
        //    postrepository.SavePost()
        //    return View(model);


        //}

        //model.Category = post.Category;
        //model.DatePosted = post.Date;
        //model.Title = post.Titel;
        //model.Text = post.Text;
    }
}