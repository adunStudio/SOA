using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

using SOA.Interface;
using SOA.Helper;
using static PInvoke.User32;

namespace SOA.Input
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

        private int m_SystemDoubleClickTime;

        private MouseButtons m_PreviousClicked;
        private Point m_PreviousClickedLocation;
        private int m_PreviousClickedTime;

        public void Init()
        {
            m_SystemDoubleClickTime = GetDoubleClickTime();
            HookHelper.instance.HookGlobalMouse(HookMouseCallback);
        }

        private void HookMouseCallback(HookData hookData)
        {
            //MouseEventInformation info = MouseEventInformation.Get(hookData);

   
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

        private bool IsDoubleClick(MouseEventInformation info)
        {
            return info.Button == m_PreviousClicked &&
                info.Location == m_PreviousClickedLocation &&
                info.Timestamp - m_PreviousClickedTime <= m_SystemDoubleClickTime;
        }

        [DllImport("user32")]
        private static extern int GetDoubleClickTime();
    }
}
