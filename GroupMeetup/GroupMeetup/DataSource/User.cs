using System;
using System.Collections.Generic;
using System.Text;
using GroupMeetup.DataSource;

namespace GroupMeetup.DataSource
{
    class User
    {
        public List<UserGetSet> Users = new List<UserGetSet>()
        {
            new UserGetSet()
            {
                Username = "lywwwa",
                Password = "password",
                Name ="Lyra"
            }
        };
    }
}
