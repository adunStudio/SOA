using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SOA.Interface
{
    public interface IGui
    {
        void SetCursor(Cursor cursor);

        void SetOpacity(float opacity);

        void SetBackgroundColor(Color color);

        void SetTransparency(bool isTransparency);

        Button AddButton(string text, int x, int y, int width, int height);
    }
}
