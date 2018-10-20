using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using GroupMeetup.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;

namespace GroupMeetup
{
    public interface IDeviceSerial
    {
        string getDeviceSerial();
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        UserController uc;
        GroupController gc;
        string DeviceSerial;
        public LoginPage ()
		{
            InitializeComponent();
            uc = new UserController();
            gc = new GroupController();
            this.BackgroundColor = Color.FromHex("#01a1ff");
            NavigationPage.SetHasNavigationBar(this, false);
            DeviceSerial = DependencyService.Get<IDeviceSerial>().getDeviceSerial();
        }

        private void LoginClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(usernameField.Text) && !string.IsNullOrEmpty(passwordField.Text)) uc.Login(usernameField.Text, passwordField.Text, DeviceSerial, this, gc);
            else DisplayAlert("Error", "Fields cannot be blank.", "Try again");
        }


        //open sign in page
        public void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            this.Navigation.PushAsync(new SignUpPage(uc));
          
        }

        //open forgot password
        public void TappedHandler(object sender, EventArgs args)
        {
            DisplayAlert("DeviceID", "Serial: " + DeviceSerial, "ok");
        //this.Navigation.PushAsync(new ForgotPasswordPage());
        }

       
    }
}