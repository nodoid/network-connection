using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace connectivity.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static AppDelegate Self { get; private set; }

        public bool IsConnected { get; set; }

        public UIChangedEvent MessageEvents { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppDelegate.Self = this;

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            // check if we can hear anything
            IsConnected = Reachability.IsHostReachable("www.ftracklive.com");
            // start the events process
            var startListening = Reachability.RemoteHostStatus(); // only needed so the events are started

            MessageEvents = new UIChangedEvent();

            // the network status has changed
            Reachability.ReachabilityChanged += delegate
            {
                // check if we're online or not
                IsConnected = Reachability.IsHostReachable("www.ftracklive.com");

                // broadcast the event
                MessageEvents.BroadcastIt("Connection", IsConnected.ToString());
            };

            // set up notifications
            var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType, null);
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);

            // produce notification
            MessageEvents.Change += (object s, UIChangedEventArgs ea) =>
            {
                if (ea.ModuleName == "Notification")
                {
                    var notification = new UILocalNotification
                    {
                        FireDate = DateTime.Now,
                        AlertAction = "Connection changed",
                        AlertBody = ea.Info,
                    };
                    UIApplication.SharedApplication.ScheduleLocalNotification(notification);
                }
            };

            return base.FinishedLaunching(app, options);
        }
    }
}

