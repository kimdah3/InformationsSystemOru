using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{
    public class MeetingsModel
    {
        public string Date { get; set; }
        public List<Meeting> Meetings { get; set; }
        public List<User> AllUsers { get; set; }
        public List<User> InvitedUsers { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Enter a time!")]
        //[DataType(DataType.DateTime)]
        public string TimeOfDay { get; set; }
        [Required(ErrorMessage = "Enter a Location!")]
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Enter a Type!")]
        [Display(Name = "Type")]
        public string Type { get; set; }

    }
}