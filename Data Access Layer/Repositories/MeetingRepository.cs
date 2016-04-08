using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        public static void AddMeeting(Meeting newMeeting)
        {
            using (var context = new IsOruDbEntities())
            {

                context.Meeting.Add(newMeeting);
                context.SaveChanges();

            }
        }

        public List<Meeting> GetMeetingsOnDate(string date)
        {
            var meetings = new List<Meeting>();

            var dateTime = DateTime.Parse(date);

            using (var db = new IsOruDbEntities())
            {
                foreach (var meeting in db.Meeting)
                {
                    if (DateTime.Equals(meeting.Date, dateTime.Date))
                    {
                        meetings.Add(meeting);
                    }
                }
            }
            return meetings;
        }
    }
}
