using System;
using Xamarin.Forms;

namespace GoogleVR.Forms.SampleApp
{
    public partial class VideoPage : ContentPage
    {
        public VrVideo Video => video;

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

        void OnClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Video clicked");
        }

        void OnDisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"New Display Mode: {e.DisplayMode}");
        }

        void OnLoadSuccess(object sender, LoadVideoSuccessEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Loaded successfully");

            positionSlider.IsVisible = true;
            positionSlider.Maximum = e.VideoDuration;
        }

        void OnLoadError(object sender, LoadErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Load error: {e.ErrorMessage}");
        }

        void OnPositionChanged(object sender, NewVideoFrameEventArgs e)
        {
            positionSlider.Value = e.Position;
        }
    }
}
