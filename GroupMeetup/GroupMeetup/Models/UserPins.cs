using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace GroupMeetup.Models
{
    public class UserPins
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public Pin UserPin { get; set; }
    }
}
