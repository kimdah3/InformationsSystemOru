﻿using System;
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
            var posts = postrepository.GetAllInformalPosts();
            return View(new BlogModel { AllPostsForUser = posts });
        }

        [Authorize]
        public ActionResult ScienceBlog()
        {
            return View();
        }

        [Authorize]
        public ActionResult EducationBlog()
        {
            //    var loggedInUser = accountRep.GetIdFromUsername(User.Identity.Name);
            var posts = postrepository.GetAllEducationPosts();
            return View(new BlogModel { AllPostsForUser = posts });
           
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

    }
}