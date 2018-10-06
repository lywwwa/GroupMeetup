using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsPage : ContentPage
	{
		public FriendsPage ()
		{
			InitializeComponent ();
            overlay.IsVisible = false;
        }
      
        public void OnButtonClicked(object a, EventArgs e)
        {
            overlay.IsVisible = true;
        }

        public void Done_Clicked(object add, EventArgs evv)
        {
            overlay.IsVisible = false;
        }

        public void Cancel_Clicked(object ad, EventArgs ev)
        {
            overlay.IsVisible = false;
        }

        public void Logout_Clicked(object obj, EventArgs a)
        {
           // this.Navigation.PushAsync(new Views.LoginPage());
           // this.Navigation.RemovePage(this);
            //remove all info
        }
    }
}