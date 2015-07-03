using System;
using connectivity.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Connection))]
namespace connectivity.Droid
{
    public class Connection: Java.Lang.Object, IConnectivity
    {
        public Connection()
        {
        }

        public bool NetworkConnected()
        {
            var connected = false;
            if (NetworkUtils.HaveConnection)
                connected = true;
            if (NetworkUtils.HaveWifiConnection)
                connected = true;
            return connected;
        }
    }
}

