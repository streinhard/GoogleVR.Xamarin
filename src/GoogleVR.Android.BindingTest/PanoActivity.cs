
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
        }

        protected override void OnResume()
        {
            base.OnResume();

            var andesStream = Assets.Open("test_2k_stereo.jpg");
            var andesBitmap = BitmapFactory.DecodeStream(andesStream);

            var options = new VrPanoramaView.Options
            {
                InputType = VrPanoramaView.Options.TypeStereoOverUnder
            };

            panoramaView.LoadImageFromBitmap(andesBitmap, options);
        }
    }
}
