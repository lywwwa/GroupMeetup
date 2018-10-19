using System.Net;
using Xamarin.Forms;
using Android.Webkit;
using System;

using Android.OS;

[assembly: Dependency(typeof(GroupMeetup.Droid.DeviceSerial))]
namespace GroupMeetup.Droid
{
    public class DeviceSerial : IDeviceSerial
    {
        public string getDeviceSerial()
        {
            if ((int)Build.VERSION.SdkInt < 26) return Android.OS.Build.Serial;
            else return Build.GetSerial();
        }
    }
}