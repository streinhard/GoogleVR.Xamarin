﻿using System;
using System.ComponentModel;
using GoogleVR.iOS;
using Xamarin.Forms.Platform.iOS;

namespace GoogleVR.Forms
{
    public abstract class VrWidgetRender<TView, TNativeView> : ViewRenderer<TView, TNativeView> where TView : VrWidget where TNativeView : WidgetView
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TView> e)
        {
            base.OnElementChanged(e);

            // There are no PauseRendering() and ResumeRendering() methods on iOS, so no need to subscribe to those events
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
            if (Control == null) return;
            Control.EnableInfoButton = Element.InfoButtonEnabled;
        }

        private void UpdateTouchTrackingEnabled()
        {
            if (Control == null) return;
            Control.EnableTouchTracking = Element.TouchTrackingEnabled;
        }

        private void UpdateStereoModeButtonEnabled()
        {
            if (Control == null) return;
            Control.EnableCardboardButton = Element.StereoModeButtonEnabled;
        }

        private void UpdateTransitionViewEnabled()
        {
            if (Control == null) return;
            Control.HidesTransitionView = !Element.TransitionViewEnabled;
        }

        private void UpdateFullscreenButtonEnabled()
        {
            if (Control == null) return;
            Control.EnableFullscreenButton = Element.FullscreenButtonEnabled;
        }
    }
}
