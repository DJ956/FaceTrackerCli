using OpenCvSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace FaceTrackerCli.service.CameraService
{
    public class CameraService : IDisposable
    {
        public const int WIDTH = 640;
        public const int HEIGHT = 480;


        /// <summary>
        /// Webカメラでキャプチャしたビットマップイメージ
        /// </summary>
        public Bitmap CaptureImg { get; set; }

        /// <summary>
        /// カメラでキャプチャしたイメージ
        /// </summary>
        public Mat frame { get; private set; }

        /// <summary>
        /// Webカメラのインスタンス
        /// </summary>
        private VideoCapture capture;


        /// <summary>
        /// カメラのインデックス
        /// </summary>
        private int cameraIndex;

        private bool disposedValue;

        public CameraService()
        {
            frame = new Mat(HEIGHT, WIDTH, MatType.CV_8UC3);
            CaptureImg = new Bitmap(frame.Cols, frame.Rows, (int)frame.Step(), PixelFormat.Format24bppRgb, frame.Data);
            cameraIndex = 0;
        }

        /// <summary>
        /// カメラを起動します。
        /// </summary>
        public void Open()
        {
            capture = new VideoCapture(cameraIndex);
            if (!capture.IsOpened())
            {
                throw new Exception("使用可能なカメラが見つかりませんでした。");
            }

            capture.FrameWidth = WIDTH;
            capture.FrameHeight = HEIGHT;
        }

        /// <summary>
        /// カメラを切り替える
        /// </summary>
        public void ChangeCameraAndOpen()
        {
            cameraIndex = cameraIndex == 0 ? 1 : 0;
            capture = VideoCapture.FromCamera(cameraIndex);
            if (!capture.IsOpened())
            {
                cameraIndex = 0;
                throw new Exception("使用可能なカメラが見つかりませんでした。");
            }

            capture.FrameWidth = WIDTH;
            capture.FrameHeight = HEIGHT;
        }


        /// <summary>
        /// カメラの映像をBitMapに流し込む
        /// </summary>
        public void Capture()
        {

            if (capture == null || capture.IsDisposed) { return; }
            if (frame == null || frame.IsDisposed) { return; }
            if (!capture.IsOpened()) { return; }


            try
            {
                capture.Grab();
                NativeMethods.videoio_VideoCapture_operatorRightShift_Mat(capture.CvPtr, frame.CvPtr);
            }
            catch (AccessViolationException e)
            {
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                if (capture != null)
                {
                    capture.Dispose();
                    capture = null;
                }
                if (frame != null)
                {
                    frame.Dispose();
                    frame = null;
                }
                if (CaptureImg != null)
                {
                    CaptureImg.Dispose();
                    CaptureImg = null;
                }
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~CameraService()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
