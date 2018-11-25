using Android.App;
using Android.Content.PM;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Com.Google.VR.Sdk.Widgets.Video;

namespace GoogleVR.Android.BindingTest
{
    [Activity(ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class VideoActivity : AppCompatActivity
    {
        private VrVideoView _videoView;
        private SeekBar _seekBar;

        private bool _isPlaying;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Video);

            _videoView = FindViewById<VrVideoView>(Resource.Id.video_view);
            _seekBar = FindViewById<SeekBar>(Resource.Id.seek_bar);

            _videoView.SetTransitionViewEnabled(false);
            _videoView.SetEventListener(new VideoEventListener(this));

            _seekBar.ProgressChanged += OnProgressChanged;

            LoadVideoFromIntent();
        }

        protected override void OnResume()
        {
            base.OnResume();

            _videoView.PlayVideo();
        }

        protected override void OnPause()
        {
            base.OnPause();

            _videoView.PauseVideo();
        }

        private void LoadVideoFromIntent()
        {
            var options = new VrVideoView.Options
            {
                InputFormat = Intent.GetIntExtra(VrIntentExtras.VIDEO_FORMAT, VrVideoView.Options.FormatDefault),
                InputType = Intent.GetIntExtra(VrIntentExtras.VIDEO_TYPE, VrVideoView.Options.TypeMono)
            };

            if (Intent.HasExtra(VrIntentExtras.VIDEO_ASSET_NAME))
            {
                var asset_name = Intent.GetStringExtra(VrIntentExtras.VIDEO_ASSET_NAME);
                _videoView.LoadVideoFromAsset(asset_name, options);
            }
            else if (Intent.HasExtra(VrIntentExtras.VIDEO_URL))
            {
                var video_uri = Uri.Parse(Intent.GetStringExtra(VrIntentExtras.VIDEO_URL));
                _videoView.LoadVideo(video_uri, options);
            }
        }

        private void OnProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (e.FromUser)
            {
                _videoView.SeekTo(e.Progress);
            }
        }

        class VideoEventListener : VrVideoEventListener
        {
            private readonly VideoActivity _activity;

            public VideoEventListener(VideoActivity _activity)
            {
                this._activity = _activity;
            }

            public override void OnLoadSuccess()
            {
                _activity._seekBar.Max = (int)_activity._videoView.Duration;
                _activity._isPlaying = true;
            }

            public override void OnLoadError(string errorMessage)
            {
                Toast.MakeText(_activity, errorMessage, ToastLength.Long).Show();
            }

            public override void OnClick()
            {
                if (_activity._isPlaying)
                {
                    _activity._videoView.PauseVideo();
                }
                else
                {
                    _activity._videoView.PlayVideo();
                }

                _activity._isPlaying = !_activity._isPlaying;
            }

            public override void OnNewFrame()
            {
                _activity._seekBar.Progress = (int)_activity._videoView.CurrentPosition;
            }

            public override void OnCompletion()
            {
                _activity._videoView.SeekTo(0);
            }
        }
    }
}
