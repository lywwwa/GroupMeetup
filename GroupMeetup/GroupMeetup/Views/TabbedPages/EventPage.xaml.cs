using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventPage : ContentPage
	{
		public EventPage ()
		{
			InitializeComponent ();
		}

        public void NewEvent_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new TabbedPages.AdditionalPages.AddEventPage());
        }
	}
}