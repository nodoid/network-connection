
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Net;

namespace connectivity.Droid
{
    [Activity(Label = "connectivity.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        static readonly int ButtonClickNotificationId = 1000;

        protected override void OnCreate(Bundle bundle)
        {
            var intentFilter = new IntentFilter();
            intentFilter.AddAction(ConnectivityManager.ConnectivityAction);
            RegisterReceiver(new Connectivity(), intentFilter);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            connectivity.Singleton.MessageEvents.Change += (object s, UIChangedEventArgs ea) =>
            {
                if (ea.ModuleName == "Notification")
                {
                    RunOnUiThread(() =>
                        {
                            var builder = new Notification.Builder(this)
                                    .SetAutoCancel(true)
                                    .SetContentTitle("Network state changed") 
                                    .SetContentText(ea.Info)
                                    .SetDefaults(NotificationDefaults.Vibrate)
                                    .SetContentText(ea.Info);

                            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                            notificationManager.Notify(ButtonClickNotificationId, builder.Build());
                        });
                }
            };

            LoadApplication(new App());
        }
    }
}

