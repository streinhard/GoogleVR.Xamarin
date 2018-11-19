using System;
using CoreGraphics;
using UIKit;

namespace GoogleVR.iOS.BindingTest
{
    public class MainViewController : UIViewController
    {
        UIButton videoButton;
        UIButton panoButton;

        public MainViewController()
        {
            Title = "Google VR";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            videoButton = new UIButton(UIButtonType.System);
            videoButton.SetTitle("Video", UIControlState.Normal);
            videoButton.TouchUpInside += ShowVideo;
            View.Add(videoButton);

            panoButton = new UIButton(UIButtonType.System);
            panoButton.SetTitle("Panorama", UIControlState.Normal);
            panoButton.TouchUpInside += ShowPanorama;
            View.Add(panoButton);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            videoButton.Frame = new CGRect(0, 100, View.Frame.Width, 50);
            panoButton.Frame = new CGRect(0, 150, View.Frame.Width, 50);
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
