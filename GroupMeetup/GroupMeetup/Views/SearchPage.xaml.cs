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
	public partial class SearchPage : ContentPage
	{
        UserController uc;
        public SearchPage (UserController ucon)
		{
            uc = ucon;
			InitializeComponent ();
		}

        public void SearchClicked(object add, EventArgs evv)
        {
            while (results.Children.Count > 0)
            {
                results.Children.RemoveAt(0);
            }
            List<User> searchResults = uc.SearchUsers(usernameSearch.Text, this);
            Label l;
            foreach (User u in searchResults)
            {
                l = new Label
                {
                    Text = "Name: " + u.FirstName + " " + u.LastName + "\nUsername: " + u.Username,
                    FontSize = 20,

                    //userID = Convert.ToInt32(u.ID)
                };
                l.GestureRecognizers.Add(new TapGestureRecognizer((view) => this.Navigation.PushAsync(new ProfilePage(uc, u))));
                results.Children.Add(l);
            }
        }
    }
}