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
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        public void Logout_Clicked(object obj,EventArgs a)
        {
            this.Navigation.PushAsync(new LoginPage());
            this.Navigation.RemovePage(this);
            //remove all info
        }
	}
}