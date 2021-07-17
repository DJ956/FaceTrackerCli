using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerCli.service.DrawingService
{
    public class DrawingService
    {

        private readonly int WIDTH;
        private readonly int HEIGHT;

        private readonly int cy;
        private readonly int cx;

        private readonly int fx;
        private readonly int fy;

        private readonly Rect CENTER_RECT;

        public DrawingService(int width, int height)
        {
            WIDTH = width;
            HEIGHT = height;

            cx = WIDTH / 2;
            cy = HEIGHT / 2;


            fx = cx / 2;
            fy = cy / 2;
            CENTER_RECT = new Rect(fx, fy, cx, cy);
        }


        /// <summary>
        /// 中央に四角を描画する
        /// </summary>
        /// <param name="img"></param>
        public void DrawCenterRectangle(Mat img)
        {
            Cv2.Rectangle(img, new Rect(cx, cy, 5, 5), Scalar.Red, 1);
            Cv2.Rectangle(img, CENTER_RECT, Scalar.Blue, 1);
        }

        /// <summary>
        /// 四角を描画する
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rect"></param>
        /// <param name="scalar"></param>
        public void DrawRectangle(Mat img, Rect rect, Scalar scalar)
        {
            Cv2.Rectangle(img, rect, scalar, 1);
        }

        /// <summary>
        /// 文字を描画
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        public void PutText(Mat img, string text)
        {
            Cv2.PutText(img, text, new Point(fx, fy), HersheyFonts.HersheyPlain, 1, Scalar.Red);
        }

    }
}
