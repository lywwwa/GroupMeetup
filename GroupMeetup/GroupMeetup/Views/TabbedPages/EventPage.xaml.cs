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
using System.Diagnostics;

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventPage : ContentPage
	{
        public List<Group> GroupsManaged;
        public List<Group> GroupsJoined;
        UserController uc;
        GroupController gc;

		public EventPage (UserController ucon, GroupController gcon)
		{
            uc = ucon;
            gc = gcon;
            GetSource();
            InitializeComponent();

            MeetingsListViewLead.ItemsSource = GroupsManaged;
            MeetingsListViewJoin.ItemsSource = GroupsJoined;
        }

        public async void GetSource()
        {
            GroupsManaged = await gc.GetGroups(uc.currentUser.ID, "lead");
            GroupsJoined = await gc.GetGroups(uc.currentUser.ID, "join");
        }

        public void NewEvent_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new TabbedPages.AdditionalPages.AddEventPage(uc, gc, null, this));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            GroupsManaged = await gc.GetGroups(uc.currentUser.ID, "lead");
            MeetingsListViewLead.ItemsSource = GroupsManaged;
        }

        private void MeetingsListViewLead_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            EventOverlay.IsVisible = true;
        }

        private void AddRemove_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddRemoveFriendsEvent(MeetingsListViewLead.SelectedItem as Group, gc, uc));
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdditionalPages.AddEventPage(uc, gc, MeetingsListViewLead.SelectedItem as Group, this));
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {

        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            EventOverlay.IsVisible = false;
            EventOverlayJoin.IsVisible = false;
        }

        private void ViewMapLead_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GroupMap(MeetingsListViewLead.SelectedItem as Group, uc));
            //Navigation.PushAsync(new GroupMap());
        }

        private void ViewMapJoin_Clicked(object sender, EventArgs e)
        {
            Trace.WriteLine((MeetingsListViewJoin.SelectedItem as Group).Id);
            Navigation.PushAsync(new GroupMap(MeetingsListViewJoin.SelectedItem as Group, uc));
            //Navigation.PushAsync(new GroupMap());
        }

        private void MeetingsListViewJoin_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            EventOverlayJoin.IsVisible = true;
        }

        private void Leave_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}