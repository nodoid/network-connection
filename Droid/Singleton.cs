using Android.Content;

namespace connectivity.Droid
{
    using Environment = System.Environment;

    public class connectivity
    {
        public Context AppContext;

        public static connectivity Singleton { get; private set; }

        public UIChangedEvent MessageEvents { get; set; }

        public ISharedPreferences prefs { get; private set; }

        public connectivity(Context c)
        {
            Singleton = this;
            AppContext = c;

            if (Singleton == null)
            {
                Singleton = this;
            }

            MessageEvents = new UIChangedEvent();

            prefs = c.GetSharedPreferences("ConPrefs", FileCreationMode.Private);
        }
    }
}