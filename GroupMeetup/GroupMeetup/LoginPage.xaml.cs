using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GroupMeetup.DataSource;

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
            string un = Username.Text;
            string pw = Password.Text;

            

            if (string.IsNullOrEmpty(un)|| string.IsNullOrEmpty(pw))
            {
                prompt.Text = "Wrong Username and Password";
            }
            else
            {
                if (un == "lywwwa" && pw== "password")
                {
                    this.Navigation.PushAsync(new HomePage());
                    this.Navigation.RemovePage(this);
                }
                else
                {
                    prompt.Text = "Wrong Username and Password";
                }

            }
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