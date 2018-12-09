using System;
using Xamarin.Forms;

namespace GoogleVR.Forms.SampleApp
{
    public partial class VideoPage : ContentPage
    {
        public VrVideo Video => video;

        private volatile bool _isSliderChangedFromUser = true;

        public VideoPage()
        {
            InitializeComponent();
        }

        private void OnPauseRenderingClicked(object sender, EventArgs e)
        {
            video.PauseRendering();
        }

        private void OnResumeRenderingClicked(object sender, EventArgs e)
        {
            video.ResumeRendering();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Video clicked");
        }

        private void OnDisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"New Display Mode: {e.DisplayMode}");
        }

        private void OnLoadSuccess(object sender, LoadSuccessEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Loaded successfully");

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

        private void OnPositionChanged(object sender, NewVideoFrameEventArgs e)
        {
            // Ugly, but a functional hack to prevent constant seeks
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

        private void OnPlay(object sender, EventArgs e)
        {
            video.Play();
        }

        private void OnPause(object sender, EventArgs e)
        {
            video.Pause();
        }
    }
}
