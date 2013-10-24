using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Media;

namespace ProjectLog.Droid.Views
{
    [Activity(Label = "My Activity")]
    public class GenericView : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var picker = new MediaPicker(this);
            if (!picker.IsCameraAvailable)
                Console.WriteLine("No camera!");
            else
            {
                var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = "test.jpg",
                    Directory = "MediaPickerSample"
                });
                StartActivityForResult(intent, 1);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // User canceled
            if (resultCode == Result.Canceled)
                return;

            data.GetMediaFileExtraAsync(this).ContinueWith(t =>
            {
                Console.WriteLine(t.Result.Path);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}