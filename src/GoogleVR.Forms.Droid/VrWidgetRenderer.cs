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
        }

        protected void UpdateWidget()
        {
            UpdateInfoButtonEnabled();
            UpdateTouchTrackingEnabled();
            UpdateStereoModeButtonEnabled();
            UpdateTransitionViewEnabled();
        }

        private void UpdateInfoButtonEnabled()
        {
            Control.SetInfoButtonEnabled(Element.InfoButtonEnabled);
        }

        private void UpdateTouchTrackingEnabled()
        {
            Control.SetTouchTrackingEnabled(Element.TouchTrackingEnabled);
        }

        private void UpdateStereoModeButtonEnabled()
        {
            Control.SetStereoModeButtonEnabled(Element.StereoModeButtonEnabled);
        }

        private void UpdateTransitionViewEnabled()
        {
            Control.SetTransitionViewEnabled(Element.TransitionViewEnabled);
        }
    }
}