using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageRecognition
{
    public class EmguCvProcessor : IEmguCvProcessor
    {
        private Bitmap MakeScreenshot()
        {
            Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics gfxScreenshot = Graphics.FromImage(screenshot);
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            gfxScreenshot.Dispose();

            return screenshot;
        }

        public Coordinates GetCoordinates(String imgPath, double threshold = 0.8)
        {
            Bitmap bitmap = MakeScreenshot();
            Coordinates coordinates = new Coordinates();
            Image<Bgr, byte> source = bitmap.ToImage<Bgr, byte>();  // Image B
            Image<Bgr, byte> template = new Image<Bgr, byte>(imgPath); // Image A

            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > threshold)
                {
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    coordinates.X = match.X;
                    coordinates.Y = match.Y;
                    coordinates.Name = imgPath;
                }
            }

            return coordinates;
        }
    }
}
