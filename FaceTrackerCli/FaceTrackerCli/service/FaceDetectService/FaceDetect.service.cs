﻿using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerCli.service.FaceDetectService
{
    public class FaceDetectService : IDisposable
    {

        private const string CASCADE_PATH = "haarcascade_frontalface_default.xml";

        private CascadeClassifier hearCascade;

        public FaceDetectService()
        {
            hearCascade = new CascadeClassifier(CASCADE_PATH);
        }

        public Rect[] DetectFace(Mat img)
        {
            return hearCascade.DetectMultiScale(image: img,
                scaleFactor: 1.1,
                minNeighbors: 3,
                flags: HaarDetectionType.ScaleImage,
                minSize: new Size(100, 100));
        }

        public void Dispose()
        {
            ((IDisposable)hearCascade).Dispose();
        }
    }
}
