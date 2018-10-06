using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GroupMeetup.Models;
using GroupMeetup.Controllers;
using System.Diagnostics;

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
        NotificationController nc;

		public NotificationPage(User user)
		{
            nc = new NotificationController();
			InitializeComponent ();
            List<Notification> NotifList = nc.PopulateNotifications(this, user.ID);
            NotificationsList.ItemsSource = NotifList;

		}

        private void NotificationsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        
    }
}