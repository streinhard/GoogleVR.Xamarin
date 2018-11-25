using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoogleVR.Forms.SampleApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowPanoStereo(object sender, EventArgs e)
        {
            var panoPage = new PanoPage();
            panoPage.Panorama.ImageSource = ImageSource.FromFile("test_2k_stereo.jpg");
            panoPage.Panorama.SourceType = VrSourceType.StereoOverUnder;
            panoPage.Panorama.TransitionViewEnabled = false;
            panoPage.Panorama.TouchTrackingEnabled = false;
            Navigation.PushAsync(panoPage);
        }

        private void ShowPanoMono(object sender, EventArgs e)
        {
            var panoPage = new PanoPage();
            panoPage.Panorama.ImageSource = ImageSource.FromFile("test_2k_mono.jpg");
            panoPage.Panorama.SourceType = VrSourceType.Mono;
            panoPage.Panorama.StereoModeButtonEnabled = false;
            Navigation.PushAsync(panoPage);
        }

        private void ShowPanoUrl(object sender, EventArgs e)
        {
            var panoPage = new PanoPage();
            var imageUri = new Uri("https://infosky.ch/media/road.jpg");
            panoPage.Panorama.ImageSource = ImageSource.FromUri(imageUri);
            panoPage.Panorama.SourceType = VrSourceType.StereoOverUnder;
            panoPage.Panorama.InfoButtonEnabled = false;
            Navigation.PushAsync(panoPage);
        }
    }
}
