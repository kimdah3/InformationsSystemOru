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


        [Display(Name = "Förnamn")]
        [RegularExpression(@"[a-zA-ZäöåÄÖÅ]+", ErrorMessage = "Ditt förnamn får endast innehålla bokstäver(A - Ö)")]
        [Required(ErrorMessage = "Ange ett Förnamn")]
        public string Firstname { get; set; }


        [Display(Name = "Efternamn")]
        [RegularExpression(@"[a-zA-ZäöåÄÖÅ]+", ErrorMessage = "Ditt efternamn får endast innehålla bokstäver(A - Ö)")]
        [Required(ErrorMessage = "Ange ett Efternamn")]
        public string Lastname { get; set; }

        [Display(Name = "E-mail")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Ange en e-mail")]
        [Required(ErrorMessage = "Ange en giltig e-mail")]
        public string Email { get; set; }


        [Display(Name = "Användarnamn")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ditt användarnamn måste vara mellan 6-30 tecken långt")]
        [RegularExpression(@"^[a-zA-ZäöåÄÖÅ_0-9]*$", ErrorMessage = "Ditt användarnamn får endast innehålla siffror och bokstäver utan blanksteg")]
        [Required(ErrorMessage = "Ange ett användarnamn")]
        public string Username { get; set; }


        [Display(Name = "Lösenord")]
        [Required(ErrorMessage = "Skriv in ett nytt lösenord")]
        [RegularExpression(@"^[a-zA-ZäöåÄÖÅ_0-9]*$", ErrorMessage = "Ditt lösenord får endast innehålla siffror och bokstäver utan blanksteg")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Ditt lösenord måste vara mellan 6-30 tecken långt")]
        public string Password { get; set; }

    }
    
}