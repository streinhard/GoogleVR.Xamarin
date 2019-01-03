using System;
using GoogleVR.iOS;
using Xamarin.Forms;

namespace GoogleVR.Forms
{
    public interface IVrImageLoader
    {
        void LoadImageInto(ImageSource imageSource, PanoramaView panoramaView, PanoramaImageType imageType);
    }
}
