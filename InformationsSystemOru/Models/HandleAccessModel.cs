using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{
    public class HandleAccessModel
    {
        public List<User> UsersWithoutAccess { get; set; }
        public List<User> UsersWithAccess { get; set; }
        public string SelectedUser { get; set; }

    }
}