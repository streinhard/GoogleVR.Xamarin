using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GoogleVR.Forms;
using GoogleVR.Widgets.Pano;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VrPanorama), typeof(VrPanoramaRenderer))]
namespace GoogleVR.Forms
{
    public class VrPanoramaRenderer : ViewRenderer<VrPanorama, VrPanoramaView>
    {
        public VrPanoramaRenderer(Android.Content.Context context) : base(context) {}

        protected override void OnElementChanged(ElementChangedEventArgs<VrPanorama> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new VrPanoramaView(Context));
            }

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                UpdateAll();
                LoadImage();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VrPanorama.ImageSourceProperty.PropertyName ||
                e.PropertyName == VrPanorama.SourceTypeProperty.PropertyName)
                LoadImage();
            else if (e.PropertyName == VrWidget.InfoButtonEnabledProperty.PropertyName)
                UpdateInfoButtonEnabled();
            else if (e.PropertyName == VrWidget.TouchTrackingEnabledProperty.PropertyName)
                UpdateTouchTrackingEnabled();
            else if (e.PropertyName == VrWidget.StereoModeButtonEnabledProperty.PropertyName)
                UpdateStereoModeButtonEnabled();
            else if (e.PropertyName == VrWidget.TransitionViewEnabledProperty.PropertyName)
                UpdateTransitionViewEnabled();

        }

        private void UpdateAll()
        {
            UpdateInfoButtonEnabled();
            UpdateTouchTrackingEnabled();
            UpdateStereoModeButtonEnabled();
            UpdateTransitionViewEnabled();
        }

        private void LoadImage()
        {
            if (Element.ImageSource != null)
            {
                Task.Run(LoadImageAsync);
            }
        }

        private async Task LoadImageAsync()
        {
            if (Element.ImageSource is IImageSourceHandler imageSourceHandler)
            {
                // TODO: Error handling
                var bitmap = await imageSourceHandler.LoadImageAsync(Element.ImageSource, Context);
                Control.LoadImageFromBitmap(bitmap, GetOptions());
            }
        }

        private VrPanoramaView.Options GetOptions()
        {
            return new VrPanoramaView.Options
            {
                InputType = (int)Element.SourceType
            };
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
