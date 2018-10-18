using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DurianCode.PlacesSearchBar;
using System.Diagnostics;

namespace GroupMeetup.TabbedPages.AdditionalPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEventPage : ContentPage
    {

        public AddEventPage()
        {

            InitializeComponent();

            search_bar.ApiKey = "AIzaSyCyQVh1VQEBkpYuonBeikY9QK1y35Djy2s";
            search_bar.Type = PlaceType.Establishment;
            search_bar.Components = new Components("country:ph");
            search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            search_bar.TextChanged += Search_Bar_TextChanged;
            search_bar.MinimumSearchText = 5;
            results_list.ItemSelected += Results_List_ItemSelected;
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            //add event on database for events
            //add the event on the selected attendees
        }

        void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
                results_list.IsVisible = true;
            else Trace.WriteLine(result.Status);
        }

        void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(search_bar.Text.Length == 5)
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    results_list.IsVisible = false;
                    spinner.IsVisible = true;
                    spinner.IsRunning = true;
                }
                else
                {
                    results_list.IsVisible = true;
                    spinner.IsRunning = false;
                    spinner.IsVisible = false;
                }
            }
                
        }

        async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var prediction = (AutoCompletePrediction)e.SelectedItem;
            results_list.SelectedItem = null;

            var place = await Places.GetPlace(prediction.Place_ID, search_bar.ApiKey);

            if (place != null) Trace.WriteLine(place.Name+" Lat: "+ place.Latitude + " Lon: "+ place.Longitude);
            else Trace.WriteLine("API SIGURO");
        }
    }
}