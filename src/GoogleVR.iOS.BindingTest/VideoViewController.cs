using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace GoogleVR.iOS.BindingTest
{
    public class VideoViewController : UIViewController
    {
        private GVRVideoView _videoView;
        private UISlider _slider;
        private bool _isPlaying;

        public VideoViewController()
        {
            Title = "Video Widget";
            View.BackgroundColor = UIColor.White;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var videoUrl = NSBundle.MainBundle.GetUrlForResource("test_1080_stereo", "mp4");

            _videoView = new GVRVideoView();
            _videoView.EnableCardboardButton = true;
            _videoView.EnableFullscreenButton = true;
            _videoView.WeakDelegate = new VideoController(this);
            _videoView.LoadFromUrl(videoUrl, GVRVideoType.StereoOverUnder);
            View.Add(_videoView);

            _slider = new UISlider();
            _slider.TouchUpInside += OnSliderTouchUpInside;
            View.Add(_slider);
        }

        private void OnSliderTouchUpInside(object sender, EventArgs e)
        {
            _videoView.SeekTo(_slider.Value);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            _videoView.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height - 50);
            _slider.Frame = new CGRect(0, View.Frame.Height - 50, View.Frame.Width, 50);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillAppear(animated);

            _videoView.Stop();
        }

        class VideoController : GVRVideoViewDelegate
        {
            private VideoViewController _parent;

            public VideoController(VideoViewController _parent)
            {
                this._parent = _parent;
            }

            public override void DidTap(GVRWidgetView widgetView)
            {
                if (_parent._isPlaying)
                {
                    _parent._videoView.Pause();
                }
                else
                {
                    _parent._videoView.Play();
                }

                _parent._isPlaying = !_parent._isPlaying;
            }

            public override void DidChangeDisplayMode(GVRWidgetView widgetView, GVRWidgetDisplayMode displayMode)
            {
                Console.WriteLine("New display mode", displayMode);
            }

            public override void DidLoadContent(GVRWidgetView widgetView, NSObject content)
            {
                Console.WriteLine("Content loaded!");

                _parent._videoView.Play();
            }

            public override void DidFailToLoadContent(GVRWidgetView widgetView, NSObject content, string errorMessage)
            {
                Console.WriteLine("Failed to load: " + errorMessage);
            }

            public override void DidUpdatePosition(GVRVideoView videoView, double position)
            {
                _parent._slider.MaxValue = (float)_parent._videoView.Duration;

                if (position < _parent._slider.MaxValue) {
                    _parent._slider.Value = (float)position;
                }

                if (Math.Abs(_parent._videoView.Duration - position) < 0.01)
                {
                    _parent._videoView.SeekTo(0);
                    _parent._videoView.Play();
                }
            }
        }
    }
}
