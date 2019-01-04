using System;
using System.ComponentModel;
using System.IO;
using Foundation;
using GoogleVR.Forms;
using GoogleVR.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VrVideo), typeof(VrVideoRenderer))]
namespace GoogleVR.Forms
{
    public class VrVideoRenderer : VrWidgetRender<VrVideo, VideoView>, IVrVideoRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VrVideo> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new VideoView());
                Control.Delegate = new VideoDelegate(this);
            }

            if (e.OldElement != null)
            {
                e.OldElement.Renderer = null;
            }

            if (e.NewElement != null)
            {
                e.NewElement.Renderer = this;
                UpdateWidget();
                LoadVideo();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VrVideo.VideoSourceProperty.PropertyName ||
                e.PropertyName == VrVideo.SourceTypeProperty.PropertyName)
            {
                LoadVideo();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control?.Stop();
                Control?.RemoveFromSuperview();
            }

            base.Dispose(disposing);
        }

        private void LoadVideo()
        {
            var videoSource = Element.VideoSource;
            if (videoSource == null) return;

            var videoUrl = TryParseVideoUrl(videoSource) ?? TryVideoBundleUrl(videoSource);

            if (videoUrl == null)
            {
                System.Diagnostics.Debug.WriteLine($"Invalid video source: {videoSource}");
                return;
            }

            var videoType = (VideoType)Element.SourceType;

            Control.LoadFromUrl(videoUrl, videoType);
        }

        private NSUrl TryParseVideoUrl(string videoSource)
        {
            var url = new NSUrl(videoSource);
            return url.Scheme == null ? null : url;
        }

        private NSUrl TryVideoBundleUrl(string videoSource)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(videoSource);
            var extension = Path.GetExtension(videoSource);
            return NSBundle.MainBundle.GetUrlForResource(fileNameWithoutExtension, extension);
        }

        public void PauseVideo()
        {
            Control?.Pause();
        }

        public void PlayVideo()
        {
            Control?.Play();
        }

        public void SeekTo(long position)
        {
            Control?.SeekTo(position);
        }

        private class VideoDelegate : VideoViewDelegate
        {
            private readonly VrVideoRenderer _renderer;

            public VideoDelegate(VrVideoRenderer renderer)
            {
                this._renderer = renderer;
            }

            public override void DidTap(WidgetView widgetView)
            {
                _renderer?.Element?.OnClicked();
            }

            public override void DidChangeDisplayMode(WidgetView widgetView, WidgetDisplayMode displayMode)
            {
                _renderer?.Element?.OnDisplayModeChanged((VrDisplayMode)displayMode);
            }

            public override void DidLoadContent(WidgetView widgetView, NSObject content)
            {
                if (_renderer != null)
                {
                    // TODO: Think about this cast
                    var videoDuration = (long)_renderer.Control.Duration;
                    _renderer.Element?.OnLoadSuccess(videoDuration);
                }

                // Enable auto play on iOS
                _renderer?.Control?.Play();
            }

            public override void DidFailToLoadContent(WidgetView widgetView, NSObject content, string errorMessage)
            {
                _renderer?.Element?.OnLoadError(errorMessage);
            }

            public override void DidUpdatePosition(VideoView videoView, double position)
            {
                // TODO: Think about this cast
                _renderer?.Element?.OnNewFrame((long)position);

                // Fire completed event when end of video is reached
                if (_renderer.Control != null && Math.Abs(_renderer.Control.Duration - position) < 0.01)
                {
                    _renderer?.Element?.OnCompleted();
                }
            }
        }
    }
}
