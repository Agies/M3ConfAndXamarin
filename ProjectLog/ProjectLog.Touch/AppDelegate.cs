using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ProjectLog.Touch
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
        MyViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);
			viewController = new MyViewController();
			var navigationController = new UINavigationController (viewController);
			navigationController.NavigationBar.Translucent = false;
            

			window.RootViewController = navigationController;

            window.MakeKeyAndVisible();

            return true;
        }
    }
}

