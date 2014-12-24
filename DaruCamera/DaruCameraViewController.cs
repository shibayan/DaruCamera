using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using UIKit;
using AVFoundation;

namespace DaruCamera
{
    public partial class DaruCameraViewController : UIViewController
    {
        public DaruCameraViewController(IntPtr handle)
            : base(handle)
        {
        }
            
        private AVCaptureDeviceInput videoInput;
        private AVCaptureStillImageOutput stillImageOutput;
        private AVCaptureSession session;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            session = new AVCaptureSession();

            session.SessionPreset = AVCaptureSession.Preset1280x720;

            var camera = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);

            if (camera == null)
            {
                return;
            }

            NSError error;

            videoInput = new AVCaptureDeviceInput(camera, out error);
            session.AddInput(videoInput);

            stillImageOutput = new AVCaptureStillImageOutput();
            session.AddOutput(stillImageOutput);

            var captureVideoPreviewLayer = new AVCaptureVideoPreviewLayer(session);
            captureVideoPreviewLayer.Frame = previewView.Bounds;
            captureVideoPreviewLayer.Orientation = AVCaptureVideoOrientation.LandscapeRight;
            captureVideoPreviewLayer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;

            var layer = previewView.Layer;
            layer.MasksToBounds = true;
            layer.AddSublayer(captureVideoPreviewLayer);

            session.StartRunning();
        }

        partial void UIButton74_TouchUpInside(UIButton sender)
        {
            var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);

            if (videoConnection == null)
            {
                return;
            }

            videoConnection.VideoOrientation = AVCaptureVideoOrientation.LandscapeRight;

            stillImageOutput.CaptureStillImageAsynchronously(videoConnection, (imageDataSampleBuffer, error) => 
                {
                    if (imageDataSampleBuffer == null)
                    {
                        return;
                    }

                    var imageData = AVCaptureStillImageOutput.JpegStillToNSData(imageDataSampleBuffer);

                    var image = new UIImage(imageData);
                    var overlay = UIImage.FromBundle("daruyanagi");

                    UIGraphics.BeginImageContext(image.Size);

                    image.Draw(new CGPoint(0, 0));

                    var width = 571;//overlay.Size.Width * 1.5f;
                    var height = 700;//overlay.Size.Height * 1.5f;

                    var x = (image.Size.Width - width);
                    var y = (image.Size.Height - height);

                    overlay.Draw(new CGRect(x, y, width, height));

                    var captured = UIGraphics.GetImageFromCurrentImageContext();

                    UIGraphics.EndImageContext();

                    captured.SaveToPhotosAlbum((img, err) =>
                        {
                        });
                });
        }
    }
}

