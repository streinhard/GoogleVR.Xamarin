using System;
using System.Collections.Generic;
using GoogleVR.Forms;
using Xamarin.Forms;

namespace GVRSample
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
