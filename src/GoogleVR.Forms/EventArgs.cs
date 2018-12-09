using System;
namespace GoogleVR.Forms
{
    public class DisplayModeChangedEventArgs : EventArgs
    {
        public VrDisplayMode DisplayMode;
    }

    public class LoadErrorEventArgs : EventArgs
    {
        public string ErrorMessage;
    }
}
