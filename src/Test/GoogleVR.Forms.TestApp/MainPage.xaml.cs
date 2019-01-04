using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoogleVR.Forms.TestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowVideoStereo(object sender, EventArgs e)
        {
            var videoPage = new VideoPage();
            videoPage.Video.VideoSource = "test_1080_stereo.mp4";
            videoPage.Video.SourceType = VrSourceType.StereoOverUnder;
            Navigation.PushAsync(videoPage);
        }

        private void ShowVideoMono(object sender, EventArgs e)
        {
            var videoPage = new VideoPage();
            videoPage.Video.VideoSource = "test_1920_mono.mp4";
            videoPage.Video.SourceType = VrSourceType.Mono;
            Navigation.PushAsync(videoPage);
        }

        private void ShowVideoUrl(object sender, EventArgs e)
        {
            var videoPage = new VideoPage();
            videoPage.Video.VideoSource = "https://infosky.ch/media/office_2k.mp4";
            videoPage.Video.SourceType = VrSourceType.StereoOverUnder;
            Navigation.PushAsync(videoPage);
        }

        private void ShowPanoStereo(object sender, EventArgs e)
        {
            var panoPage = new PanoPage();
            panoPage.Panorama.ImageSource = ImageSource.FromFile("test_2k_stereo.jpg");
            panoPage.Panorama.SourceType = VrSourceType.StereoOverUnder;
            Navigation.PushAsync(panoPage);
        }

        private void ShowPanoMono(object sender, EventArgs e)
        {
            var panoPage = new PanoPage();
            panoPage.Panorama.ImageSource = ImageSource.FromFile("test_2k_mono.jpg");
            panoPage.Panorama.SourceType = VrSourceType.Mono;
            Navigation.PushAsync(panoPage);
        }

        private void ShowPanoUrl(object sender, EventArgs e)
        {
            var panoPage = new PanoPage();
            var imageUri = new Uri("https://infosky.ch/media/road.jpg");
            panoPage.Panorama.ImageSource = ImageSource.FromUri(imageUri);
            panoPage.Panorama.SourceType = VrSourceType.StereoOverUnder;
            Navigation.PushAsync(panoPage);
        }
    }
}
