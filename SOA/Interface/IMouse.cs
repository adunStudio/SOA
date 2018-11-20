using System;

namespace SOA.Interface
{
    public delegate void MouseEvent(int x, int y);

    public interface IMouse : IInit
    {
        event MouseEvent OnMouseMove;
        event MouseEvent OnMouseClick;
        event MouseEvent OnMouseDoubleClick;
        event MouseEvent OnMouseDown;
        event MouseEvent OnMouseUp;
        event MouseEvent OnMouseWheel;
        event MouseEvent OnMouseDragStart;
        event MouseEvent OnMouseDragEnd;
    }
}
