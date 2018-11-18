using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Com.Google.VR.Sdk.Widgets.Video;

namespace GoogleVR.Android.BindingTest
{
    [Activity]
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

            _videoView.SetEventListener(new VideoEventListener(this));

            _seekBar.ProgressChanged += OnProgressChanged;
        }

        protected override void OnResume()
        {
            base.OnResume();

            var options = new VrVideoView.Options
            {
                InputFormat = VrVideoView.Options.FormatDefault,
                InputType = VrVideoView.Options.TypeMono
            };
            _videoView.LoadVideoFromAsset("test_1920_mono.mp4", options);
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
