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

        public event EventHandler Clicked;
        public event EventHandler<DisplayModeChangedEventArgs> DisplayModeChanged;
        public event EventHandler<LoadSuccessEventArgs> LoadSuccess;
        public event EventHandler<LoadErrorEventArgs> LoadError;

        public event EventHandler _RenderingPaused;
        public event EventHandler _RenderingResumed;

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

        public void PauseRendering()
        {
            _RenderingPaused?.Invoke(this, EventArgs.Empty);
        }

        public void ResumeRendering()
        {
            _RenderingResumed?.Invoke(this, EventArgs.Empty);
        }

        public void _OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void _OnDisplayModeChanged(VrDisplayMode newDisplayMode)
        {
            DisplayModeChanged?.Invoke(this, new DisplayModeChangedEventArgs
            {
                DisplayMode = newDisplayMode
            });
        }

        public void _OnLoadSuccess(long? duration = null)
        {
            LoadSuccess?.Invoke(this, new LoadSuccessEventArgs
            {
                Duration = duration
            });
        }

        public void _OnLoadError(string errorMessage)
        {
            LoadError?.Invoke(this, new LoadErrorEventArgs
            {
                ErrorMessage = errorMessage
            });
        }
    }
}
