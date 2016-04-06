using System.Web.Mvc;
using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;


namespace InformationsSystemOru.Controllers
{ 

    public class AccountController : Controller
    {
        private UserRepository userRepository;
        private AccountRepository accountRepository;

        public AccountController()
        {
            userRepository = new UserRepository();
            accountRepository= new AccountRepository();

     
        }



        public ActionResult Create()
        {
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
            }
            return View(model);
           
        }
    }
}