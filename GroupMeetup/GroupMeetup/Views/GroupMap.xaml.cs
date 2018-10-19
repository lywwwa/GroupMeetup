using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

using GroupMeetup.Models;
using Plugin.Geolocator;
using System.Diagnostics;
using System.Threading;
using System.Net;
using GroupMeetup.Controllers;

namespace GroupMeetup.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupMap : ContentPage
	{
        Plugin.Geolocator.Abstractions.IGeolocator Geolocator = CrossGeolocator.Current;
        Plugin.Geolocator.Abstractions.Position CurrentPos;

        UserController uc;

        WebClient client;
        Pin p;
        Group CurrentGroup;

        List<UserPins> UPins;
        List<int> ListOfMembersInt;
        List<int> ListOfMembersArrived;
        List<User> ListOfMembers;

        public GroupMap(Group _CurrentGroup, UserController ucon)
        {
            uc = ucon;
            p = new Pin();
            UPins = new List<UserPins>();
            CurrentGroup = _CurrentGroup;
            InitializeComponent();
            GetDestinationLoc();
            InitialMapView();
            GetMembers();

            var ts = new ThreadStart(GetMemberLocations);
            var backgroundThread = new Thread(ts);
            backgroundThread.Start();
        }

        public async void GetMembers()
        {
            ListOfMembers = new List<User>();
            ListOfMembersInt = await uc.GetGroupMembers(CurrentGroup.Id);
            ListOfMembersArrived = await uc.GetGroupMembersArrived(CurrentGroup.Id);
            foreach (int L in ListOfMembersInt)
            {
                ListOfMembers.Add(uc.GetUserDataById(L));
            }
            MembersList.ItemsSource = ListOfMembers;
        }

        public void GetMemberLocations()
        {
            while (true)
            {
                Trace.WriteLine("GET MEMBER LOCATIONS");
                //CurrentPos = await Geolocator.GetPositionAsync(TimeSpan.FromSeconds(3));
                client = new WebClient();
                string response = "";
                try
                {
                    if (!client.IsBusy)
                    {
                        response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/downloadMemberLocations.php?gid=" + CurrentGroup.Id));
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            PlaceNewPins(response);
                        });
                    }
                }
                catch (Exception e)
                {

                    Trace.WriteLine(e.Message);
                }
                
                
                Trace.WriteLine(gpsMap.Pins.Count);
                Thread.Sleep(5000);
            }
        }

        public void PlaceNewPins(string response) {
            gpsMap.Pins.Clear();
            string[] PinLoc = response.Split('\n');
            UserPins toAdd;
            foreach (string n in PinLoc)
            {
                Trace.WriteLine(n);
                string[] eachPin = n.Split('/');
                if (n != "" && eachPin[1] != uc.currentUser.Username)
                {
                    toAdd = new UserPins
                    {
                        UserId = Convert.ToInt32(eachPin[0]),
                        Username = eachPin[1],
                        UserPin = new Pin
                        {
                            Position = new Position(Convert.ToDouble(eachPin[2]), Convert.ToDouble(eachPin[3])),
                            Label = eachPin[1] + "'s Current Location",
                            Type = PinType.SavedPin
                        }
                    };
                    gpsMap.Pins.Add(toAdd.UserPin);
                    GetDestinationLoc();
                }
            }
        }

        public void GetDestinationLoc()
        {
            p.Position = new Position(CurrentGroup.MeetupLocationLat, CurrentGroup.MeetupLocationLon);
            p.Label = "Meetup Location: " + CurrentGroup.MeetupLocation;
            p.Type = PinType.Generic;
            gpsMap.Pins.Add(p);
        }

        public async void InitialMapView()
        {
            CurrentPos = await Geolocator.GetPositionAsync(TimeSpan.FromSeconds(1));
            gpsMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(CurrentPos.Latitude, CurrentPos.Longitude), Distance.FromMiles(1)));

        }

        private void MemberButton_Clicked(object sender, EventArgs e)
        {
            MembersOverlay.IsVisible = true;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(uc.currentUser.ID == CurrentGroup.UserID){
                User SelUser = MembersList.SelectedItem as User;
                int x = 0;
                int Arrived = 0;
                foreach (User u in ListOfMembers)
                {
                    if (u.ID != SelUser.ID)
                    {
                        x++;
                    }
                    else break;
                }
                ListOfMembersArrived = await uc.GetGroupMembersArrived(CurrentGroup.Id);
                int z = 0;
                foreach (int y in ListOfMembersArrived)
                {
                    Trace.WriteLine("ASDHSAD: "+y);
                    if(z != x)
                    {
                        z++;
                    }
                    else
                    {
                        Arrived = y;
                        break;
                    }
                }
                if (Arrived == 0)
                {
                    var action = await DisplayAlert(SelUser.Username, "Did they arrive at the meetup location?", "Yes", "No");
                    if (action)
                    {
                        client = new WebClient();
                        if (!client.IsBusy)
                        {
                            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/someoneArrived.php?gid=" + CurrentGroup.Id + "&uid=" + (MembersList.SelectedItem as User).ID));

                        }

                    }
                }
                else
                {
                    await DisplayAlert(SelUser.Username, "Already arrived.", "Okay");
                }
            }
        }
        
        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            MembersOverlay.IsVisible = false;
        }
    }
}