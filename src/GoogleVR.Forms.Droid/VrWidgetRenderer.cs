using System;
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
                e.OldElement._RenderingPaused -= OnRenderingPaused;
                e.OldElement._RenderingResumed -= OnRenderingResumed;
            }

            if (e.NewElement != null)
            {
                e.NewElement._RenderingPaused += OnRenderingPaused;
                e.NewElement._RenderingResumed += OnRenderingResumed;
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
