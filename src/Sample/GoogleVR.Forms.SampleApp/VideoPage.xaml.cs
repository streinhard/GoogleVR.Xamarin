using System;
using Xamarin.Forms;

namespace GoogleVR.Forms.SampleApp
{
    public partial class VideoPage : ContentPage
    {
        public VrVideo Video => video;

        private bool _isSliderChangedFromUser = true;
        private bool _isPlaying;

        public VideoPage()
        {
            InitializeComponent();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                video.PauseVideo();
            }
            else
            {
                video.PlayVideo();
            }

            _isPlaying = !_isPlaying;
        }

        private void OnDisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"New Display Mode: {e.DisplayMode}");
        }

        private void OnLoadSuccess(object sender, LoadSuccessEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Loaded successfully");

            _isPlaying = true;

            positionSlider.IsVisible = true;
            if (e.Duration.HasValue)
            {
                positionSlider.Maximum = e.Duration.Value;
            }
        }

        private void OnLoadError(object sender, LoadErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Load error: {e.ErrorMessage}");
        }

        private void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            // Ugly, but functional hack to prevent constant seeks
            _isSliderChangedFromUser = false;
            positionSlider.Value = e.Position;
            _isSliderChangedFromUser = true;
        }

        private void OnVideoSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (_isSliderChangedFromUser)
            {
                System.Diagnostics.Debug.WriteLine($"SeekTo({e.NewValue})");
                video.SeekTo((long)e.NewValue);
            }
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Video completed");
        }
    }
}
