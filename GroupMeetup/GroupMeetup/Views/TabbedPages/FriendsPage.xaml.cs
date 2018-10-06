using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupMeetup.Controllers;
using GroupMeetup.Models;

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsPage : ContentPage
	{
        UserController uc;
		public FriendsPage ()
		{
			InitializeComponent ();
            //overlay.IsVisible = false;
        }
      
        

        public void Cancel_Clicked(object ad, EventArgs ev)
        {
            //overlay.IsVisible = false;
        }

        
    }
}