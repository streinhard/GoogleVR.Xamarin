using System;
namespace GoogleVR.Forms
{
    public class VrImageLoader
    {
        private static IVrImageLoader _instance;

        public static IVrImageLoader Instance
        {
            get => _instance ?? (_instance = new DefaultImageLoader());
            set => _instance = value;
        }
    }
}
