using System;

namespace SOA.Interface
{
    public delegate void MouseEvent(int x, int y);

    public interface IMouse : IInit
    {
        event MouseEvent OnMove;
        event MouseEvent OnClick;
        event MouseEvent OnDoubleClick;
        event MouseEvent OnDown;
        event MouseEvent OnUp;
        event MouseEvent OnWheel;
        event MouseEvent OnDragStart;
        event MouseEvent OnDragEnd;
    }
}
