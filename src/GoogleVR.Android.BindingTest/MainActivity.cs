using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Content;
using Com.Google.VR.Sdk.Widgets.Video;
using Com.Google.VR.Sdk.Widgets.Pano;

namespace GoogleVR.Android.BindingTest
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var videoStereo = FindViewById<Button>(Resource.Id.video_stereo);
            var videoMono = FindViewById<Button>(Resource.Id.video_mono);
            var videoUrl = FindViewById<Button>(Resource.Id.video_url);
            var panoramaStereo = FindViewById<Button>(Resource.Id.panorama_stereo);
            var panoramaMono = FindViewById<Button>(Resource.Id.panorama_mono);
            var panoramaUrl = FindViewById<Button>(Resource.Id.panorama_url);

            videoStereo.Click += ShowVideoStereo;
            videoMono.Click += ShowVideoMono;
            videoUrl.Click += ShowVideoUrl;
            panoramaStereo.Click += ShowPanoStereo;
            panoramaMono.Click += ShowPanoMono;
            panoramaUrl.Click += ShowPanoUrl;
        }

        private void ShowVideoStereo(object sender, System.EventArgs e)
        {
            var videoIntent = new Intent(this, typeof(VideoActivity));
            videoIntent.PutExtra(VrIntentExtras.VIDEO_ASSET_NAME, "test_1080_stereo.mp4");
            videoIntent.PutExtra(VrIntentExtras.VIDEO_TYPE, VrVideoView.Options.TypeStereoOverUnder);
            StartActivity(videoIntent);
        }

        private void ShowVideoMono(object sender, System.EventArgs e)
        {
            var videoIntent = new Intent(this, typeof(VideoActivity));
            videoIntent.PutExtra(VrIntentExtras.VIDEO_ASSET_NAME, "test_1920_mono.mp4");
            videoIntent.PutExtra(VrIntentExtras.VIDEO_TYPE, VrVideoView.Options.TypeMono);
            StartActivity(videoIntent);
        }

        private void ShowVideoUrl(object sender, System.EventArgs e)
        {
            var videoIntent = new Intent(this, typeof(VideoActivity));
            videoIntent.PutExtra(VrIntentExtras.VIDEO_URL, "https://infosky.ch/media/bigbuckbunny.mp4");
            videoIntent.PutExtra(VrIntentExtras.VIDEO_FORMAT, VrVideoView.Options.FormatDefault);
            videoIntent.PutExtra(VrIntentExtras.VIDEO_TYPE, VrVideoView.Options.TypeStereoOverUnder);
            StartActivity(videoIntent);
        }

        private void ShowPanoStereo(object sender, System.EventArgs e)
        {
            var panoIntent = new Intent(this, typeof(PanoActivity));
            panoIntent.PutExtra(VrIntentExtras.IMAGE_ASSET_NAME, "test_2k_stereo.jpg");
            panoIntent.PutExtra(VrIntentExtras.IMAGE_TYPE, VrPanoramaView.Options.TypeStereoOverUnder);
            StartActivity(panoIntent);
        }

        private void ShowPanoMono(object sender, System.EventArgs e)
        {
            var panoIntent = new Intent(this, typeof(PanoActivity));
            panoIntent.PutExtra(VrIntentExtras.IMAGE_ASSET_NAME, "test_2k_mono.jpg");
            panoIntent.PutExtra(VrIntentExtras.IMAGE_TYPE, VrPanoramaView.Options.TypeMono);
            StartActivity(panoIntent);
        }

        private void ShowPanoUrl(object sender, System.EventArgs e)
        {
            var panoIntent = new Intent(this, typeof(PanoActivity));
            panoIntent.PutExtra(VrIntentExtras.IMAGE_URL, "https://infosky.ch/media/burg_schellenberg_8192.jpg");
            panoIntent.PutExtra(VrIntentExtras.IMAGE_TYPE, VrPanoramaView.Options.TypeMono);
            StartActivity(panoIntent);
        }
    }
}

