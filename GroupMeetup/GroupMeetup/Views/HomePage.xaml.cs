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
    public partial class HomePage : TabbedPage
    {
        public HomePage ()
        {
            InitializeComponent();
        }

        public void Account_Clicked(object obj, EventArgs a)
        {
            this.Navigation.PushAsync(new Views.TabbedPages.ProfilePage());
            //
        }
    }
}