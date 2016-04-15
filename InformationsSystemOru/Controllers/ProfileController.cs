using Data_Access_Layer;
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
        private User_Post_CommentRepository userPostCommentRep = new User_Post_CommentRepository();
        private CommentRepository commentRep = new CommentRepository();

        // GET: Profile
        [Authorize]
        public ActionResult Profile()
        {
            var loggedInUser = accountRep.GetIdFromUsername(User.Identity.Name);
            var posts = postrepository.GetProfileBlogPosts(loggedInUser, postPostType.GetAllPrivatePostIds());
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
        public ActionResult Profile(BlogModel model)
        {
            var postingUserId = accountRep.GetIdFromUsername(User.Identity.Name);
            var posts = postrepository.GetProfileBlogPosts(postingUserId, postPostType.GetAllPrivatePostIds());
            var _model = new BlogModel();

            if (!ModelState.IsValid)
            {
                foreach (var p in posts)
                {
                    var commentIds = userPostCommentRep.GetPostCommentIds(p.Id);
                    var commentList = new List<Comment>();
                    var user = userRep.GetUserFromId(p.PostingUserID);

                    foreach (var id in commentIds)
                    {
                        commentList.Add(commentRep.GetComment(id));
                    }

                    model.AllPosts.Add(new PostModel
                    {
                        Category = p.Category,
                        DatePosted = p.Date,
                        Comments = commentList,
                        FileUrl = p.FileURL,
                        Filename = p.Filename,
                        PostId = p.Id,
                        Text = p.Text,
                        PostingUserId = p.PostingUserID,
                        PostingUsersName = user.Firstname + " " + user.Lastname,
                        Title = p.Titel
                    });
                }
                return View(model);
            }
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
        public ActionResult ChangeProfilePic(ProfileModel pModel)
        {
            if (pModel.File.ContentLength > 1 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "Bilden får inte vara större än 1MB");
                return View(pModel);
            }
            var fileExtension = Path.GetExtension(pModel.File.FileName);
            var imgTypes = new[] {".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG"};
            if (imgTypes.Contains(fileExtension))
            {
                MemoryStream target = new MemoryStream();
                pModel.File.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                var userId = userRep.GetIdFromUser(User.Identity.Name);
                userRep.UploadPic(data, userId);
                return RedirectToAction("ChangeProfilePic", "Profile");
            }
            ModelState.AddModelError("File", "Bilden måste vara .jpg, .jpeg, .png eller .gif");
            return View("ChangeProfilePic", pModel);


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