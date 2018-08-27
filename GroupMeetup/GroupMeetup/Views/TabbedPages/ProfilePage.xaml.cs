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
		public ProfilePage (UserController ucon)
		{
            uc = ucon;
            InitializeComponent ();
            User profile = uc.GetUserData("ovvnzixl", this);
            profileUsername.Text = profile.username;
            profileFullName.Text = profile.firstName + " " + profile.lastName;
        }
	}
}