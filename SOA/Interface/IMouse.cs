using System;
using System.Windows.Forms;

namespace SOA.Interface
{
    public delegate void MouseEvent(int x, int y, MouseButtons button, int delta = 0);

    public interface IMouse : IInit, IDelay<IMouse>
    {
        event MouseEvent OnMouseDown;
        event MouseEvent OnMouseUp;
        event MouseEvent OnMouseClick;
        event MouseEvent OnMouseDoubleClick;
        event MouseEvent OnMouseMove;
        event MouseEvent OnMouseWheel;
        event MouseEvent OnMouseDragStart;
        event MouseEvent OnMouseDragEnd;

        IMouse MoveTo(int x, int y);

        IMouse MoveBy(int x, int y);

        IMouse Click(MouseButtons button);

        IMouse DoubleClick(MouseButtons button);

        IMouse Wheel(int delta);

        IMouse Drag(int x, int y, int dx, int dy);

        bool IsDragMode();
    }
}
