using System;
using Xamarin.Forms;

namespace GoogleVR.Forms
{
    public class VrPanorama : VrWidget
    {
        public static BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(VrPanorama));

        public static BindableProperty SourceTypeProperty =
            BindableProperty.Create(nameof(SourceType), typeof(VrSourceType), typeof(VrPanorama), VrSourceType.Mono);

        public event EventHandler Clicked;
        public event EventHandler<DisplayModeChangedEventArgs> DisplayModeChanged;
        public event EventHandler LoadSuccess;
        public event EventHandler<LoadErrorEventArgs> LoadError;

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public VrSourceType SourceType
        {
            get => (VrSourceType)GetValue(SourceTypeProperty);
            set => SetValue(SourceTypeProperty, value);
        }

        public void OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void OnDisplayModeChanged(VrDisplayMode newDisplayMode)
        {
            DisplayModeChanged?.Invoke(this, new DisplayModeChangedEventArgs
            {
                DisplayMode = newDisplayMode
            });
        }

        public void OnLoadSuccess()
        {
            LoadSuccess?.Invoke(this, EventArgs.Empty);
        }

        public void OnLoadError(string errorMessage)
        {
            LoadError?.Invoke(this, new LoadErrorEventArgs
            {
                ErrorMessage = errorMessage
            });
        }
    }
}
