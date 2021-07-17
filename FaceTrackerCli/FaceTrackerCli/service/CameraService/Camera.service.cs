using OpenCvSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerCli.service.CameraService
{
    public class CameraService
    {
        public const int WIDTH = 640;
        public const int HEIGHT = 480;


        public Bitmap CaptureImg { get; set; }

        private VideoCapture capture;

        public Mat frame { get; private set; }

        public CameraService()
        {
            frame = new Mat(HEIGHT, WIDTH, MatType.CV_8UC3);
            CaptureImg = new Bitmap(frame.Cols, frame.Rows, (int)frame.Step(), PixelFormat.Format24bppRgb, frame.Data);
        }

        public void Open()
        {
            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                throw new Exception("使用可能なカメラが見つかりませんでした。");
            }

            capture.FrameWidth = WIDTH;
            capture.FrameHeight = HEIGHT;
        }

        public void Close()
        {
            if (capture != null)
            {
                capture.Dispose();
            }

            if (frame != null)
            {
                frame.Dispose();
            }
        }

        /// <summary>
        /// カメラの映像をBitMapに流し込む
        /// </summary>
        public void Capture()
        {
            if (!capture.IsOpened()) { return; }

            capture.Grab();
            NativeMethods.videoio_VideoCapture_operatorRightShift_Mat(capture.CvPtr, frame.CvPtr);
        }
    }
}
