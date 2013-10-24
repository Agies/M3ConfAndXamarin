using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Media;

namespace ProjectLog.Touch
{
	public partial class GenericView : UIViewController
	{
		public GenericView () : base ("GenericView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
            var picker = new MediaPicker();
            picker.PickPhotoAsync().ContinueWith(t =>
            {
                MediaFile file = t.Result;
                Console.WriteLine(file.Path);
            }, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}

