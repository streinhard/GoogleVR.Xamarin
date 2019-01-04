using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace GoogleVR.iOS.BindingTest
{
    public class MainViewController : UIViewController
    {
        private int _heightOffset = 100;

        public MainViewController()
        {
            Title = "Google VR";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            var videoStereo = AddButton("Video Stereo 1080p");
            var videoMono = AddButton("Video Mono 1920p");
            var videoUrl = AddButton("Video from URL");
            var panoStereo = AddButton("Panorama Stereo 2k");
            var panoMono = AddButton("Panorama Mono 2k");
            var panoUrl = AddButton("Panorama form URL");

            videoStereo.TouchUpInside += ShowVideoStereo;
            videoMono.TouchUpInside += ShowVideoMono;
            videoUrl.TouchUpInside += ShowVideoUrl;
            panoStereo.TouchUpInside += ShowPanoStereo;
            panoMono.TouchUpInside += ShowPanoMono;
            panoUrl.TouchUpInside += ShowPanoUrl;

        }

        private UIButton AddButton(string title)
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);
            button.Frame = new CGRect(0, _heightOffset, View.Frame.Width, 50);
            View.Add(button);

            _heightOffset += 50;

            return button;
        }

        private void ShowVideoStereo(object sender, EventArgs e)
        {
            var videoViewController = new VideoViewController
            {
                VideoUrl = NSBundle.MainBundle.GetUrlForResource("test_1080_stereo", "mp4"),
                VideoType = VideoType.StereoOverUnder
            };

            NavigationController.PushViewController(videoViewController, true);
        }

        private void ShowVideoMono(object sender, EventArgs e)
        {
            var videoViewController = new VideoViewController
            {
                VideoUrl = NSBundle.MainBundle.GetUrlForResource("test_1920_mono", "mp4"),
                VideoType = VideoType.Mono
            };

            NavigationController.PushViewController(videoViewController, true);
        }

        private void ShowVideoUrl(object sender, EventArgs e)
        {
            var videoViewController = new VideoViewController
            {
                VideoUrl = NSUrl.FromString("https://infosky.ch/media/office_2k.mp4"),
                VideoType = VideoType.StereoOverUnder
            };

            NavigationController.PushViewController(videoViewController, true);
        }

        private void ShowPanoStereo(object sender, EventArgs e)
        {
            var panoViewController = new PanoViewController
            {
                PanoImage = new UIImage("test_2k_stereo.jpg"),
                ImageType = PanoramaImageType.StereoOverUnder
            };

            NavigationController.PushViewController(panoViewController, true);
        }

        private void ShowPanoMono(object sender, EventArgs e)
        {
            var panoViewController = new PanoViewController
            {
                PanoImage = new UIImage("test_2k_mono.jpg"),
                ImageType = PanoramaImageType.Mono
            };

            NavigationController.PushViewController(panoViewController, true);
        }

        private void ShowPanoUrl(object sender, EventArgs e)
        {
            var imageData = NSData.FromUrl(NSUrl.FromString("https://infosky.ch/media/road.jpg"));
            var panoViewController = new PanoViewController
            {
                PanoImage = UIImage.LoadFromData(imageData),
                ImageType = PanoramaImageType.StereoOverUnder
            };

            NavigationController.PushViewController(panoViewController, true);
        }
    }
}
