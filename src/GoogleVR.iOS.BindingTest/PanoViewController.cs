using System;
using UIKit;

namespace GoogleVR.iOS.BindingTest
{
    public class PanoViewController : UIViewController
    {
        GVRPanoramaView _panoramaView;

        public PanoViewController()
        {
            Title = "Panorama Widget";
            View.BackgroundColor = UIColor.White;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var image = new UIImage("test_2k_stereo.jpg");

            _panoramaView = new GVRPanoramaView();
            _panoramaView.EnableCardboardButton = true;
            _panoramaView.EnableFullscreenButton = true;
            _panoramaView.LoadImage(image, GVRPanoramaImageType.StereoOverUnder);
            View.Add(_panoramaView);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            _panoramaView.Frame = View.Frame;
        }
    }
}
