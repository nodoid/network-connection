using System;

using Xamarin.Forms;

namespace connectivity
{
    public class ConnectionState : ContentPage
    {
        public ConnectionState()
        {
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);

            var titleLabel = new Label
            {
                FontSize = Font.OfSize("System", NamedSize.Large),
                TextColor = Color.Red,
                Text = "Network state"
            };

            var currentState = new Label
            {
                TextColor = App.Self.IsConnected ? Color.Green : Color.Red,
                Text = App.Self.IsConnected ? "connected" : "disconnected"
            };
            
            Content = new StackLayout
            { 
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    titleLabel,
                    new Label{ Text = "Current state : " },
                    currentState
                }
            };

            App.Self.MessageEvent.Change += (object s, UIChangedEventArgs ea) =>
            {
                if (ea.ModuleName == "Notification")
                {
                    currentState.TextColor = App.Self.IsConnected ? Color.Green : Color.Red;
                    currentState.Text = App.Self.IsConnected ? "connected" : "disconnected";
                }      
            };
        }
    }
}


