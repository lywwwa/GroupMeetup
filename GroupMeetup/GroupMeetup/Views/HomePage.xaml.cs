using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupMeetup.Controllers;
using GroupMeetup.Models;
using GroupMeetup.Views;
using System.Threading;
using Plugin.Geolocator;
using System.Net;
using System.Diagnostics;

namespace GroupMeetup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        UserController uc;
        GroupController gc;

        Plugin.Geolocator.Abstractions.IGeolocator Geolocator = CrossGeolocator.Current;
        Plugin.Geolocator.Abstractions.Position CurrentPos;

        WebClient client;

        public HomePage (UserController ucon, GroupController gcon)
        {
            uc = ucon;
            gc = gcon;
            InitializeComponent();

            Children.Add(new TabbedPages.FriendsPage(uc));
            Children.Add(new TabbedPages.NotificationPage(uc.currentUser, uc, gc));
            Children.Add(new TabbedPages.EventPage(uc, gc));
            NavigationPage.SetHasBackButton(this, false);
            this.BarBackgroundColor = Color.FromHex("#392c4b");

            var ts = new ThreadStart(UpCoord);
            var backgroundThread = new Thread(ts);
            backgroundThread.Start();
        }

        private async void UpCoord()
        {
            while (true)
            {
                Trace.WriteLine("DUMADAAN FREN");
                CurrentPos = await Geolocator.GetPositionAsync(TimeSpan.FromSeconds(1));
                client = new WebClient();
                string response = "";
                try
                {
                    if(!client.IsBusy) response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/uploadCoord.php?id=" + uc.currentUser.ID + "&lat=" + CurrentPos.Latitude + "&lon=" + CurrentPos.Longitude));
                }
                catch(Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
                Thread.Sleep(5000);
            }
        }

        public void Account_Clicked(object w,EventArgs args)
        {
            this.Navigation.PushAsync(new ProfilePage(uc, uc.currentUser));
        }


        public void Logout_Clicked(object obj, EventArgs a)
        {
            
        }

    }
}