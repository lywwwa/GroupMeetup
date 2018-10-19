using System;
using System.Collections.Generic;
using System.Text;

namespace GroupMeetup.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string MeetupLocationId { get; set; }
        public string MeetupLocation { get; set; }
        public double MeetupLocationLat { get; set; }
        public double MeetupLocationLon { get; set; }
        public DateTime MeetupDateTime { get; set; }
        public DateTime MeetupDateTimeEnd { get; set; }
    }
}
