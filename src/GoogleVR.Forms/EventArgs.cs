using System;
namespace GoogleVR.Forms
{
    public class DisplayModeChangedEventArgs : EventArgs
    {
        public VrDisplayMode DisplayMode;
    }

    public class LoadSuccessEventArgs : EventArgs
    {
        public long? Duration;
    }

    public class LoadErrorEventArgs : EventArgs
    {
        public string ErrorMessage;
    }

    public class NewVideoFrameEventArgs : EventArgs
    {
        public long Position;
    }

    public class SeekToEventArgs : EventArgs
    {
        public long Position;
    }
}
