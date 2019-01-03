using System;
using System.Threading.Tasks;
using GoogleVR.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace GoogleVR.Forms
{
    public class DefaultImageLoader : IVrImageLoader
    {
        public virtual async void LoadImageInto(ImageSource imageSource, PanoramaView panoramaView, PanoramaImageType imageType)
        {
            var image = await LoadImageFromImageSource(imageSource);

            panoramaView.LoadImage(image, imageType);
        }

        private async Task<UIImage> LoadImageFromImageSource(ImageSource imageSource)
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

            return await handler.LoadImageAsync(imageSource);
        }
    }
}
