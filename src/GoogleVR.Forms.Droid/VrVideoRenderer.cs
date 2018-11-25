using System;
using System.ComponentModel;
using Android.Content;
using GoogleVR.Forms;
using GoogleVR.Widgets.Video;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VrVideo), typeof(VrVideoRenderer))]
namespace GoogleVR.Forms
{
    public class VrVideoRenderer : VrWidgetRender<VrVideo, VrVideoView>
    {
        public VrVideoRenderer(Context context) : base(context) {}

        protected override void OnElementChanged(ElementChangedEventArgs<VrVideo> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new VrVideoView(Context));
            }

            if (e.NewElement != null)
            {
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
    }
}
