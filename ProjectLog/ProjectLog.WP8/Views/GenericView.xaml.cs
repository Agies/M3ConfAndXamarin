using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace ProjectLog.WP8.Views
{
    public partial class GenericView : PhoneApplicationPage
    {
        private CaptureSource _captureSource;
        private FileSink _fileSink;
        private VideoCaptureDevice _videoCaptureDevice;
        private VideoBrush _videoBrush;
        private ImageBrush _imageBrush;


        public GenericView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeVideoRecorder();
        }

        private void InitializeVideoRecorder()
        {
            if (_captureSource == null)
            {
                _captureSource = new CaptureSource();
                _fileSink = new FileSink();

                _videoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
                _captureSource.CaptureFailed += OnCaptureSourceOnCaptureFailed;
                _captureSource.CaptureImageCompleted += CaptureSourceOnCaptureImageCompleted;

                if (_videoCaptureDevice != null)
                {
                    _videoBrush = new VideoBrush();
                    _videoBrush.SetSource(_captureSource);

                    ViewFinderRectangle.Fill = _videoBrush;
                    _captureSource.Start();
                }
            }
        }

        private void CaptureSourceOnCaptureImageCompleted(object sender, CaptureImageCompletedEventArgs args)
        {
        }

        private void OnCaptureSourceOnCaptureFailed(object sender, ExceptionRoutedEventArgs args)
        {
            Dispatcher.BeginInvoke(() => Console.WriteLine("ERROR: " + args.ErrorException.Message));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            DisposeVideoRecorder();

            base.OnNavigatingFrom(e);
        }

        private void DisposeVideoRecorder()
        {
            if (_captureSource != null)
            {
                if (_captureSource.VideoCaptureDevice != null
                    && _captureSource.State == CaptureState.Started)
                    _captureSource.Stop();
                _captureSource.CaptureFailed -= OnCaptureSourceOnCaptureFailed;
                _captureSource = null;
                _videoCaptureDevice = null;
                _fileSink = null;
                _videoBrush = null;
            }
        }

        private void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            _captureSource.CaptureImageAsync();
        }
    }
}