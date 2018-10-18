using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using System.Diagnostics;

namespace GroupMeetup.TabbedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GPSPagexaml : ContentPage
	{
        Plugin.Geolocator.Abstractions.IGeolocator Geolocator = CrossGeolocator.Current;
        Plugin.Geolocator.Abstractions.Position CurrentPos;
        Pin p;
        public GPSPagexaml ()
        {
            GetLoc();
            InitializeComponent();
        }

        public async void GetLoc()
        {
            CurrentPos = await Geolocator.GetPositionAsync(TimeSpan.FromSeconds(5));
            gpsMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(CurrentPos.Latitude, CurrentPos.Longitude), Distance.FromMiles(1)));
            //p.Position = new Position(CurrentPos.Latitude, CurrentPos.Longitude);
            //gpsMap.Pins.Add(p);
        }
    }
}