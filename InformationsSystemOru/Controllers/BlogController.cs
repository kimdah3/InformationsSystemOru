using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer;
using InformationsSystemOru.Models;
using Data_Access_Layer.Repositories;
using System.IO;

namespace InformationsSystemOru.Controllers
{
    public class BlogController : Controller
    {
        private AccountRepository accountRep = new AccountRepository();
        private Post_PostTypeRespository postPostType = new Post_PostTypeRespository();
        private PostRepository postrepository = new PostRepository();
        private CommentRepository commentRep = new CommentRepository();
        private User_Post_CommentRepository userPostCommentRep = new User_Post_CommentRepository();
        private UserRepository userRep = new UserRepository();
        private string fileName = null;
        private string path = null;

        public BlogModel LoadPosts(List<Post> postList)
        {
            var model = new BlogModel();
            model.AllPosts = new List<PostModel>();

            foreach (var post in postList)
            {
                var commentIds = userPostCommentRep.GetPostCommentIds(post.Id);
                var commentList = new List<Comment>();
                var user = userRep.GetUserFromId(post.PostingUserID);

                foreach (var id in commentIds)
                {
                    commentList.Add(commentRep.GetComment(id));
                }

                model.AllPosts.Add(new PostModel
                {
                    Category = post.Category,
                    DatePosted = post.Date,
                    Comments = commentList,
                    FileUrl = post.FileURL,
                    Filename = post.Filename,
                    PostId = post.Id,
                    Text = post.Text,
                    PostingUserId = post.PostingUserID,
                    PostingUsersName = user.Firstname + " " + user.Lastname,
                    Title = post.Titel
                });
            }

            return model;
        }


        // GET: Blog
        [Authorize]
        public ActionResult InformalBlog()
        {
            var posts = postrepository.GetAllInformalPosts();
            var model = LoadPosts(posts);

            return View(model);
        }

        [Authorize]
        public ActionResult ScienceBlog()
        {
            var posts = postrepository.GetAllSciencePosts();
            var model = LoadPosts(posts);

            return View(model);
        }

        [HttpPost]
        public ActionResult ScienceBlog(BlogModel model)
        {
            var postingUserId = accountRep.GetIdFromUsername(User.Identity.Name);
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
            postPostType.SavePosttype(post.Id, 2);
            return RedirectToAction("Scienceblog");
        }

        [Authorize]
        public ActionResult EducationBlog()
        {
            //    var loggedInUser = accountRep.GetIdFromUsername(User.Identity.Name);
            var posts = postrepository.GetAllEducationPosts();
            var model = new BlogModel();
            model.AllPosts = new List<PostModel>();

            foreach (var post in posts)
            {
                var commentIds = userPostCommentRep.GetPostCommentIds(post.Id);
                var commentList = new List<Comment>();
                var user = userRep.GetUserFromId(post.PostingUserID);

                foreach (var id in commentIds)
                {
                    commentList.Add(commentRep.GetComment(id));
                }

                model.AllPosts.Add(new PostModel
                {
                    Category = post.Category,
                    DatePosted = post.Date,
                    Comments = commentList,
                    FileUrl = post.FileURL,
                    Filename = post.Filename,
                    PostId = post.Id,
                    Text = post.Text,
                    PostingUserId = post.PostingUserID,
                    PostingUsersName = user.Firstname + " " + user.Lastname,
                    Title = post.Titel
                });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EducationBlog(BlogModel model)
        {
            var postingUserId = accountRep.GetIdFromUsername(User.Identity.Name);

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
            postPostType.SavePosttype(post.Id, 3);
            return RedirectToAction("Educationblog");
        }

        [HttpPost]
        public ActionResult DeletePost(int postid, string sida)
        {
            postrepository.DeletePost(postid);
            if (sida == "Education")
            {
                return RedirectToAction("Educationblog");
            }
            else if (sida == "InformalBlog")
            {
                return RedirectToAction("InformalBlog");
            }
            else
                return RedirectToAction("ScienceBlog");

        }

    }
}