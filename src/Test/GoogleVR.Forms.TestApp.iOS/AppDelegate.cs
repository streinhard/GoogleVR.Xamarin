using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace GoogleVR.Forms.TestApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            var pleaseDontRemoveThis = new VrPanoramaRenderer();

            return base.FinishedLaunching(app, options);
        }
    }
}
