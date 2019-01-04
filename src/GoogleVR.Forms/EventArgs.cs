using System;
namespace GoogleVR.Forms
{
    public class DisplayModeChangedEventArgs : EventArgs
    {
        public VrDisplayMode DisplayMode;
    }

    public class LoadSuccessEventArgs : EventArgs
    {
        public double? Duration;
    }

    public class LoadErrorEventArgs : EventArgs
    {
        public string ErrorMessage;
    }

    public class PositionChangedEventArgs : EventArgs
    {
        public double Position;
    }

    public class SeekToEventArgs : EventArgs
    {
        public double Position;
    }
}
