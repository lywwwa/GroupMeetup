using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GroupMeetup.Controllers;
using GroupMeetup.Models;

namespace GroupMeetup.Views.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        UserController uc;
        User profile;
        public ProfilePage(UserController ucon, User profile)
        {
            this.profile = profile;
            uc = ucon;
            InitializeComponent();
            profileUsername.Text = "Username: " + profile.Username;
            profileFullName.Text = "Full Name: " + profile.FirstName + " " + profile.LastName;
            if (uc.currentUser.ID == profile.ID)
            {
                FriendButton.IsVisible = false;
            }
            else
            {
                LogOutL.IsVisible = false;
                int ConnectionStatus = CallGetUserConnectionStatus(uc.currentUser.ID, profile.ID);
                int LatestAction = CallGetUserLatestAction(uc.currentUser.ID, profile.ID);
                if (ConnectionStatus == 0)
                {
                    if(LatestAction == 1) FriendButton.Text = "Cancel friend request";
                    else FriendButton.Text = "Add " + profile.Username + " as Friend";
                }
                else if (ConnectionStatus == 1) FriendButton.Text = "Remove " + profile.Username + "from Friends List";
            }
        }

        public void FriendButtonClicked(object sender, EventArgs e)
        {
            //return uc.ManageConnection(uc.currentUser, profile, FriendButton, this);
        }

        private int CallGetUserConnectionStatus(int User1, int User2)
        {
            return uc.GetUserConnection(User1, User2);

        }

        private int CallGetUserLatestAction(int User1, int User2)
        {
            return uc.GetUserLatestAction(User1, User2);

        }
    }
}