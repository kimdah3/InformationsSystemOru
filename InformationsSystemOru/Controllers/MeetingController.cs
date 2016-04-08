using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;

namespace InformationsSystemOru.Controllers
{
    public class MeetingController : Controller
    {
        // GET: Meeting
        public ActionResult Calendar()
        {
            var days = new List<DateTime>();
            var currentDate = DateTime.Now;
            var meetingRepository = new MeetingRepository();

            for (var i = 0; i < 60; i++)
            {
                days.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }
            var model = new CalendarModel()
            {
                UpcomingDays = days,
                Meetings = meetingRepository.GetAllMeetings()
            };

            return View(model);
        }

        public PartialViewResult Meeting(string date)
        {
            var meetingRepository = new MeetingRepository();

            var model = new MeetingModel()
            {
                Date = date,
                Meetings = meetingRepository.GetMeetingsOnDate(date),
            };

            return PartialView("_Meeting", model);
        }

        public ActionResult Book(NewMeetingModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Meeting", model);

            var meeting = new Meeting
            {
                Date = model.Date,
                Location = model.Location,
                Type = model.Type
            };

            MeetingRepository.AddMeeting(meeting);

            return RedirectToAction("Calendar");


        }
    }
}