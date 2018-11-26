using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Util;
using GoogleVR.Widgets.Pano;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace GoogleVR.Forms
{
    public class VrImageLoader
    {
        private const string TAG = "VrImageLoader";

        private static VrImageLoader _instance;

        public static VrImageLoader Instance
        {
            get => _instance ?? (_instance = new VrImageLoader());
            set => _instance = value;
        }

        public virtual void LoadImageInto(
            Context context,
            ImageSource imageSource,
            VrPanoramaView panoramaView,
            VrPanoramaView.Options options
        )
        {
            LoadImageAsync(context, imageSource, panoramaView, options).ConfigureAwait(false);
        }

        private async Task LoadImageAsync(
            Context context,
            ImageSource imageSource,
            VrPanoramaView panoramaView,
            VrPanoramaView.Options options
        )
        {
            var bitmap = await LoadBitmapFromImageSource(context, imageSource).ConfigureAwait(false);
            if (bitmap == null) return;

            try
            {
                panoramaView.LoadImageFromBitmap(bitmap, options);
            }
            catch (Java.IO.IOException)
            {
                Log.Error(TAG, $"Could not load image {imageSource}");
            }
        }

        private async Task<Bitmap> LoadBitmapFromImageSource(Context context, ImageSource imageSource)
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

            return await handler.LoadImageAsync(imageSource, context);
        }
    }
}
