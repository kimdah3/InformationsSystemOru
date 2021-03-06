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
        private CommentRepository commentRep = new CommentRepository();

        public BlogModel LoadPosts(List<Post> postList)
        {
            var model = new BlogModel();
            model.AllPosts = new List<PostModel>();
            model.loggedInUserId = accountRep.GetIdFromUsername(User.Identity.Name);

            foreach (var post in postList)
            {
                var comments = commentRep.GetPostComments(post.Id);
                var user = userRep.GetUserFromId(post.PostingUserID);

                foreach (var c in comments)
                    c.User = userRep.GetUserFromId((int)c.AuthorId);

                model.AllPosts.Add(new PostModel
                {
                    Category = post.Category,
                    DatePosted = post.Date,
                    Comments = comments,
                    FileUrl = post.FileURL,
                    Filename = post.Filename,
                    PostId = post.Id,
                    Text = post.Text,
                    PostingUserId = post.PostingUserID,
                    PostingUsersName = user.Firstname + " " + user.Lastname,
                    Title = post.Titel,
                    CommentCount = comments.Count(),
                    PostingUsersProfilePicture = user.ProfilePicture == null ? "" : string.Format("data:image/png;base64,{0}", Convert.ToBase64String(user.ProfilePicture))
                });
            }

            return model;
        }

        // GET: Profile
        [Authorize]
        public ActionResult Profile()
        {
            var loggedInUser = userRep.GetUserFromId(accountRep.GetIdFromUsername(User.Identity.Name));
            var posts = postrepository.GetProfileBlogPosts(loggedInUser.Id, postPostType.GetAllPrivatePostIds());

            var model = new ProfileModel
            {
                AllPosts = LoadPosts(posts).AllPosts,
                Email = loggedInUser.Email,
                Firstname = loggedInUser.Firstname, 
                Lastname = loggedInUser.Lastname
            };

            if (loggedInUser.ProfilePicture == null)
                model.ProfilePicture = "";
            else
                model.ProfilePicture = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(loggedInUser.ProfilePicture));

            return View(model);
        }


        [HttpPost]
        public ActionResult Profile(BlogModel model)
        {
            var postingUserId = accountRep.GetIdFromUsername(User.Identity.Name);
            var posts = postrepository.GetProfileBlogPosts(postingUserId, postPostType.GetAllPrivatePostIds());
            string fileName = null;
            string path = null;
            

            if (model.NewPost.File != null)
            {
                fileName = model.NewPost.File.FileName;
                path = Path.Combine(Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName));
                model.NewPost.File.SaveAs(path);
            }

            var post = new Post()
            {
                Category = model.NewPost.Category,
                Date = DateTime.Now,
                Titel = model.NewPost.Title,
                Text = model.NewPost.Text,
                PostingUserID = postingUserId,
                FileURL = path,
                Filename = fileName
            };

            postrepository.SavePost(post);
            postPostType.SavePosttype(post.Id, 1);

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public ActionResult GetFile(string path)
        {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }

        [HttpGet]
        public ActionResult ChangeProfilePic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeProfilePic(ProfilePictureModel pModel)
        {
            if (pModel.File.ContentLength > 1 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "Bilden får inte vara större än 1MB");
                return View(pModel);
            }
            var fileExtension = Path.GetExtension(pModel.File.FileName);
            var imgTypes = new[] { ".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG" };
            if (imgTypes.Contains(fileExtension))
            {
                MemoryStream target = new MemoryStream();
                pModel.File.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                var userId = userRep.GetIdFromUser(User.Identity.Name);
                userRep.UploadPic(data, userId);
                return RedirectToAction("Profile", "Profile");
            }
            ModelState.AddModelError("File", "Bilden måste vara .jpg, .jpeg, .png eller .gif");
            return View("ChangeProfilePic", pModel);
        }

        public ActionResult VisitingProfile(int visitedUserID)
        {
            
            var posts = postrepository.GetProfileBlogPosts(visitedUserID, postPostType.GetAllPrivatePostIds());

            var model = new VisitingUserModel
            {
                UserPosts = LoadPosts(posts).AllPosts,
                VisitedUser = userRep.GetUserFromId(visitedUserID),
                loggedInUserId = accountRep.GetIdFromUsername(User.Identity.Name)
            };

            if (model.VisitedUser.ProfilePicture == null)
                model.ProfilePicture = "";
            else
                model.ProfilePicture = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(model.VisitedUser.ProfilePicture));

            return View(model);
        } 


    }
}