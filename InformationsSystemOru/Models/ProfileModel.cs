using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace InformationsSystemOru.Models
{
    public class ProfileModel
    {
         
        [Display(Name = "Profilbild")]
        [DataType(DataType.Upload)]
        [FileExtensions()]
        [Required(ErrorMessage = "Välj en bild.")]
        public HttpPostedFileBase File { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
