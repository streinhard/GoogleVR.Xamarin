using System;
using System.Runtime.InteropServices;

namespace GoogleVR.iOS
{
    public enum GVRWidgetDisplayMode : ulong
    {
        Embedded = 1,
        Fullscreen,
        FullscreenVR
    }

    public enum GVRPanoramaImageType : ulong
    {
        Mono = 1,
        StereoOverUnder
    }

    public enum GVRVideoType : ulong
    {
        Mono = 1,
        StereoOverUnder,
        SphericalV2
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GVRHeadRotation
    {
        public nfloat yaw;
        public nfloat pitch;
    }
}
