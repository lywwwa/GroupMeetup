using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupMeetup.Controllers;
using GroupMeetup.Models;

namespace GroupMeetup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        UserController uc;
        User currentUser;
        public HomePage (UserController ucon, User cUser)
        {
            currentUser = cUser;
            uc = ucon;
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        public void Account_Clicked(object w,EventArgs args)
        {
            this.Navigation.PushAsync(new Views.TabbedPages.ProfilePage(uc, currentUser));
        }


        public void OnButtonClicked(object a, EventArgs e)
        {
            this.Navigation.PushAsync(new Views.SearchPage(uc));
        }

        public void Logout_Clicked(object obj, EventArgs a)
        {
            //this.Navigation.PushAsync(new LoginPage());
            // this.Navigation.RemovePage(this);
            //remove all info
        }

    }
}