using System;
namespace GoogleVR.Forms
{
    public class DisplayModeChangedEventArgs : EventArgs
    {
        public VrDisplayMode DisplayMode;
    }

    public class LoadVideoSuccessEventArgs : EventArgs
    {
        public long VideoDuration;
    }

    public class LoadErrorEventArgs : EventArgs
    {
        public string ErrorMessage;
    }

    public class NewVideoFrameEventArgs : EventArgs
    {
        public long Position;
    }
}
