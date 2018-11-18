using System;
using CoreGraphics;
using UIKit;
namespace GoogleVR.iOS.BindingTest
{
    public class MainViewController : UIViewController
    {
        public MainViewController()
        {
            Title = "Google VR";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            var videoButton = new UIButton(UIButtonType.System);
            videoButton.SetTitle("Video", UIControlState.Normal);
            videoButton.TouchUpInside += ShowVideo;
            videoButton.Frame = new CGRect(0, 100, View.Frame.Width, 50);
            View.Add(videoButton);

            var panoButton = new UIButton(UIButtonType.System);
            panoButton.SetTitle("Panorama", UIControlState.Normal);
            panoButton.TouchUpInside += ShowPanorama;
            panoButton.Frame = new CGRect(0, 150, View.Frame.Width, 50);
            View.Add(panoButton);
        }

        private void ShowVideo(object sender, EventArgs e)
        {
            var videoViewController = new VideoViewController();
            NavigationController.PushViewController(videoViewController, true);
        }

        private void ShowPanorama(object sender, EventArgs e)
        {
            var panoViewController = new PanoViewController();
            NavigationController.PushViewController(panoViewController, true);
        }
    }
}
