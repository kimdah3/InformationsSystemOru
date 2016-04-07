using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class MeetingRepository
    {
        public List<Meeting> GetAllMeetings()
        {
            var meetings = new List<Meeting>();
            using (var db = new IsOruDbEntities())
            {
                foreach (var meeting in db.Meeting)
                {
                    meetings.Add(meeting);
                }
            }
            return meetings;
        }


        // Lägger till ett nytt möte
        public static void RegisterUser(Meeting newMeeting)
        {
            using (var context = new IsOruDbEntities())
            {
                
                context.Meeting.Add(newMeeting);
                context.SaveChanges();

            }
        }
    }
}
