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
    public class VrVideoRenderer : ViewRenderer<VrVideo, VrVideoView>
    {
        public VrVideoRenderer(Context context) : base(context) {}

        protected override void OnElementChanged(ElementChangedEventArgs<VrVideo> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new VrVideoView(Context));
            }

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                UpdateAll();
                LoadVideo();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VrVideo.VideoSourceProperty.PropertyName ||
                e.PropertyName == VrVideo.SourceTypeProperty.PropertyName)
                LoadVideo();
            else if (e.PropertyName == VrWidget.InfoButtonEnabledProperty.PropertyName)
                UpdateInfoButtonEnabled();
            else if (e.PropertyName == VrWidget.TouchTrackingEnabledProperty.PropertyName)
                UpdateTouchTrackingEnabled();
            else if (e.PropertyName == VrWidget.StereoModeButtonEnabledProperty.PropertyName)
                UpdateStereoModeButtonEnabled();
            else if (e.PropertyName == VrWidget.TransitionViewEnabledProperty.PropertyName)
                UpdateTransitionViewEnabled();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Control != null)
            {
                Control.PauseVideo();
                Control.PauseRendering();
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

        private void UpdateAll()
        {
            UpdateInfoButtonEnabled();
            UpdateTouchTrackingEnabled();
            UpdateStereoModeButtonEnabled();
            UpdateTransitionViewEnabled();
        }

        private void UpdateInfoButtonEnabled()
        {
            Control.SetInfoButtonEnabled(Element.InfoButtonEnabled);
        }

        private void UpdateTouchTrackingEnabled()
        {
            Control.SetTouchTrackingEnabled(Element.TouchTrackingEnabled);
        }

        private void UpdateStereoModeButtonEnabled()
        {
            Control.SetStereoModeButtonEnabled(Element.StereoModeButtonEnabled);
        }

        private void UpdateTransitionViewEnabled()
        {
            Control.SetTransitionViewEnabled(Element.TransitionViewEnabled);
        }
    }
}
