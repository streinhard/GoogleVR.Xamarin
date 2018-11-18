using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace GoogleVR.Android.BindingTest
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var videoButton = FindViewById<Button>(Resource.Id.video_button);
            var panoramaButton = FindViewById<Button>(Resource.Id.panorama_button);

            videoButton.Click += OnVideoButtonClicked;
            panoramaButton.Click += OnPanoramaButtonClicked;
        }

        private void OnVideoButtonClicked(object sender, System.EventArgs e)
        {
            StartActivity(typeof(VideoActivity));
        }

        private void OnPanoramaButtonClicked(object sender, System.EventArgs e)
        {
            StartActivity(typeof(PanoActivity));
        }
    }
}

