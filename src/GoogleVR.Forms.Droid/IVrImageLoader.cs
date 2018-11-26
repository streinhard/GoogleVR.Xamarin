using System;
using Android.Content;
using GoogleVR.Widgets.Pano;
using Xamarin.Forms;

namespace GoogleVR.Forms
{
    public interface IVrImageLoader
    {
        void LoadImageInto(
            Context context,
            ImageSource imageSource,
            VrPanoramaView panoramaView,
            VrPanoramaView.Options options
        );
    }
}
