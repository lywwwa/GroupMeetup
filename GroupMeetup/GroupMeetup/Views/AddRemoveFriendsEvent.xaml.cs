using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using GroupMeetup.Controllers;
using GroupMeetup.Models;

namespace GroupMeetup.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddRemoveFriendsEvent : ContentPage
	{
        List<int> GroupMembers;
        List<User> Friends;
        GroupController gc;
        UserController uc;
        Group group;

		public AddRemoveFriendsEvent (Group g, GroupController gcon, UserController ucon)
		{
            group = g;
            gc = gcon;
            uc = ucon;

			InitializeComponent ();

            GetGroupMembers();
            GetFriendsList();

        }

        public async void GetGroupMembers()
        {
            GroupMembers = await uc.GetGroupMembers(group.Id);
        }

        public async void GetFriendsList()
        {
            Friends = await uc.GetFriends(uc.currentUser.ID);
            FriendsList.ItemsSource = Friends;
            
        }

        private async void Friend_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Trace.WriteLine("DAAN");
            bool MemberAlready = false;
            User selectedUser = FriendsList.SelectedItem as User;
            foreach(int x in GroupMembers)
            {
                Trace.WriteLine("Member id "+x);
                if(x == selectedUser.ID)
                {
                    MemberAlready = true;
                    break;
                }
            }
            if (!MemberAlready)
            {
                var action = await DisplayAlert((FriendsList.SelectedItem as User).Username, "Add to Participants?", "Yes", "No");
                if (action)
                {
                    gc.AddRemoveMember(group.Id, selectedUser.ID, "add", this, uc.currentUser.Username, uc.currentUser.ID);
                }
            }
            else
            {
                var action = await DisplayAlert((FriendsList.SelectedItem as User).Username, "Remove as Participant?", "Yes", "No");
                if (action)
                {
                    gc.AddRemoveMember(group.Id, selectedUser.ID, "remove", this, uc.currentUser.Username, uc.currentUser.ID);
                }
            }
        }
    }
}