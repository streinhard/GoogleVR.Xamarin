using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace GoogleVR.iOS.BindingTest
{
    public class VideoViewController : UIViewController
    {
        public NSUrl VideoUrl { get; set;  }
        public VideoType VideoType { get; set; }

        private VideoView _videoView;
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

            _videoView = new VideoView
            {
                EnableCardboardButton = true,
                EnableFullscreenButton = true,
                EnableTouchTracking = true,
                WeakDelegate = new VideoController(this)
            };
            View.Add(_videoView);

            _slider = new UISlider();
            _slider.TouchUpInside += OnSliderTouchUpInside;
            View.Add(_slider);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            _videoView.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height - 50);
            _slider.Frame = new CGRect(0, View.Frame.Height - 50, View.Frame.Width, 50);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (VideoUrl != null)
            {
                _videoView.LoadFromUrl(VideoUrl, VideoType);
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            _videoView.Stop();
            _videoView.RemoveFromSuperview();
        }

        private void OnSliderTouchUpInside(object sender, EventArgs e)
        {
            _videoView.SeekTo(_slider.Value);
        }

        class VideoController : VideoViewDelegate
        {
            private VideoViewController _parent;

            public VideoController(VideoViewController _parent)
            {
                this._parent = _parent;
            }

            public override void DidTap(WidgetView widgetView)
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

            public override void DidChangeDisplayMode(WidgetView widgetView, WidgetDisplayMode displayMode)
            {
                Console.WriteLine("New display mode", displayMode);
            }

            public override void DidLoadContent(WidgetView widgetView, NSObject content)
            {
                Console.WriteLine("Content loaded!");

                _parent._videoView.Play();
            }

            public override void DidFailToLoadContent(WidgetView widgetView, NSObject content, string errorMessage)
            {
                Console.WriteLine("Failed to load: " + errorMessage);
            }

            public override void DidUpdatePosition(VideoView videoView, double position)
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
