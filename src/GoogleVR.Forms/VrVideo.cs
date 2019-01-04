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

        public static BindableProperty AutoPlayProperty =
            BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(VrVideo), false);

        public static BindableProperty LoopProperty =
            BindableProperty.Create(nameof(Loop), typeof(bool), typeof(VrVideo), false);

        public event EventHandler<PositionChangedEventArgs> PositionChanged;
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

        public bool AutoPlay
        {
            get => (bool)GetValue(AutoPlayProperty);
            set => SetValue(AutoPlayProperty, value);
        }

        public bool Loop
        {
            get => (bool)GetValue(LoopProperty);
            set => SetValue(LoopProperty, value);
        }

        public void SeekTo(double position)
        {
            Renderer?.SeekTo(position);
        }

        public void SetVolume(float volume)
        {
            Renderer?.SetVolume(volume);
        }

        public void PlayVideo()
        {
            Renderer?.PlayVideo();
        }

        public void PauseVideo()
        {
            Renderer?.PauseVideo();
        }

        internal void OnPositionChanged(double position)
        {
            PositionChanged?.Invoke(this, new PositionChangedEventArgs
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
