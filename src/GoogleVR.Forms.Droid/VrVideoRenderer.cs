using System;
using System.ComponentModel;
using Android.Content;
using Android.Runtime;
using GoogleVR.Forms;
using GoogleVR.Widgets.Video;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VrVideo), typeof(VrVideoRenderer))]
namespace GoogleVR.Forms
{
    public class VrVideoRenderer : VrWidgetRender<VrVideo, VrVideoView>, IVrVideoRenderer
    {
        public VrVideoRenderer(Context context) : base(context) {}

        protected override void OnElementChanged(ElementChangedEventArgs<VrVideo> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new VrVideoView(Context));
                Control.SetEventListener(new VideoEventListener(this));
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
                Control?.PauseVideo();
                Control?.PauseRendering();
            }

            base.Dispose(disposing);
        }

        private void LoadVideo()
        {
            var videoSource = Element.VideoSource;
            if (videoSource == null) return;

            var videoUri = TryParseUri(videoSource);
            if (videoUri != null)
            {
                Control.LoadVideo(videoUri, GetOptions());
            }
            else
            {
                Control.LoadVideoFromAsset(videoSource, GetOptions());
            }
        }

        private Android.Net.Uri TryParseUri(string videoSource)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(videoSource);
                return uri.Scheme == null ? null : uri;
            }
            catch (Java.Lang.NullPointerException)
            {
                return null;
            }
        }

        private VrVideoView.Options GetOptions()
        {
            return new VrVideoView.Options
            {
                InputType = (int)Element.SourceType,
                InputFormat = VrVideoView.Options.FormatDefault // TODO: Make configurable? Not supported on iOS :(
            };
        }

        public void PlayVideo()
        {
            Control?.PlayVideo();
        }

        public void PauseVideo()
        {
            Control.PauseVideo();
        }

        public void SeekTo(long position)
        {
            Control?.SeekTo(position);
        }
    }

    class VideoEventListener : VrVideoEventListener
    {
        private VrVideoRenderer _renderer;

        public VideoEventListener(IntPtr javaReference, JniHandleOwnership ownership) : base(javaReference, ownership)
        {
        }

        public VideoEventListener(VrVideoRenderer renderer)
        {
            this._renderer = renderer;
        }

        public override void OnClick()
        {
            _renderer?.Element?.OnClicked();
        }

        public override void OnDisplayModeChanged(int newDisplayMode)
        {
            _renderer?.Element?.OnDisplayModeChanged((VrDisplayMode)newDisplayMode);
        }

        public override void OnLoadSuccess()
        {
            if (_renderer.Control == null) return;

            var videoDuration = _renderer.Control.Duration;
            _renderer?.Element?.OnLoadSuccess(videoDuration);
        }

        public override void OnLoadError(string errorMessage)
        {
            _renderer?.Element?.OnLoadError(errorMessage);
        }

        public override void OnNewFrame()
        {
            if (_renderer.Control == null) return;

            var position = _renderer.Control.CurrentPosition;
            _renderer?.Element?.OnNewFrame(position);
        }

        public override void OnCompletion()
        {
            _renderer?.Element?.OnCompleted();
        }
    }
}
