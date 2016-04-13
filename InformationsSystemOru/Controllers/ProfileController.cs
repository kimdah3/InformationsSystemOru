﻿using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationsSystemOru.Controllers
{
    public class ProfileController : Controller
    {
        private UserRepository userRep = new UserRepository();
        private PostRepository postrepository = new PostRepository();
        private AccountRepository accountRep = new AccountRepository();
        private Post_PostTypeRespository postPostType = new Post_PostTypeRespository();

        // GET: Profile
        [Authorize]
        public ActionResult Profile()
        {
            var loggedInUser = accountRep.GetIdFromUsername(User.Identity.Name);
            var posts = postrepository.GetProfileBlogPosts(loggedInUser, postPostType.GetAllPrivatePostIds());

            return View(new BlogModel { AllPostsForUser = posts });
        }


        [HttpPost]
        public ActionResult Profile(BlogModel model)
        {
            var postingUserId = accountRep.GetIdFromUsername(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                var posts = postrepository.GetProfileBlogPosts(postingUserId, postPostType.GetAllPrivatePostIds());
                model.AllPostsForUser = posts;
                return View(model);
            }
            string fileName = null;
            string path = null;

          if(model.File != null) { 
            if (model.File.ContentLength > 0)
            {
                fileName = Path.GetFileName(model.File.FileName);
                path = Path.Combine(Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName));
                model.File.SaveAs(path);
            }
            }

            var post = new Post()
            {
                Category = model.Category,
                Date = DateTime.Now,
                Titel = model.Title,
                Text = model.Text,
                PostingUserID = postingUserId,
                FileURL = path,
                Filename = fileName
            };
        
            int type = model.Type;
            postrepository.SavePost(post, type);
            postPostType.SavePosttype(post.Id, 1);

           
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public ActionResult GetFile(string path)
        {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));

        }


        //public ActionResult ProfilePostResult(int userID)
        //{
        //    postrepository.
        //    List<Post> = 
        //    var model = new BlogModel();
        //    model.Category = post.Category;
        //    model.DatePosted = post.Date;
        //    model.Title = post.Titel;
        //    model.Text = post.Text;
        //    return View(model);
        //}
    }
}