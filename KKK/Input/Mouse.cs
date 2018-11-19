using System;
using System.Runtime.InteropServices;

using KKK.Interface;
using KKK.Helper;
using static PInvoke.User32;

namespace KKK.Input
{
    public class Mouse : IMouse
    {
        public event MouseEvent OnMouseMove = null;
        public event MouseEvent OnMouseClick = null;
        public event MouseEvent OnMouseDoubleClick = null;
        public event MouseEvent OnMouseDown = null;
        public event MouseEvent OnMouseUp = null;
        public event MouseEvent OnMouseWheel = null;
        public event MouseEvent OnMouseDragStart = null;
        public event MouseEvent OnMouseDragEnd = null;

        public void Init()
        {
            HookHelper.instance.HookGlobalMouse(HookMouseCallback);
        }

        private void HookMouseCallback(HookData hookData)
        {
            WindowMessage state = (WindowMessage)hookData.wParam;

            MOUSEINPUT mouseStruct = Marshal.PtrToStructure<MOUSEINPUT>(hookData.lParam);
            int x = mouseStruct.dx;
            int y = mouseStruct.dy;

            switch (state)
            {
                case WindowMessage.WM_MOUSEMOVE:
                    OnMouseMove?.Invoke(x, y); break;
                case WindowMessage.WM_LBUTTONDOWN:
                case WindowMessage.WM_RBUTTONDOWN:
                    OnMouseClick?.Invoke(x, y); break;
                case WindowMessage.WM_LBUTTONUP:
                case WindowMessage.WM_RBUTTONUP:
                    OnMouseDown?.Invoke(x, y); break;
            }
        }
    }
}
