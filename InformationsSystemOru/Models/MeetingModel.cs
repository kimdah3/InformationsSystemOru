using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{
    public class MeetingModel
    {
        public Meeting Meeting { get; set; }
        public User Host { get; set; }

        public List<User> AllNoneInvitedUsers { get; set; }
        public List<User> InvitedUsers { get; set; }
        public List<User> AcceptedUsers { get; set; }
        public string InvitedUser { get; set; }
        public string RemoveInvitationUser { get; set; }
    }
}