using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class User_MeetingRepository
    {
        public void InviteUser(Meeting meeting, User invitedUser)
        {
            using (var db = new IsOruDbEntities())
            {
                var user_meeting = new User_Meeting()
                {
                    MeetingId = meeting.Id,
                    UserId = invitedUser.Id,
                    Accepted = false,
                };
                db.User_Meeting.Add(user_meeting);
                db.SaveChanges();
            }
        }

        public List<User_Meeting> GetInvitedUsersByMeetingId(int meetingId)
        {
            var invitedUsers = new List<User_Meeting>();

            using (var db = new IsOruDbEntities())
            {
                invitedUsers = db.User_Meeting.Where(x => x.MeetingId == meetingId).ToList();
            }


            return invitedUsers;
        }

        public void RemoveInvitation(User user, int meetingId)
        {

            using (var db = new IsOruDbEntities())
            {
                db.User_Meeting.Remove(db.User_Meeting.FirstOrDefault(x => x.UserId == user.Id && x.MeetingId == meetingId));
                db.SaveChanges();
            }
        }

        public List<Meeting> GetInvitedToMeetingsByUserId(User user)
        {
            var meetings = new List<Meeting>();
            using (var db = new IsOruDbEntities())
            {
                var userMeetings = db.User_Meeting.Where(x => x.UserId == user.Id && !x.Accepted.Value);

                foreach (var meeting in userMeetings)
                {
                    var m = db.Meeting.FirstOrDefault(x => x.Id == meeting.MeetingId);
                    m.User = db.User.FirstOrDefault(x => x.Id == m.HostId.Value);
                    meetings.Add(m);
                }
            }
            return meetings;
        }

        public List<Meeting> GetAttendingMeetingsByUserId(User user)
        {
            var meetings = new List<Meeting>();

            using (var db = new IsOruDbEntities())
            {
                var userMeetings = db.User_Meeting.Where(x => x.Accepted.Value && x.UserId == user.Id);
                foreach (var meeting in userMeetings)
                {
                    var m = db.Meeting.FirstOrDefault(x => x.Id == meeting.MeetingId);
                    m.User = db.User.FirstOrDefault(x => x.Id == m.HostId.Value);
                    meetings.Add(m);
                }
            }
            return meetings;
        }

        public List<Meeting> GetHostingMeetingsByUserId(User user)
        {
            var meetings = new List<Meeting>();
            using (var db = new IsOruDbEntities())
            {
                meetings.AddRange(db.Meeting.Where(x => x.HostId == user.Id));

            }
            return meetings;
        }

        public void AcceptInvitation(User user, Meeting meeting)
        {
            using (var db = new IsOruDbEntities())
            {
                db.User_Meeting.First(x => x.UserId == user.Id && x.MeetingId == meeting.Id).Accepted = true;
                db.SaveChanges();
            }
        }
    }
}
