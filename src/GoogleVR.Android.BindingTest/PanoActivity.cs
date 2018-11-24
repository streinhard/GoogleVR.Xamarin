using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Com.Google.VR.Sdk.Widgets.Pano;

namespace GoogleVR.Android.BindingTest
{
    [Activity(ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class PanoActivity : AppCompatActivity
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private VrPanoramaView _panoramaView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Pano);

            _panoramaView = FindViewById<VrPanoramaView>(Resource.Id.pano_view);

            LoadPanoramaFromIntent().ConfigureAwait(false);
        }

        private async Task LoadPanoramaFromIntent()
        {
            var options = new VrPanoramaView.Options
            {
                InputType = Intent.GetIntExtra(VrIntentExtras.IMAGE_TYPE, VrPanoramaView.Options.TypeMono)
            };

            if (Intent.HasExtra(VrIntentExtras.IMAGE_ASSET_NAME))
            {
                var imageStream = Assets.Open(Intent.GetStringExtra(VrIntentExtras.IMAGE_ASSET_NAME));
                var imageBitmap = BitmapFactory.DecodeStream(imageStream);
                _panoramaView.LoadImageFromBitmap(imageBitmap, options);
            }
            else if (Intent.HasExtra(VrIntentExtras.IMAGE_URL))
            {
                var imageUrl = Intent.GetStringExtra(VrIntentExtras.IMAGE_URL);
                var imageStream = await _httpClient.GetStreamAsync(imageUrl);
                var imageBitmap = await BitmapFactory.DecodeStreamAsync(imageStream);
                _panoramaView.LoadImageFromBitmap(imageBitmap, options);
            }
        }
    }
}
