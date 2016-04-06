using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Data_Access_Layer;

namespace InformationsSystemOru.Models
{
    public class MeetingModel
    {
        public List<DateTime> UpcomingDays { get; set; }
        public List<Meeting> Meetings { get; set; }
    }
}