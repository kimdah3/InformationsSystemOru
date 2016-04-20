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
        private UserRepository userRep = new UserRepository();
        private PostTypeRepository _postTypeRepository = new PostTypeRepository();
        private Post_PostTypeRespository _postPostTypeRepository = new Post_PostTypeRespository();

        public BlogModel LoadPosts(List<Post> postList)
        {
            var model = new BlogModel();
            model.AllPosts = new List<PostModel>();

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
                    CommentCount = comments.Count()
                });
            }

            model.PostTypes = _postTypeRepository.GetAllPostTypes();

            return model;
        }

        [HttpPost]
        public ActionResult PostComment(int postId, string tbComment)
        {
            var loggedInUser = userRep.GetUserFromId(accountRep.GetIdFromUsername(User.Identity.Name));
            var newComment = new Comment
            {
                AuthorId = loggedInUser.Id,
                Date = DateTime.Now,
                Text = tbComment,
                Id = commentRep.GetNewId(),
                PostId = postId
            };

            commentRep.SaveComment(newComment);

            return PartialView("_CommentPartial", new CommentModel
            {
                Authorname = loggedInUser.Firstname + " " + loggedInUser.Lastname,
                Date = newComment.Date,
                Text = newComment.Text,
                CommentId = newComment.Id,
                AuthorId = loggedInUser.Id,
                PostId = postId,
                User = loggedInUser
            });
        }

        [HttpPost]
        public void RemoveComment(int postId, int commentId, int authorId)
        {
            commentRep.RemoveComment(commentId);
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

            foreach (var post in model.AllPosts)
            {
            }


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
        public ActionResult newsBlog()
        {
            var posts = postrepository.GetAllNewsPosts();
            var model = LoadPosts(posts);

            // TOO: Lägg till PostType för varje post. 

            return View(model);
        }

        [HttpPost]
        public ActionResult NewsBlog(BlogModel model)
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

            var postType =
                _postTypeRepository.GetPostTypeById(int.Parse(model.NewPost.PostTypeHolder.Substring(0,
                    model.NewPost.PostTypeHolder.LastIndexOf("."))));

            postrepository.SavePost(post);
            postPostType.SavePosttype(post.Id, postType.Id);
            return RedirectToAction("NewsBlog");
        }

        [Authorize]
        public ActionResult EducationBlog()
        {
            //    var loggedInUser = accountRep.GetIdFromUsername(User.Identity.Name);
            var posts = postrepository.GetAllEducationPosts();
            var model = LoadPosts(posts);

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
            else if (sida == "News")
            {
                return RedirectToAction("newsBlog");
            }
            else
                return RedirectToAction("ScienceBlog");

        }

    }
}