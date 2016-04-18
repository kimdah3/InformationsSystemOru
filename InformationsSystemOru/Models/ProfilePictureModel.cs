using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class ProfilePictureModel
    {
        [Display(Name = "Profilbild")]
        [DataType(DataType.Upload)]
        [FileExtensions()]
        [Required(ErrorMessage = "Välj en bild.")]
        public HttpPostedFileBase File { get; set; }

    }
}