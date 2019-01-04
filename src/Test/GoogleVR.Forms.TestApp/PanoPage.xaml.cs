using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GoogleVR.Forms.TestApp
{
    public partial class PanoPage : ContentPage
    {
        public VrPanorama Panorama => panorama;

        public PanoPage()
        {
            InitializeComponent();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Panorama clicked");
        }

        private void OnDisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"New Display Mode: {e.DisplayMode}");
        }

        private void OnLoadSuccess(object sender, LoadSuccessEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Loaded successfully");
        }

        private void OnLoadError(object sender, LoadErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Load error: {e.ErrorMessage}");
        }
    }
}
