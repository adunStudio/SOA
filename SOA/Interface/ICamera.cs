using System;
using System.Drawing;

namespace SOA.Interface
{
    public interface ICamera : IInit
    {
        Bitmap Capture();

        Bitmap Capture(int sx, int sy, int dx, int dy);
    }
}
