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

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsPage : ContentPage
	{
        UserController uc;
		public FriendsPage (UserController ucon)
		{
            uc = ucon;
            InitializeComponent ();
            
            //overlay.IsVisible = false;
        }

        public void OnButtonClicked(object a, EventArgs e)
        {
            this.Navigation.PushAsync(new Views.SearchPage(uc));
        }

        public void Cancel_Clicked(object ad, EventArgs ev)
        {
            //overlay.IsVisible = false;
        }

        private void FriendsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(uc, FriendsList.SelectedItem as User));
        }
    }
}