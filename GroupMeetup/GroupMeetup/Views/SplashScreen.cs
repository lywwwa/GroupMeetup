using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;
using GroupMeetup.Controllers;
using System.Diagnostics;

namespace GroupMeetup
{
    class SplashScreen: ContentPage
    {
        Image splashImage;
        UserController uc;
        public SplashScreen(UserController ucon)
        {
            uc = ucon;
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "logo.png",
                WidthRequest = 200,
                HeightRequest = 200
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
               AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
             new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#01a1ff");
            this.Content = sub;

        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000); //Time-consuming processes such as initialization
            await this.Navigation.PushAsync(new LoginPage());  //After loading  MainPage it gets Navigated to our new Page
            this.Navigation.RemovePage(this);

        }
    }
}
