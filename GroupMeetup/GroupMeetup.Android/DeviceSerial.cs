using System.Net;
using Xamarin.Forms;
using Android.Webkit;
using System;

[assembly: Dependency(typeof(GroupMeetup.Droid.DeviceSerial))]
namespace GroupMeetup.Droid
{
    public class DeviceSerial : IDeviceSerial
    {
        public string getDeviceSerial()
        {
            return Android.OS.Build.Serial;
        }
    }
}