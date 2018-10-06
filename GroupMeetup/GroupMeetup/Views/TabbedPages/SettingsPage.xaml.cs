using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupMeetup.Views.TabbedPages
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