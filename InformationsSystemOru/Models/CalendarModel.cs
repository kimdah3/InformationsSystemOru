using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data_Access_Layer;
using Newtonsoft.Json.Linq;

namespace InformationsSystemOru.Models
{
    public class CalendarModel
    {
        public List<DateTime> UpcomingDays { get; set; }
        public List<Meeting> Meetings { get; set; }
        public DateTime CurrentDate { get; set; }
        public List<EventHolder> Events { get; set; }

    }

    public class EventHolder
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
    }
}