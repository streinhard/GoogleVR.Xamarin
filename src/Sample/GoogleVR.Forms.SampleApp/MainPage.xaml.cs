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

        private void ShowPanoStereo(object sender, System.EventArgs e)
        {
            var panoPage = new PanoPage();
            panoPage.Panorama.ImageSource = ImageSource.FromFile("test_stereo_2k.jpg");
            panoPage.Panorama.SourceType = VrSourceType.StereoOverUnder;
            Navigation.PushAsync(panoPage);
        }

        private void ShowPanoMono(object sender, System.EventArgs e)
        {
            var panoPage = new PanoPage();
            panoPage.Panorama.ImageSource = ImageSource.FromFile("test_mono_2k.jpg");
            panoPage.Panorama.SourceType = VrSourceType.Mono;
            Navigation.PushAsync(panoPage);
        }
    }
}
