using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GroupMeetup.Models;
using GroupMeetup.Views;
using GroupMeetup.Controllers;
using System.Diagnostics;

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
    {
        NotificationController nc;
        UserController uc;

        public NotificationPage(User user, UserController ucon)
		{
            uc = ucon;
            nc = new NotificationController();
			InitializeComponent ();
            List<Notification> NotifList = nc.PopulateNotifications(this, user.ID);
            NotificationsList.ItemsSource = NotifList;

		}

        private void NotificationsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Notification SelectedNotification = NotificationsList.SelectedItem as Notification;
            string[] UNameExtract = SelectedNotification.NotificationContent.Split(' ');
            string UserNotifSenderName = UNameExtract[0];
            User UserNotifSender = uc.GetUserData(UserNotifSenderName, this);
            Trace.WriteLine(UserNotifSender.Username);
            Navigation.PushAsync(new ProfilePage(uc, UserNotifSender));
        }

        
    }
}