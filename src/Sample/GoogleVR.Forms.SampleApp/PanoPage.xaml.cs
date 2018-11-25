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
    }
}
