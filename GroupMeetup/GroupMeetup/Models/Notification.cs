using System;
using System.Collections.Generic;
using System.Text;

namespace GroupMeetup.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public string NotificationContent { get; set; }
        public string NotificationType { get; set; }
        public int ReadCheck { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int IsDeleted { get; set; }
    }
}
