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

        public PartialViewResult MeetingsAndBook(string date)
        {
            var meetingRepository = new MeetingRepository();
            var userRepository = new UserRepository();

            var model = new MeetingsModel()
            {
                Date = date,
                Meetings = meetingRepository.GetMeetingsOnDate(date),
                AllUsers = userRepository.GetAllUsers(),
                InvitedUsers = new List<User>()
            };

            return PartialView("_MeetingsAndBook", model);
        }

        public ActionResult Book(MeetingsModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Calendar");
            var meetingRepository = new MeetingRepository();
            var accountRepository = new AccountRepository();

            //Parsar valda tiden och datumet korrekt DateTime variabel.
            var timeofday = DateTime.ParseExact(model.TimeOfDay, "h:mmtt", CultureInfo.InvariantCulture);
            var date = DateTime.Parse(model.Date);
            date = date.Add(timeofday.TimeOfDay);

            var meeting = new Meeting
            {
                Date = date,
                Location = model.Location,
                Type = model.Type,
                HostId = accountRepository.GetIdFromUsername(User.Identity.Name),
            };

            meetingRepository.AddMeeting(meeting);
            return RedirectToAction("Calendar");


        }

        public ActionResult Meeting(string meetingId)
        {
            var meetingRepository = new MeetingRepository();
            var userRepository = new UserRepository();

            var model = new MeetingModel()
            {
                Meeting = meetingRepository.GettMeetingById(int.Parse(meetingId)),
                AllUsers = userRepository.GetAllUsers(),
                AcceptedUsers = new List<User>(),
                InvitedUsers = new List<User>(),
            };

            if(!model.Meeting.HostId.HasValue) throw new NullReferenceException("Saknas value i db!");
            model.Host = userRepository.GetUserFromId(model.Meeting.HostId.Value);
            
            return View(model);
        }
    }
}