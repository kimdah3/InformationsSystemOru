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
         
        public string ProfilePicture { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public List<PostModel> AllPosts { get; set; }
        public PostModel NewPost { get; set; }
    }
}
