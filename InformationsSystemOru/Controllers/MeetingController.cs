using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer;
using Data_Access_Layer.Repositories;
using InformationsSystemOru.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InformationsSystemOru.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        private UserRepository _userRepository;
        private MeetingRepository _meetingRepository;
        private AccountRepository _accountRepository;
        private User_MeetingRepository _userMeetingRepository;

        public MeetingController()
        {
            _userRepository = new UserRepository();
            _meetingRepository = new MeetingRepository();
            _accountRepository = new AccountRepository();
            _userMeetingRepository = new User_MeetingRepository();
        }

        // GET: Meeting
        [Authorize]
        public ActionResult Calendar()
        {
            var days = new List<DateTime>();
            var currentDate = DateTime.Now;

            for (var i = 0; i < 60; i++)
            {
                days.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }

            //    string json = @"{
            //        '2016-04-30': { 'number': 5, 'badgeClass': 'badge-warning', 'url': 'http://w3widgets.com/responsive-calendar' },
            //        '2016-04-26': { 'number': 1, 'badgeClass': 'badge-warning', 'url': 'http://w3widgets.com' },
            //        '2016-05-03': { 'number': 1, 'badgeClass': 'badge-error' },
            //        '2016-06-12': { }
            //}";

            //    var o = JObject.Parse(json);
            var model = new CalendarModel()
            {
                UpcomingDays = days,
                Meetings = _meetingRepository.GetAllMeetings(),
                CurrentDate = DateTime.Today,
                Events = new List<EventHolder>()
            };


            foreach (var meeting in model.Meetings)
            {
                model.Events.Add(new EventHolder()
                {
                    Id = meeting.Id,
                    Date = meeting.Date.Value.ToString("yyyy-MM-dd"),
                    Location = meeting.Location,
                    Type = meeting.Type
                });
            }

            return View(model);
        }

        public PartialViewResult MeetingsAndBook(string date)
        {

            var model = new MeetingsModel()
            {
                Date = date,
                Meetings = _meetingRepository.GetMeetingsOnDate(date),
                AllUsers = _userRepository.GetAllUsers(),
                InvitedUsers = new List<User>()
            };

            return PartialView("_MeetingsAndBook", model);
        }

        public ActionResult Book(MeetingsModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Calendar");

            //Parsar valda tiden och datumet korrekt DateTime variabel.
            var timeofday = DateTime.ParseExact(model.TimeOfDay, "h:mmtt", CultureInfo.InvariantCulture);
            var date = DateTime.Parse(model.Date);
            date = date.Add(timeofday.TimeOfDay);

            var meeting = new Meeting
            {
                Date = date,
                Location = model.Location,
                Type = model.Type,
                HostId = _accountRepository.GetIdFromUsername(User.Identity.Name),
            };

            _meetingRepository.AddMeeting(meeting);
            return RedirectToAction("Calendar");
        }

        [HttpGet]
        public ActionResult Meeting(string meetingId)
        {

            var model = new MeetingModel
            {
                Meeting = _meetingRepository.GetMeetingById(int.Parse(meetingId)),
                AcceptedUsers = new List<User>(),
                InvitedUsers = new List<User>(),
                AllNoneInvitedUsers = _userRepository.GetAllUsers()
            };

            // Loopar igenom listan med alla user_meetings för att lägga till i accepted eller icke
            var userMeetings = _userMeetingRepository.GetInvitedUsersByMeetingId(int.Parse(meetingId));
            foreach (var userMeeting in userMeetings)
            {
                if ((bool)userMeeting.Accepted)
                {
                    model.AcceptedUsers.Add(_userRepository.GetUserFromId((int)userMeeting.UserId));
                }
                else
                {
                    model.InvitedUsers.Add(_userRepository.GetUserFromId((int)userMeeting.UserId));
                }
                var user = _userRepository.GetUserFromId((int)userMeeting.UserId);
                model.AllNoneInvitedUsers.RemoveAll(x => x.Id == user.Id);
            }

            if (!model.Meeting.HostId.HasValue) throw new NullReferenceException("Saknas value i db!");
            model.Host = _userRepository.GetUserFromId(model.Meeting.HostId.Value);

            return View(model);
        }

        [HttpPost]
        public ActionResult Invite(MeetingModel model, int meetingId)
        {
            var index = model.InvitedUser.LastIndexOf(".");
            var userId = int.Parse(model.InvitedUser.Substring(0, index));

            var meeting = _meetingRepository.GetMeetingById(meetingId);
            var invitedUser = _userRepository.GetUserFromId(userId);

            _userMeetingRepository.InviteUser(meeting, invitedUser);

            return RedirectToAction("Meeting", new { @meetingId = meeting.Id });
        }

        public ActionResult RemoveInvitation(MeetingModel model, int meetingId)
        {
            var userMeetingRepository = new User_MeetingRepository();

            //Parsar om meetingId = "1. Kim Dahlberg" till = 1
            var index = model.RemoveInvitationUser.LastIndexOf(".");
            var userId = int.Parse(model.RemoveInvitationUser.Substring(0, index));

            var user = _userRepository.GetUserFromId(userId);

            userMeetingRepository.RemoveInvitation(user, meetingId);

            return RedirectToAction("Meeting", new { @meetingId = meetingId });
        }

        public ActionResult MyMeetings()
        {
            var userId = _accountRepository.GetIdFromUsername(User.Identity.Name);
            var user = _userRepository.GetUserFromId(userId);

            var model = new MyMeetingsModel()
            {
                InvitedToMeetings = _userMeetingRepository.GetInvitedToMeetingsByUserId(user),
                AttendingToMeetings = _userMeetingRepository.GetAttendingMeetingsByUserId(user),
                HostToMeetings = _userMeetingRepository.GetHostingMeetingsByUserId(user)
            };

            return View(model);
        }

        public ActionResult AcceptMeeting(int meetingId)
        {
            var userId = _accountRepository.GetIdFromUsername(User.Identity.Name);
            var user = _userRepository.GetUserFromId(userId);
            var meeting = _meetingRepository.GetMeetingById(meetingId);

            _userMeetingRepository.AcceptInvitation(user, meeting);

            return RedirectToAction("MyMeetings");
        }

        public ActionResult DeclineMeeting(int meetingId)
        {
            var userId = _accountRepository.GetIdFromUsername(User.Identity.Name);
            var user = _userRepository.GetUserFromId(userId);

            _userMeetingRepository.RemoveInvitation(user, meetingId);

            return RedirectToAction("MyMeetings");
        }
    }
}