
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupChat.ViewModels;
using GroupChat.Views;

namespace GroupChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupListViewPage : ContentPage
    {

        public GroupListViewPage()
        {
            InitializeComponent();
            // set custom view
            MyListView.ItemTemplate = new DataTemplate(typeof(GroupViewCell));
            MyListView.ItemsSource = TemporaryDatabase.DefaultGroups;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
