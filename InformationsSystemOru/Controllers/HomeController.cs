using InformationsSystemOru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Extensions;

namespace InformationsSystemOru.Controllers
{
    public class HomeController : Controller
    {
        private UserRepository userRepository = new UserRepository();
        private AccountRepository accountRepository = new AccountRepository();


        [Authorize]
        public ActionResult Index()
        {
            var model = new BlogModel();
            var postRepo = new PostRepository();
            var commentRepo = new CommentRepository();
            var accountRepo = new AccountRepository();
            var newsposts = postRepo.GetAllNewsPosts();
            model.AllPosts = new List<PostModel>();

            foreach (var x in newsposts)
            {
                var postmodel = new PostModel()
                {
                    PostId = x.Id,
                    Text = x.Text,
                    Category = x.Category,
                    DatePosted = x.Date,
                    PostingUserId = x.PostingUserID,
                    PostingUsersName = accountRepo.GetUserNameFromId(x.PostingUserID),
                    Title = x.Titel,
                    Type = 4
                };

                model.AllPosts.Add(postmodel);
            }
            

            

            return View(model);
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!accountRepository.Exists(model.Username, model.Password))
            {
                model.ErrorMessage = "Wrong username or password";
                return View(model);
            }
            FormsAuthentication.SetAuthCookie(model.Username, false);
            return RedirectToAction("Profile", "Profile", new RouteValueDictionary(new { username = model.Username }));
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {

            var message2 = "Don't reply to this mail";
            if (ModelState.IsValid)
            {
                var userID = accountRepository.GetIdFromUsername(User.Identity.Name);
                var emailFrom = userRepository.GetUserFromId(userID);
                var name = emailFrom.Firstname + " " + emailFrom.Lastname;
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>{3}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.ToEmail)); // replace with valid value 
                message.From = new MailAddress("orusystemdeluxe@outlook.com"); // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, name, emailFrom.Email, model.Message, message2);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "orusystemdeluxe@outlook.com", // replace with valid value
                        Password = "Kattkattkatt1" // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }


        public ActionResult Sent()
        {
            return View();
        }
    }
}