using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Util;
using GoogleVR.Forms;
using GoogleVR.Widgets.Pano;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VrPanorama), typeof(VrPanoramaRenderer))]
namespace GoogleVR.Forms
{
    public class VrPanoramaRenderer : VrWidgetRender<VrPanorama, VrPanoramaView>
    {
        private const string TAG = "VrPanoramaRenderer";

        public VrPanoramaRenderer(Context context) : base(context) {}

        protected override void OnElementChanged(ElementChangedEventArgs<VrPanorama> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new VrPanoramaView(Context));
            }

            if (e.NewElement != null)
            {
                UpdateWidget();
                LoadImage();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VrPanorama.ImageSourceProperty.PropertyName ||
                e.PropertyName == VrPanorama.SourceTypeProperty.PropertyName)
            {
                LoadImage();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control?.PauseRendering();
            }

            base.Dispose(disposing);
        }

        private void LoadImage()
        {
            if (Element.ImageSource != null)
            {
                VrImageLoader.Instance.LoadImageInto(Context, Element.ImageSource, Control, GetOptions());
            }
        }

        private VrPanoramaView.Options GetOptions()
        {
            return new VrPanoramaView.Options
            {
                InputType = (int)Element.SourceType
            };
        }
    }
}
