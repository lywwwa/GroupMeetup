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
	public partial class SigninPage : ContentPage
	{
		public SigninPage ()
		{
			InitializeComponent ();
            this.BackgroundColor = Color.FromHex("#00313c");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //for sign in
            //check if pass==reppass
            //check if email is already existing
        }

    }
}