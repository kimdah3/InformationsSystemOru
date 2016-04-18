using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{
    public class MyMeetingsModel
    {
        public List<Meeting> InvitedToMeetings { get; set; }
        public List<Meeting> HostToMeetings { get; set; }
        public List<Meeting> AttendingToMeetings { get; set; }
    }
}