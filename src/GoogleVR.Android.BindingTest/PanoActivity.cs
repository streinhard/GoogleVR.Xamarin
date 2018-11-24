
using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Com.Google.VR.Sdk.Widgets.Pano;

namespace GoogleVR.Android.BindingTest
{
    [Activity]
    public class PanoActivity : AppCompatActivity
    {
        private VrPanoramaView panoramaView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Pano);

            panoramaView = FindViewById<VrPanoramaView>(Resource.Id.pano_view);

            LoadPanoramaFromIntent();
        }

        private void LoadPanoramaFromIntent()
        {
            var options = new VrPanoramaView.Options
            {
                InputType = Intent.GetIntExtra(VrIntentExtras.IMAGE_TYPE, VrPanoramaView.Options.TypeMono)
            };

            if (Intent.HasExtra(VrIntentExtras.IMAGE_ASSET_NAME))
            {
                var andesStream = Assets.Open(Intent.GetStringExtra(VrIntentExtras.IMAGE_ASSET_NAME));
                var andesBitmap = BitmapFactory.DecodeStream(andesStream);
                panoramaView.LoadImageFromBitmap(andesBitmap, options);
            }
            else if (Intent.HasExtra(VrIntentExtras.IMAGE_URL))
            {
                throw new NotImplementedException();
            }
        }
    }
}
