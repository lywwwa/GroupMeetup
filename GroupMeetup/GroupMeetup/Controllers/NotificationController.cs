﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using Xamarin.Forms;
using GroupMeetup.Models;
using System.Diagnostics;

namespace GroupMeetup.Controllers
{
    public class NotificationController
    {
        WebClient client;
        public NotificationController()
        {

        }

        public List<Notification> PopulateNotifications(ContentPage page, int id)
        {
            List<Notification> Notifications = new List<Notification>();
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/populateNotifications.php?id=" + id));
            //page.DisplayAlert("",response,"nice");
            string[] Notifs = response.Split('\n');
            Notification toAdd;
            foreach (string n in Notifs)
            {
                string[] eachNotif = n.Split('/');
                if (n != "")
                {
                    toAdd = new Notification
                    {
                        ID = Convert.ToInt32(eachNotif[0]),
                        UserID = Convert.ToInt32(eachNotif[1]),
                        NotificationContent = eachNotif[2],
                        NotificationType = eachNotif[3],
                        ReadCheck = Convert.ToInt32(eachNotif[4]),
                        IsDeleted = Convert.ToInt32(eachNotif[5])
                    };
                    Notifications.Add(toAdd);
                }
            }
            return Notifications;
        }
    }
}
