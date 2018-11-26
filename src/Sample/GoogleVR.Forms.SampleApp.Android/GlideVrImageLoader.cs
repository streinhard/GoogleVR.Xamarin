using System;
using Android.Content;
using Com.Bumptech.Glide;
using GoogleVR.Widgets.Pano;
using Xamarin.Forms;
using Com.Bumptech.Glide.Request.Target;
using Com.Bumptech.Glide.Request.Transition;
using Java.Lang;
using Android.Graphics.Drawables;

namespace GoogleVR.Forms.SampleApp.Droid
{
    public class GlideVrImageLoader : VrImageLoader
    {
        public override void LoadImageInto(
            Context context,
            ImageSource imageSource,
            VrPanoramaView panoramaView,
            VrPanoramaView.Options options
        )
        {
            if (imageSource is UriImageSource uriImageSource)
            {
                var target = new VrImageTarget(panoramaView, options);
                Glide.With(context).Load(uriImageSource.Uri.ToString()).Into(target);
            }
            else if (imageSource is FileImageSource fileImageSource)
            {
                var assetUri = Android.Net.Uri.Parse($"file:///android_asset/{fileImageSource.File}");
                var target = new VrImageTarget(panoramaView, options);
                Glide.With(context).Load(assetUri).Into(target);
            }
            else
            {
                base.LoadImageInto(context, imageSource, panoramaView, options);
            }
        }

        class VrImageTarget : SimpleTarget
        {
            private readonly VrPanoramaView _panoramaView;
            private readonly VrPanoramaView.Options _options;

            public VrImageTarget(VrPanoramaView _panoramaView, VrPanoramaView.Options _options)
            {
                this._panoramaView = _panoramaView;
                this._options = _options;
            }

            public override void OnResourceReady(Java.Lang.Object resource, ITransition transition)
            {
                if (resource is BitmapDrawable drawable)
                {
                    _panoramaView.LoadImageFromBitmap(drawable.Bitmap, _options);
                }
            }
        }
    }
}
