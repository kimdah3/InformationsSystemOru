using System.Web.Mvc;
using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;
using InformationsSystemOru.Extensions;
using Data_Access_Layer;
using System.Collections.Generic;

namespace InformationsSystemOru.Controllers
{ 

    public class AccountController : Controller
    {
        private UserRepository userRepository;
        private AccountRepository accountRepository;
        private AccessRepository accessRepository;

        public AccountController()
        {
            userRepository = new UserRepository();
            accountRepository= new AccountRepository();
            accessRepository = new AccessRepository();
        }

        [Authorize]
        public ActionResult Create()
        {
            if (!User.Identity.IsAdmin())
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email,


                };
                userRepository.AddUser(user);


                var account = new Account()
                {
                    UserID = user.Id,
                    Username = model.Username,
                    Password = model.Password

                };
                accountRepository.Addaccount(account);

                if (model.Accesslevel == 5)
                {
                    var userAccess1 = new User_Access()
                    {

                        UserId = user.Id,
                        AccessId = 3



                    };
                    accessRepository.CreateNewAccess(userAccess1);
                    var userAccess2 = new User_Access()
                    {
                        UserId = user.Id,
                        AccessId = 4

                    };
                    accessRepository.CreateNewAccess(userAccess2);
                }
                else
                {
                    var userAccess = new User_Access()
                    {

                        UserId = user.Id,
                        AccessId = model.Accesslevel,



                    };
                    accessRepository.CreateNewAccess(userAccess);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public PartialViewResult GetAmountOfMeetings()
        {
            var meetingRepo = new User_MeetingRepository();
            var accountRepo = new AccountRepository();
            var userRepo = new UserRepository();
            var userId = accountRepo.GetIdFromUsername(User.Identity.Name);
            var user = userRepo.GetUserFromId(userId);
            var model = new AmountOfMeetingInvitesModel();
            var list = new List<Meeting>();
            list = meetingRepo.GetInvitedToMeetingsByUserId(user);
            model.amountOfInvites = list.Count;
            return PartialView(model);
        }
    }
}