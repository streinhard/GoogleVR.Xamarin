using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GoogleVR.Forms.SampleApp
{
    public partial class PanoPage : ContentPage
    {
        public VrPanorama Panorama => panorama;

        public PanoPage()
        {
            InitializeComponent();
        }

        private void OnPauseRenderingClicked(object sender, EventArgs e)
        {
            panorama.PauseRendering();
        }

        private void OnResumeRenderingClicked(object sender, EventArgs e)
        {
            panorama.ResumeRendering();
        }

        void OnClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Panorama clicked");
        }

        void OnDisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"New Display Mode: {e.DisplayMode}");
        }

        void OnLoadSuccess(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Loaded successfully");
        }

        void OnLoadError(object sender, LoadErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Load error: {e.ErrorMessage}");
        }
    }
}
