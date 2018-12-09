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

        public event EventHandler<NewVideoFrameEventArgs> PositionChanged;
        public event EventHandler Completed;

        public event EventHandler<SeekToEventArgs> _SeekTo;
        public event EventHandler _PlayVideo;
        public event EventHandler _PauseVideo;

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

        public void Play()
        {
            _PlayVideo?.Invoke(this, EventArgs.Empty);
        }

        public void Pause()
        {
            _PauseVideo?.Invoke(this, EventArgs.Empty);
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
