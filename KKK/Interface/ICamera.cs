using System;
using System.Drawing;

namespace KKK.Interface
{
    public interface ICamera : IInit
    {
        Bitmap Capture();
    }
}
