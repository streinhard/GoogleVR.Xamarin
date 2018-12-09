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
    }
}
