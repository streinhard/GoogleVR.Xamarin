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

        internal IVrVideoRenderer Renderer { get; set; }

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
            Renderer?.SeekTo(position);
        }

        public void PlayVideo()
        {
            Renderer?.PlayVideo();
        }

        public void PauseVideo()
        {
            Renderer?.PauseVideo();
        }

        internal void OnNewFrame(long position)
        {
            PositionChanged?.Invoke(this, new NewVideoFrameEventArgs
            {
                Position = position
            });
        }

        internal void OnCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}
