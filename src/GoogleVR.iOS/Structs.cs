using System;
using System.Runtime.InteropServices;

namespace GoogleVR.iOS
{
    public enum WidgetDisplayMode : ulong
    {
        Embedded = 1,
        Fullscreen,
        FullscreenVR
    }

    public enum PanoramaImageType : ulong
    {
        Mono = 1,
        StereoOverUnder
    }

    public enum VideoType : ulong
    {
        Mono = 1,
        StereoOverUnder,
        SphericalV2
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HeadRotation
    {
        public nfloat yaw;
        public nfloat pitch;
    }
}
