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
        public event MouseEvent OnMove = null;
        public event MouseEvent OnClick = null;
        public event MouseEvent OnDoubleClick = null;
        public event MouseEvent OnDown = null;
        public event MouseEvent OnUp = null;
        public event MouseEvent OnWheel = null;
        public event MouseEvent OnDragStart = null;
        public event MouseEvent OnDragEnd = null;

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
                    OnMove?.Invoke(x, y); break;
                case WindowMessage.WM_LBUTTONDOWN:
                case WindowMessage.WM_RBUTTONDOWN:
                    OnClick?.Invoke(x, y); break;
                case WindowMessage.WM_LBUTTONUP:
                case WindowMessage.WM_RBUTTONUP:
                    OnDown?.Invoke(x, y); break;
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
