using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GroupMeetup.Controllers;
using GroupMeetup.Models;

namespace GroupMeetup.Views
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
                FriendRejectButton.IsVisible = false;
            }
            else
            {
                LogOutL.IsVisible = false;
                string ConnectionStatus = CallGetUserConnectionStatus(uc.currentUser.ID, profile.ID);
                string LatestAction = "";
                if (ConnectionStatus == "null")
                {
                    ConnectionStatus = CallGetUserConnectionStatus(profile.ID, uc.currentUser.ID);
                    if (ConnectionStatus == "null")
                    {
                        FriendButton.Text = "Add " + uc.currentUser.Username + " as Friend";
                        FriendRejectButton.IsVisible = false;
                    }
                    else if (ConnectionStatus == "0")
                    {
                        LatestAction = CallGetUserLatestAction(profile.ID, uc.currentUser.ID);
                        if (LatestAction == "1")
                        {
                            FriendButton.Text = "Accept friend request";
                        }
                        else if (LatestAction == "0" || LatestAction == "2" || LatestAction == "3")
                        {
                            FriendButton.Text = "Add " + profile.Username + " as Friend";
                            FriendRejectButton.IsVisible = false;
                        }
                    }
                    else if (ConnectionStatus == "1")
                    {
                        FriendButton.Text = "Remove " + profile.Username + "from Friends List";
                        FriendRejectButton.IsVisible = false;
                    }
                }
                else if (ConnectionStatus == "0")
                {
                    LatestAction = CallGetUserLatestAction(uc.currentUser.ID, profile.ID);
                    if (LatestAction == "1") FriendButton.Text = "Cancel friend request";
                    else if (LatestAction == "0" || LatestAction == "2" || LatestAction == "3")
                        FriendButton.Text = "Add " + profile.Username + " as Friend";
                    FriendRejectButton.IsVisible = false;
                }
                else if (ConnectionStatus == "1")
                {
                    FriendButton.Text = "Remove " + profile.Username + " from Friends List";
                    FriendRejectButton.IsVisible = false;
                }
            }
        }

        private void FriendButtonClicked(object sender, EventArgs e)
        {
            if (!uc.ManageConnection(uc.currentUser, profile, FriendButton, FriendRejectButton))
            {
                DisplayAlert("", "Error", "Okay");
            }
        }
        private void FriendRejectButtonClicked(object sender, EventArgs e)
        {
            if (!uc.ManageConnection(uc.currentUser, profile, FriendRejectButton, FriendButton))
            {
                DisplayAlert("", "Error", "Okay");
            }
        }

        private string CallGetUserConnectionStatus(int User1, int User2)
        {
            return uc.GetUserConnection(User1, User2);

        }

        private string CallGetUserLatestAction(int User1, int User2)
        {
            return uc.GetUserLatestAction(User1, User2);

        }
    }
}