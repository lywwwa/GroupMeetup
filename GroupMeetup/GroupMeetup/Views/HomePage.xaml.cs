using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupMeetup.Controllers;
using GroupMeetup.Models;
using GroupMeetup.Views;

namespace GroupMeetup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        UserController uc;
        public User currentUser;
        public HomePage (UserController ucon, User cUser)
        {
            currentUser = cUser;
            uc = ucon;
            InitializeComponent();
            
            Children.Add(new TabbedPages.FriendsPage(uc));
            Children.Add(new TabbedPages.NotificationPage(ucon.currentUser, ucon));
            Children.Add(new TabbedPages.GPSPagexaml());
            Children.Add(new TabbedPages.EventPage());
            NavigationPage.SetHasBackButton(this, false);
            //
            NavigationPage.SetHasNavigationBar(this, false);
            this.BarBackgroundColor = Color.FromHex("#392c4b");
        }

        public void Account_Clicked(object w,EventArgs args)
        {
            this.Navigation.PushAsync(new ProfilePage(uc, currentUser));
        }


       /* public void OnButtonClicked(object a, EventArgs e)
        {
            this.Navigation.PushAsync(new Views.SearchPage(uc));
        }*/

        public void Logout_Clicked(object obj, EventArgs a)
        {
            //this.Navigation.PushAsync(new LoginPage());
            // this.Navigation.RemovePage(this);
            //remove all info
        }

    }
}