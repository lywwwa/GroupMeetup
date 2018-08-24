using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupMeetup.Controllers;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace GroupMeetup
{
	public partial class App : Application
	{
        UserController uc;
        public App ()
		{
            uc = new UserController();
			InitializeComponent();
            //MainPage = new MainPage();
            MainPage = new NavigationPage(new SplashScreen(uc));
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
