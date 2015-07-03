using System;

using Xamarin.Forms;

namespace connectivity
{
    public class App : Application
    {
        public static App Self { get; private set; }

        public UIChangedEvent MessageEvent { get; set; }

        public bool IsConnected { get; private set; }

        public App()
        {
            IsConnected = DependencyService.Get<IConnectivity>().NetworkConnected();

            MessageEvent = new UIChangedEvent();

            MessageEvent.Change += (object s, UIChangedEventArgs ea) =>
            {
                if (ea.ModuleName == "Connection")
                {
                    IsConnected = Convert.ToBoolean(ea.Info);
                    MessageEvent.BroadcastIt("Notification", "You have {0} connection", IsConnected ? "a" : "no");
                }
            };

            // The root page of your application
            MainPage = new ConnectionState();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

