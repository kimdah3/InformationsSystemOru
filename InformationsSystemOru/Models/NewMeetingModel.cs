using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{
    public class NewMeetingModel
    {
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Enter a date!")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Enter a Location!")]
        [Display (Name = "Location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Enter a Type!")]
        [Display(Name = "Type")]
        public string Type { get; set; }
    }
}