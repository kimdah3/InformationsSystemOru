using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{
    public class VisitingUserModel
    {
        public User VisitedUser { get; set; }
        public List<PostModel> UserPosts { get; set; }
        public string ProfilePicture { get; set; }
        public int loggedInUserId { get; set; }
    }
}