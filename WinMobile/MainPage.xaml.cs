using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinMobile.Resources;
using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using Microsoft.Phone.Net.NetworkInformation;
using Windows.UI.Notifications;

namespace WinMobile
{
    public partial class MainPage : FormsApplicationPage
    {
        // Constructor
        public UIChangedEvent MessageEvent { get; set; }
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();
            MessageEvent = new UIChangedEvent();
            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(ChangeDetected);

            MessageEvent.Change += (object s, UIChangedEventArgs ea) =>
             {
                 if (ea.ModuleName == "Notification")
                 {
                     var toast = new ShellToast
                     {
                         Title = "Connection changed",
                         Content = ea.Info,
                     };
                     toast.Show();
                 }
             };

            LoadApplication(new connectivity.App());
        }

        void ChangeDetected(object s, NetworkNotificationEventArgs e)
        {
            MessageEvent.BroadcastIt("Connection", e.NotificationType == NetworkNotificationType.InterfaceConnected ? true.ToString() : false.ToString());
        }
    }
}