using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace SOA.Extension
{
    public static class ImageExtension
    {
        public static string Save(this Image image, string directory = "", string filename = "")
        {
            if(string.IsNullOrEmpty(filename) == true)
            {
                filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            }

            if(string.IsNullOrEmpty(directory) == false)
            {
                filename = Path.Combine(directory, filename);
            }

            image.Save(filename, ImageFormat.Png);

            return filename;
        }
    }
}
