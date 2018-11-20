using System;
using System.Drawing;

namespace SOA.Interface
{
    public interface ICamera : IInit
    {
        Bitmap Capture();
    }
}
