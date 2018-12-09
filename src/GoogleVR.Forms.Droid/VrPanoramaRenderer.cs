using System;
using System.ComponentModel;
using Android.Content;
using Android.Runtime;
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
                Control.SetEventListener(new PanoramaEventListener(this));
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

        private class PanoramaEventListener : VrPanoramaEventListener
        {
            private VrPanoramaRenderer _renderer;

            public PanoramaEventListener(IntPtr javaReference, JniHandleOwnership ownership) : base(javaReference, ownership)
            {
            }

            public PanoramaEventListener(VrPanoramaRenderer renderer)
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
                _renderer?.Element?.OnLoadSuccess();
            }

            public override void OnLoadError(string errorMessage)
            {
                _renderer?.Element?.OnLoadError(errorMessage);
            }
        }
    }
}
