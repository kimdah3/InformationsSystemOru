using System.Web.Mvc;
using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;


namespace InformationsSystemOru.Controllers
{ 

    public class AccountController : Controller
    {
        private UserRepository userRepository;

        public AccountController()
        {
            userRepository = new UserRepository();
     
        }



        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            var user = new User()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email

            };
            userRepository.AddUser(user);
            return View(model);
        }
        
    }
}