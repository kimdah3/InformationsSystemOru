using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data_Access_Layer;
using System.ComponentModel.DataAnnotations;

namespace InformationsSystemOru.Models
{
    public class HandleAccessModel
    {
        public List<User> UsersWithoutAccess { get; set; }
        public List<User> UsersWithAccess { get; set; }
        [Required(ErrorMessage = "You have to choose a user.")]
        public string SelectedUser { get; set; }

    }
}