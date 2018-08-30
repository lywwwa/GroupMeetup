using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupMeetup.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupMeetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
        UserController uc;
		public SignUpPage (UserController ucon)
		{
            uc = ucon;
			InitializeComponent ();
            this.BackgroundColor = Color.FromHex("#00313c");
        }

        private void SignUpButtonClicked(object sender, EventArgs e)
        {
            uc.Signup(usernameSignup.Text, passwordSignup.Text, passwordSignupRepeat.Text, firstNameField.Text, lastNameField.Text, this);
            //for sign in
            //check if pass==reppass
            //check if email is already existing
        }

    }
}