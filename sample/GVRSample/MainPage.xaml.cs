using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleVR.Forms;
using Xamarin.Forms;

namespace GVRSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowVideoUrl(object sender, EventArgs e)
        {
            var videoPage = new VideoPage();
            videoPage.Video.VideoSource = "https://infosky.ch/media/office_2k.mp4";
            videoPage.Video.SourceType = VrSourceType.StereoOverUnder;
            Navigation.PushAsync(videoPage);
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
