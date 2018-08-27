using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupMeetup.Controllers;

namespace GroupMeetup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        UserController uc;
        public HomePage (UserController ucon)
        {
            uc = ucon;
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        public void Account_Clicked(object w,EventArgs args)
        {
            this.Navigation.PushAsync(new Views.TabbedPages.ProfilePage(uc));
        }
    }
}