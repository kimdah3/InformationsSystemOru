using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;

namespace InformationsSystemOru.Controllers
{
    public class MeetingController : Controller
    {
        // GET: Meeting
        public ActionResult Index()
        {
            var days = new List<DateTime>();
            var currentDate = DateTime.Now;
            var meetingRepository = new MeetingRepository();

            for (var i = 0; i < 60; i++)
            {
                days.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }
            var model = new MeetingModel()
            {
                UpcomingDays = days,
                Meetings = meetingRepository.GetAllMeetings()
            };

            return View(model);
        }
    }
}