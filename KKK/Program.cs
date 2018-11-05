using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace KKK
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            Application.Run(app);

            using (Image img = ScreenCapture.Capture())
            {
                img.Save(Path.Combine(Environment.CurrentDirectory, "screenShoot.jpg"), ImageFormat.Jpeg);
            }  
        }
    }
}
