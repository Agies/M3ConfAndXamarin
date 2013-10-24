using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Xamarin.Media;
using Camera = Android.Hardware.Camera;
using Console = System.Console;

namespace ProjectLog.Droid.Views
{
    [Activity(Label = "My Activity")]
    public class GenericView : Activity, TextureView.ISurfaceTextureListener
    {
        Camera _camera;
        TextureView _textureView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            try
            {
                SetContentView(Resource.Layout.Generic);
                var texture = FindViewById<TextureView>(Resource.Id.textureView1);
                texture.SurfaceTextureListener = this;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            try
            {
                // User canceled
                if (resultCode == Result.Canceled)
                    return;

                data.GetMediaFileExtraAsync(this).ContinueWith(t =>
                    {
                        Console.WriteLine(t.Result.Path);
                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void OnSurfaceTextureAvailable(Android.Graphics.SurfaceTexture surface, int w, int h)
        {
            try
            {
                _camera = Camera.Open();

                _textureView.LayoutParameters =
                    new FrameLayout.LayoutParams(w, h);

                try
                {
                    _camera.SetPreviewTexture(surface);
                    _camera.StartPreview();

                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            try
            {
                _camera.StopPreview();
                _camera.Release();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
        }
    }
}