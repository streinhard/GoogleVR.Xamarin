﻿using System;
using UIKit;
using Foundation;
using ObjCRuntime;

namespace GoogleVR.iOS
{
    // @interface GVRWidgetView : UIView
    [BaseType(typeof(UIView))]
    interface GVRWidgetView
    {
        [Wrap("WeakDelegate")]
        GVRWidgetViewDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<GVRWidgetViewDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (nonatomic) BOOL enableFullscreenButton;
        [Export("enableFullscreenButton")]
        bool EnableFullscreenButton { get; set; }

        // @property (nonatomic) BOOL enableCardboardButton;
        [Export("enableCardboardButton")]
        bool EnableCardboardButton { get; set; }

        // @property (nonatomic) BOOL enableInfoButton;
        [Export("enableInfoButton")]
        bool EnableInfoButton { get; set; }

        // @property (nonatomic) BOOL hidesTransitionView;
        [Export("hidesTransitionView")]
        bool HidesTransitionView { get; set; }

        // @property (nonatomic) BOOL enableTouchTracking;
        [Export("enableTouchTracking")]
        bool EnableTouchTracking { get; set; }

        // @property (readonly, nonatomic) GVRHeadRotation headRotation;
        [Export("headRotation")]
        GVRHeadRotation HeadRotation { get; }

        // @property (nonatomic) GVRWidgetDisplayMode displayMode;
        [Export("displayMode", ArgumentSemantic.Assign)]
        GVRWidgetDisplayMode DisplayMode { get; set; }

        // +(void)setViewerParamsFromUrl:(NSURL *)url withCompletion:(void (^)(BOOL, NSError *))completion;
        [Static]
        [Export("setViewerParamsFromUrl:withCompletion:")]
        void SetViewerParamsFromUrl(NSUrl url, Action<bool, NSError> completion);
    }

    // @protocol GVRWidgetViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface GVRWidgetViewDelegate
    {
        // @optional -(void)widgetViewDidTap:(GVRWidgetView *)widgetView;
        [Export("widgetViewDidTap:")]
        void DidTap(GVRWidgetView widgetView);

        // @optional -(void)widgetView:(GVRWidgetView *)widgetView didChangeDisplayMode:(GVRWidgetDisplayMode)displayMode;
        [Export("widgetView:didChangeDisplayMode:")]
        void DidChangeDisplayMode(GVRWidgetView widgetView, GVRWidgetDisplayMode displayMode);

        // @optional -(void)widgetView:(GVRWidgetView *)widgetView didLoadContent:(id)content;
        [Export("widgetView:didLoadContent:")]
        void DidLoadContent(GVRWidgetView widgetView, NSObject content);

        // @optional -(void)widgetView:(GVRWidgetView *)widgetView didFailToLoadContent:(id)content withErrorMessage:(NSString *)errorMessage;
        [Export("widgetView:didFailToLoadContent:withErrorMessage:")]
        void DidFailToLoadContent(GVRWidgetView widgetView, NSObject content, string errorMessage);
    }

    // @interface GVRPanoramaView : GVRWidgetView
    [BaseType(typeof(GVRWidgetView))]
    interface GVRPanoramaView
    {
        // -(void)loadImage:(UIImage *)image;
        [Export("loadImage:")]
        void LoadImage(UIImage image);

        // -(void)loadImage:(UIImage *)image ofType:(GVRPanoramaImageType)imageType;
        [Export("loadImage:ofType:")]
        void LoadImage(UIImage image, GVRPanoramaImageType imageType);
    }

    // @interface GVRVideoView : GVRWidgetView
    [BaseType(typeof(GVRWidgetView))]
    interface GVRVideoView
    {
        // -(void)loadFromUrl:(NSURL *)videoUrl;
        [Export("loadFromUrl:")]
        void LoadFromUrl(NSUrl videoUrl);

        // -(void)loadFromUrl:(NSURL *)videoUrl ofType:(GVRVideoType)videoType;
        [Export("loadFromUrl:ofType:")]
        void LoadFromUrl(NSUrl videoUrl, GVRVideoType videoType);

        // -(void)pause;
        [Export("pause")]
        void Pause();

        // -(void)play;
        [Export("play")]
        void Play();

        // -(void)stop;
        [Export("stop")]
        void Stop();

        // -(NSTimeInterval)duration;
        [Export("duration")]
        double Duration { get; }

        // -(NSTimeInterval)playableDuration;
        [Export("playableDuration")]
        double PlayableDuration { get; }

        // -(void)seekTo:(NSTimeInterval)position;
        [Export("seekTo:")]
        void SeekTo(double position);

        // @property (nonatomic) float volume;
        [Export("volume")]
        float Volume { get; set; }
    }

    // @protocol GVRVideoViewDelegate <GVRWidgetViewDelegate>
    [Protocol, Model]
    [BaseType(typeof(GVRWidgetViewDelegate))]
    interface GVRVideoViewDelegate
    {
        // @required -(void)videoView:(GVRVideoView *)videoView didUpdatePosition:(NSTimeInterval)position;
        [Abstract]
        [Export("videoView:didUpdatePosition:")]
        void DidUpdatePosition(GVRVideoView videoView, double position);
    }
}
