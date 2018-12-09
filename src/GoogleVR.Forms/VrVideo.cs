using System;
using Xamarin.Forms;

namespace GoogleVR.Forms
{
    public class VrVideo : VrWidget
    {
        public static BindableProperty VideoSourceProperty =
            BindableProperty.Create(nameof(VideoSource), typeof(string), typeof(VrVideo));

        public static BindableProperty SourceTypeProperty =
            BindableProperty.Create(nameof(SourceType), typeof(VrSourceType), typeof(VrVideo), VrSourceType.Mono);

        public event EventHandler Clicked;
        public event EventHandler<DisplayModeChangedEventArgs> DisplayModeChanged;
        public event EventHandler<LoadVideoSuccessEventArgs> LoadSuccess;
        public event EventHandler<LoadErrorEventArgs> LoadError;
        public event EventHandler<NewVideoFrameEventArgs> PositionChanged;
        public event EventHandler Completed;

        public event EventHandler<SeekToEventArgs> _SeekTo;

        public string VideoSource
        {
            get => (string)GetValue(VideoSourceProperty);
            set => SetValue(VideoSourceProperty, value);
        }

        public VrSourceType SourceType
        {
            get => (VrSourceType)GetValue(SourceTypeProperty);
            set => SetValue(SourceTypeProperty, value);
        }

        public void SeekTo(long position)
        {
            _SeekTo?.Invoke(this, new SeekToEventArgs { Position = position });
        }

        public void _OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void _OnDisplayModeChanged(VrDisplayMode newDisplayMode)
        {
            DisplayModeChanged?.Invoke(this, new DisplayModeChangedEventArgs
            {
                DisplayMode = newDisplayMode
            });
        }

        public void _OnLoadSuccess(long videoDuration)
        {
            LoadSuccess?.Invoke(this, new LoadVideoSuccessEventArgs
            {
                VideoDuration = videoDuration
            });
        }

        public void _OnLoadError(string errorMessage)
        {
            LoadError?.Invoke(this, new LoadErrorEventArgs
            {
                ErrorMessage = errorMessage
            });
        }

        public void _OnNewFrame(long position)
        {
            PositionChanged?.Invoke(this, new NewVideoFrameEventArgs
            {
                Position = position
            });
        }

        public void _OnCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}
