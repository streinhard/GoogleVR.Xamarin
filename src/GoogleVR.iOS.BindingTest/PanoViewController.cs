using System;
using UIKit;

namespace GoogleVR.iOS.BindingTest
{
    public class PanoViewController : UIViewController
    {
        public UIImage PanoImage { get; set; }
        public PanoramaImageType ImageType { get; set; }

        private PanoramaView _panoramaView;

        public PanoViewController()
        {
            Title = "Panorama Widget";
            View.BackgroundColor = UIColor.White;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _panoramaView = new PanoramaView
            {
                EnableCardboardButton = true,
                EnableFullscreenButton = true,
                EnableTouchTracking = true
            };

            View.Add(_panoramaView);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            _panoramaView.Frame = View.Frame;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (PanoImage != null)
            {
                _panoramaView.LoadImage(PanoImage, ImageType);
            }
        }
    }
}
