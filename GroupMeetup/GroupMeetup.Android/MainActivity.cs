using System;
using System.Threading.Tasks;

using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GroupMeetup.Droid
{
    [Activity(Label = "GroupMeetup", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        int[] grantResults = { 1, 1 };
        protected async override void OnCreate(Bundle bundle)
        {
            await TryToGetPermissions();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App());
        }

        #region RuntimePermissions

        async Task TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermissionsAsync();
                return;
            }


        }
        const int RequestLocationId = 0;

        readonly string[] PermissionsGroupLocation ={
            //TODO add more permissions
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
        };
        async Task GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                //TODO change the message to show the permissions name
                Toast.MakeText(this, "Location permissions granted", ToastLength.Short).Show();
                return;
            }
            
            if (ShouldShowRequestPermissionRationale(permission))
            {
                RequestPermissions(PermissionsGroupLocation, RequestLocationId);
            }

        }

        void OnRequestPermissionsResult(int requestCode, string[] permissions, int[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();
                        }
                        else
                        {
                            //Permission Denied :(
                            Toast.MakeText(this, "Location permission denied, exiting", ToastLength.Long).Show();
                            killApp(2000);
                        }
                    }
                    break;
            }
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion

        async void killApp(int milli)
        {
            await Task.Delay(milli);
            Process.KillProcess(Process.MyPid());
        }
    }
}

