using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using GroupMeetup.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupMeetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        UserController uc;
        public LoginPage (UserController ucon)
		{
            // ASK FOR PERMISSION (GPS, STORAGE)s
            

            uc = ucon;
            InitializeComponent();
            uc = new UserController();
            this.BackgroundColor = Color.FromHex("#00313c");
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void LoginClicked(object sender, EventArgs e)
        {
            uc.Login(usernameField.Text, passwordField.Text, this);
        }


        //open sign in page
        public void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            this.Navigation.PushAsync(new SignUpPage(uc));
          
        }

        //open forgot password
        public void TappedHandler(object sender, EventArgs args)
        {
            //this.Navigation.PushAsync(new ForgotPasswordPage());
        }

       
    }
}