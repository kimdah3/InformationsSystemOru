using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationsSystemOru.Models
{
    public class CreateUserModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
}