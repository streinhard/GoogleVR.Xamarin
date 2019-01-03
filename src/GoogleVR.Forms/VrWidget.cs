using System;
using Xamarin.Forms;

namespace GoogleVR.Forms
{
    public class VrWidget : View
    {
        public static BindableProperty InfoButtonEnabledProperty =
            BindableProperty.Create(nameof(InfoButtonEnabled), typeof(bool), typeof(VrWidget), true);

        public static BindableProperty TouchTrackingEnabledProperty =
            BindableProperty.Create(nameof(TouchTrackingEnabled), typeof(bool), typeof(VrWidget), true);

        public static BindableProperty StereoModeButtonEnabledProperty =
            BindableProperty.Create(nameof(StereoModeButtonEnabled), typeof(bool), typeof(VrWidget), true);

        public static BindableProperty TransitionViewEnabledProperty =
            BindableProperty.Create(nameof(TransitionViewEnabled), typeof(bool), typeof(VrWidget), true);

        public static BindableProperty FullscreenButtonEnabledProperty =
            BindableProperty.Create(nameof(FullscreenButtonEnabled), typeof(bool), typeof(VrWidget), true);

        public event EventHandler Clicked;
        public event EventHandler<DisplayModeChangedEventArgs> DisplayModeChanged;
        public event EventHandler<LoadSuccessEventArgs> LoadSuccess;
        public event EventHandler<LoadErrorEventArgs> LoadError;

        internal event EventHandler RenderingPaused;
        internal event EventHandler RenderingResumed;

        public bool InfoButtonEnabled
        {
            get => (bool)GetValue(InfoButtonEnabledProperty);
            set => SetValue(InfoButtonEnabledProperty, value);
        }

        public bool TouchTrackingEnabled
        {
            get => (bool)GetValue(TouchTrackingEnabledProperty);
            set => SetValue(TouchTrackingEnabledProperty, value);
        }

        public bool StereoModeButtonEnabled
        {
            get => (bool)GetValue(StereoModeButtonEnabledProperty);
            set => SetValue(StereoModeButtonEnabledProperty, value);
        }

        public bool TransitionViewEnabled
        {
            get => (bool)GetValue(TransitionViewEnabledProperty);
            set => SetValue(TransitionViewEnabledProperty, value);
        }

        public bool FullscreenButtonEnabled
        {
            get => (bool)GetValue(FullscreenButtonEnabledProperty);
            set => SetValue(FullscreenButtonEnabledProperty, value);
        }

        public void PauseRendering()
        {
            RenderingPaused?.Invoke(this, EventArgs.Empty);
        }

        public void ResumeRendering()
        {
            RenderingResumed?.Invoke(this, EventArgs.Empty);
        }

        internal void OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        internal void OnDisplayModeChanged(VrDisplayMode newDisplayMode)
        {
            DisplayModeChanged?.Invoke(this, new DisplayModeChangedEventArgs
            {
                DisplayMode = newDisplayMode
            });
        }

        internal void OnLoadSuccess(long? duration = null)
        {
            LoadSuccess?.Invoke(this, new LoadSuccessEventArgs
            {
                Duration = duration
            });
        }

        internal void OnLoadError(string errorMessage)
        {
            LoadError?.Invoke(this, new LoadErrorEventArgs
            {
                ErrorMessage = errorMessage
            });
        }
    }
}
