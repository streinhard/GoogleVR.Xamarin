﻿using System;
using System.ComponentModel;
using GoogleVR.Widgets.Common;
using Xamarin.Forms.Platform.Android;

namespace GoogleVR.Forms
{
    public abstract class VrWidgetRender<TView, TNativeView> : ViewRenderer<TView, TNativeView> where TView : VrWidget where TNativeView : VrWidgetView
    {
        protected VrWidgetRender(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.RenderingPaused -= OnRenderingPaused;
                e.OldElement.RenderingResumed -= OnRenderingResumed;
            }

            if (e.NewElement != null)
            {
                e.NewElement.RenderingPaused += OnRenderingPaused;
                e.NewElement.RenderingResumed += OnRenderingResumed;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VrWidget.InfoButtonEnabledProperty.PropertyName)
                UpdateInfoButtonEnabled();
            else if (e.PropertyName == VrWidget.TouchTrackingEnabledProperty.PropertyName)
                UpdateTouchTrackingEnabled();
            else if (e.PropertyName == VrWidget.StereoModeButtonEnabledProperty.PropertyName)
                UpdateStereoModeButtonEnabled();
            else if (e.PropertyName == VrWidget.TransitionViewEnabledProperty.PropertyName)
                UpdateTransitionViewEnabled();
            else if (e.PropertyName == VrWidget.FullscreenButtonEnabledProperty.PropertyName)
                UpdateFullscreenButtonEnabled();

        }

        protected void UpdateWidget()
        {
            UpdateInfoButtonEnabled();
            UpdateTouchTrackingEnabled();
            UpdateStereoModeButtonEnabled();
            UpdateTransitionViewEnabled();
            UpdateFullscreenButtonEnabled();
        }

        private void UpdateInfoButtonEnabled()
        {
            Control?.SetInfoButtonEnabled(Element.InfoButtonEnabled);
        }

        private void UpdateTouchTrackingEnabled()
        {
            Control?.SetTouchTrackingEnabled(Element.TouchTrackingEnabled);
        }

        private void UpdateStereoModeButtonEnabled()
        {
            Control?.SetStereoModeButtonEnabled(Element.StereoModeButtonEnabled);
        }

        private void UpdateTransitionViewEnabled()
        {
            Control?.SetTransitionViewEnabled(Element.TransitionViewEnabled);
        }

        private void UpdateFullscreenButtonEnabled()
        {
            Control?.SetFullscreenButtonEnabled(Element.FullscreenButtonEnabled);
        }

        private void OnRenderingPaused(object sender, EventArgs e)
        {
            Control?.PauseRendering();
        }

        private void OnRenderingResumed(object sender, EventArgs e)
        {
            Control?.ResumeRendering();
        }
    }
}
