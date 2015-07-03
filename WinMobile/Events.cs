using System;

namespace WinMobile
{
    public class UIChangedEventArgs : EventArgs
    {
        public UIChangedEventArgs(string val = "", string info = "")
        {
            ModuleName = val;
            Info = info;
        }

        public readonly string ModuleName;
        public readonly string Info;
    }

    public class UIChangedEvent
    {
        public event UIChangeHandler Change;

        public delegate void UIChangeHandler(object s,UIChangedEventArgs ea);

        protected void OnChange(object s, UIChangedEventArgs e)
        {
            if (Change != null)
                Change(s, e);
        }

        public void BroadcastIt(string message, string info = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                var inf = new UIChangedEventArgs(message, info);
                OnChange(this, inf);
            }
        }
    }
}

