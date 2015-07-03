using Android.Content;
using Android.OS;
using Android.Net;

namespace connectivity.Droid
{
    [BroadcastReceiver]
    public class Connectivity : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var extras = intent.Extras;

            using (var info = extras.GetParcelable("networkInfo") as NetworkInfo)
            {
                var state = info.GetState(); 

                var result = state == NetworkInfo.State.Connected || state == NetworkInfo.State.Connecting;

                // store the online state in the internal settings system  

                ConfigUtils.SaveSetting("online", result ? true : false, SettingType.Bool);

                // broadcast the event

                connectivity.Singleton.MessageEvents.BroadcastIt("Connection", result.ToString());
            }
        }
    }
}

