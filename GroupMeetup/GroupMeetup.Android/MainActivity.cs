using System;
using System.Threading.Tasks;
using System.Diagnostics;

using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;

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
        const int RequestId = 0;

        readonly string[] PermissionsGroup ={
            //TODO add more permissions
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage
        };

        async Task GetPermissionsAsync()
        {
            const string permissionCoarseLocation = Manifest.Permission.AccessCoarseLocation;
            const string permissionFineLocation = Manifest.Permission.AccessFineLocation;
            const string permissionReadExternal = Manifest.Permission.ReadExternalStorage;
            const string permissionWriteExternal = Manifest.Permission.WriteExternalStorage;

            if (CheckSelfPermission(permissionCoarseLocation) == (int)Android.Content.PM.Permission.Granted || CheckSelfPermission(permissionFineLocation) == (int)Android.Content.PM.Permission.Granted || CheckSelfPermission(permissionReadExternal) == (int)Android.Content.PM.Permission.Granted || CheckSelfPermission(permissionWriteExternal) == (int)Android.Content.PM.Permission.Granted)
            {
                //TODO change the message to show the permissions name
                Toast.MakeText(this, "Permissions already granted: "+Build.Serial, ToastLength.Long).Show();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permissionCoarseLocation) || ShouldShowRequestPermissionRationale(permissionFineLocation) || ShouldShowRequestPermissionRationale(permissionReadExternal) || ShouldShowRequestPermissionRationale(permissionWriteExternal))
            {
                RequestPermissions(PermissionsGroup, RequestId);
            }
        }

        void OnRequestPermissionsResult(int requestCode, string[] permissions, int[] grantResults)
        {
            switch (requestCode)
            {
                case RequestId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            Toast.MakeText(this, "Permissions granted", ToastLength.Long).Show();
                        }
                        else
                        {
                            //Permission Denied :(
                            Toast.MakeText(this, "Permissions denied, exiting", ToastLength.Long).Show();
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
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}

