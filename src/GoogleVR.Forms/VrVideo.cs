using System;
using Xamarin.Forms;

namespace GoogleVR.Forms
{
    public class VrVideo : VrWidget
    {
        public static BindableProperty VideoUrlProperty =
            BindableProperty.Create(nameof(VideoUrl), typeof(string), typeof(VrPanorama));

        public static BindableProperty SourceTypeProperty =
            BindableProperty.Create(nameof(SourceType), typeof(VrSourceType), typeof(VrPanorama), VrSourceType.Mono);
         
        public string VideoUrl
        {
            get => (string)GetValue(VideoUrlProperty);
            set => SetValue(VideoUrlProperty, value);
        }

        public VrSourceType SourceType
        {
            get => (VrSourceType)GetValue(SourceTypeProperty);
            set => SetValue(SourceTypeProperty, value);
        }
    }
}
