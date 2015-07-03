using System;
using Android.Net;
using connectivity.Droid;
using Xamarin.Forms;

namespace connectivity.Droid
{
    public static class NetworkUtils
    {
        public static bool HaveConnection
        {
            get
            {
                // we need to get the connectivity manager. We need to get the app context. We need the connectivity service
                var cm = (ConnectivityManager)Forms.Context.GetSystemService("connectivity");

                // get the active network information
                var activeNetwork = cm.ActiveNetworkInfo;

                // store the result
                var haveConnection = activeNetwork != null && activeNetwork.IsConnectedOrConnecting;
                ConfigUtils.SaveSetting("online", haveConnection, SettingType.Bool);
                return haveConnection;
            }
        }

        public static bool HaveWifiConnection
        {
            get
            {
                var cm = (ConnectivityManager)connectivity.Singleton.AppContext.GetSystemService("connectivity");
                var activeNetwork = cm.ActiveNetworkInfo;
                var haveWifi = activeNetwork != null && activeNetwork.IsConnectedOrConnecting && activeNetwork.Type == ConnectivityType.Wifi;
                ConfigUtils.SaveSetting("online", haveWifi, SettingType.Bool);
                return haveWifi;
            }
        }
    }
}

