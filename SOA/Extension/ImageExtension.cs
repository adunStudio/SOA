using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SOA.Extension
{
    public static class ImageExtension
    {
        public static string Save(this Image image)
        {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

            image.Save(filename, ImageFormat.Png);

            return filename;
        }
    }
}
