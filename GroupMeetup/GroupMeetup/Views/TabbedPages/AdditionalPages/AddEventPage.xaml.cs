using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GroupMeetup.Models;
using GroupMeetup.Controllers;

using DurianCode.PlacesSearchBar;
using System.Diagnostics;

namespace GroupMeetup.TabbedPages.AdditionalPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEventPage : ContentPage
    {
        Group group;
        GroupController gc;
        UserController uc;
        EventPage ev;
        Place place;
        String PlaceId;

        public AddEventPage(UserController ucon, GroupController gcon, Group g, EventPage e)
        {
            //gc = new GroupController();
            uc = ucon;
            gc = gcon;
            ev = e;
            InitializeComponent();
            

            search_bar.ApiKey = "AIzaSyCyQVh1VQEBkpYuonBeikY9QK1y35Djy2s";
            search_bar.Type = PlaceType.Establishment;
            search_bar.Components = new Components("country:ph");
            search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            search_bar.TextChanged += Search_Bar_TextChanged;
            search_bar.MinimumSearchText = 5;
            results_list.ItemSelected += Results_List_ItemSelected;

            if (g == null) group = new Group();
            else
            {
                group = g;
                Subject.Text = group.Name;
                StartDate.Date = group.MeetupDateTime;
                StartTime.Time = group.MeetupDateTime.TimeOfDay;
                EndDate.Date = group.MeetupDateTimeEnd;
                EndTime.Time = group.MeetupDateTimeEnd.TimeOfDay;
                LocationName.Text = "Location: " + group.MeetupLocation;
                EventAddUpdate.Text = "Update";
                GetPlaceIDEditEvent();
            }

        }

        public async void GetPlaceIDEditEvent()
        {
            PlaceId = group.MeetupLocationId;
            place = await Places.GetPlace(group.MeetupLocationId, search_bar.ApiKey);
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(Subject.Text) || string.IsNullOrEmpty(place.Name))
            {
                group.UserID = uc.currentUser.ID;
                group.Name = Subject.Text;
                group.MeetupLocation = place.Name;
                group.MeetupLocationId = PlaceId;
                group.MeetupLocationLat = place.Latitude;
                group.MeetupLocationLon = place.Longitude;
                group.MeetupDateTime = StartDate.Date.Add(StartTime.Time);
                group.MeetupDateTimeEnd = EndDate.Date.Add(EndTime.Time);
                //add event on database for events
                //add the event on the selected attendees
                Trace.WriteLine("To add: " + group.UserID + "/" + group.Name + "/" + group.MeetupLocation + "/" + group.MeetupLocationId + "/" + group.MeetupLocationLat + "/" + group.MeetupLocationLon + "/" + group.MeetupDateTime + "/" + group.MeetupDateTimeEnd);
                gc.AddGroup(group, this);
                ev.EventOverlay.IsVisible = false;
                GetGroupsFromAddEvent();
                ev.MeetingsListViewLead.ItemsSource = ev.GroupsManaged;
            }
            else
            {
                DisplayAlert("Error","Fields cannot be blank.","Try again");
            }



        }

        public async void GetGroupsFromAddEvent()
        {
            ev.GroupsManaged = await gc.GetGroups(group.UserID, "lead");
        }

        void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
                results_list.IsVisible = true;
            else
            {
                Trace.WriteLine(result.Status);
            }
        }

        void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(search_bar.Text.Length == 5)
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    results_list.IsVisible = false;
                    spinner.IsVisible = true;
                    spinner.IsRunning = true;
                }
                else
                {
                    results_list.IsVisible = true;
                    spinner.IsRunning = false;
                    spinner.IsVisible = false;
                }
            }
                
        }

        async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var prediction = (AutoCompletePrediction)e.SelectedItem;
            results_list.SelectedItem = null;

            place = await Places.GetPlace(prediction.Place_ID, search_bar.ApiKey);
            if (place != null)
            {
                LocationName.Text = "Location: " + place.Name;
                PlaceId = prediction.Place_ID;
                Trace.WriteLine("PlaceId: " + PlaceId);
            }
        }
    }
}