using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class CreateUserModel
    {
        public string Userid { get; set; }


        [Display(Name = "Firstname")]
        [RegularExpression(@"[a-zA-ZäöåÄÖÅ]+", ErrorMessage = "Your first name can only contain letters (A - Z )")]
        [Required(ErrorMessage = "Enter a firstname")]
        public string Firstname { get; set; }


        [Display(Name = "Lastname")]
        [RegularExpression(@"[a-zA-ZäöåÄÖÅ]+", ErrorMessage = "Your last name must contain only letters (A - Z )")]
        [Required(ErrorMessage = "enter a lastname")]
        public string Lastname { get; set; }

        [Display(Name = "E-mail")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Enter an e-mail")]
        [Required(ErrorMessage = "Enter a valid e-mail")]
        public string Email { get; set; }


        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Your username must contain 6-30 letters")]
        [RegularExpression(@"^[a-zA-ZäöåÄÖÅ_0-9]*$", ErrorMessage = "Your username must contain only numbers and letters without spaces")]
        [Required(ErrorMessage = "Enter a username")]
        public string Username { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Type in new password")]
        [RegularExpression(@"^[a-zA-ZäöåÄÖÅ_0-9]*$", ErrorMessage = "Your password must contain only numbers and letters without spaces")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Your password must be between 6 - 30 characters long")]
        public string Password { get; set; }

        [Display(Name = "Enter access")]
        [Required(ErrorMessage = "Enter an access level")]
        public int Accesslevel { get; set; }

    }
    
}