using System;
using System.ComponentModel;
using Foundation;
using GoogleVR.Forms;
using GoogleVR.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VrPanorama), typeof(VrPanoramaRenderer))]
namespace GoogleVR.Forms
{
    public class VrPanoramaRenderer : VrWidgetRender<VrPanorama, PanoramaView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VrPanorama> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new PanoramaView());
                Control.Delegate = new PanoramaViewDelegate(this);
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

        private void LoadImage()
        {
            if (Element.ImageSource != null)
            {
                VrImageLoader.Instance.LoadImageInto(Element.ImageSource, Control, GetImageType());
            }
        }

        private PanoramaImageType GetImageType()
        {
            return (PanoramaImageType)Element.SourceType;
        }

        private class PanoramaViewDelegate : WidgetViewDelegate
        {
            private readonly VrPanoramaRenderer _renderer;

            public PanoramaViewDelegate(VrPanoramaRenderer renderer)
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
                _renderer?.Element?.OnLoadSuccess();
            }

            public override void DidFailToLoadContent(WidgetView widgetView, NSObject content, string errorMessage)
            {
                _renderer?.Element?.OnLoadError(errorMessage);
            }
        }
    }
}
