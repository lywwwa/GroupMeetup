using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupMeetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            this.BackgroundColor = Color.FromHex("#00313c");
            NavigationPage.SetHasNavigationBar(this, false);
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            //for log in
            //check if valid u/n and pw
        }


        //open sign in page
        public void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            this.Navigation.PushAsync(new SigninPage());
          
        }

        //open forgot password
        public void TappedHandler(object sender, EventArgs args)
        {
            //this.Navigation.PushAsync(new ForgotPasswordPage());
        }

       
    }
}