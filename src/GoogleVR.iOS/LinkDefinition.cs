using System;
using ObjCRuntime;

[assembly: LinkWith(
    "libGVRSDK.a",
    LinkTarget.ArmV7 | LinkTarget.ArmV6 | LinkTarget.Arm64 | LinkTarget.Simulator | LinkTarget.Simulator64,
    LinkerFlags = "-ObjC -lc++",
    Frameworks = "AVFoundation AudioToolbox CoreImage CoreGraphics CoreMedia CoreMotion CoreText CoreVideo GLKit MediaPlayer OpenGLES QuartzCore Security",
    SmartLink = true,
    ForceLoad = false,
    IsCxx = true
)]