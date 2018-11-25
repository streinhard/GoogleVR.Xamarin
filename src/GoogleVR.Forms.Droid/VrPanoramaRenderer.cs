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
                LoadImageAsync().ConfigureAwait(false);
            }
        }

        private async Task LoadImageAsync()
        {
            var bitmap = await LoadBitmapFromImageSource(Element.ImageSource);
            if (bitmap == null) return;

            try
            {
                Control.LoadImageFromBitmap(bitmap, GetOptions());
            }
            catch (Java.IO.IOException)
            {
                Log.Error(TAG, $"Could not load image {Element.ImageSource}");
            }
        }

        private async Task<Bitmap> LoadBitmapFromImageSource(ImageSource imageSource)
        {
            IImageSourceHandler handler;

            if (imageSource is FileImageSource)
                handler = new FileImageSourceHandler();
            else if (imageSource is StreamImageSource)
                handler = new StreamImagesourceHandler();
            else if (imageSource is UriImageSource)
                handler = new ImageLoaderSourceHandler();
            else
                return null;

            return await handler.LoadImageAsync(imageSource, Context);
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
