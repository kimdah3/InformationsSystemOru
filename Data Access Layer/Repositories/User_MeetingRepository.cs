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
    }
}
