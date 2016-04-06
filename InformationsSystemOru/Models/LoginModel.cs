using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class LoginModel
    {

        
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Du måste ange ett användarnamn!")]
        public string Username { get; set; }
        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
        
    }
}