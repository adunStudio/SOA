using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace KKK
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Image img = ScreenCapture.Capture())
            {
                img.Save(Path.Combine(Environment.CurrentDirectory, "screenShoot.jpg"), ImageFormat.Jpeg);
            }
        }
    }
}
