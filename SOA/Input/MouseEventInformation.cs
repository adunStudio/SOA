using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

using SOA.Helper;
using static PInvoke.User32;
namespace SOA.Input
{
    public class MouseEventInformation : MouseEventArgs
    {
        public int Timestamp { get; }

        public bool IsMouseDown { get; }
        public bool IsMouseUp { get; }

        public bool IsMouseWheelScrolled
        {
            get
            {
                return Delta != 0;
            }
        }

        public bool IsMouseClicked
        {
            get
            {
                return Clicks > 0;
            }
        }
        

        public static MouseEventInformation Get(HookData hookData)
        {
            WindowMessage message = (WindowMessage)hookData.wParam;

            MOUSEINPUT mouseStruct = Marshal.PtrToStructure<MOUSEINPUT>(hookData.lParam);

            int mx = mouseStruct.dx;
            int my = mouseStruct.dy;

            int timestamp = (int)mouseStruct.time;

            MouseButtons button = MouseButtons.None;
            int delta = 0;
            int clickCount = 0;
            bool isMouseDown = false;
            bool isMouseUp = false;

            switch (message)
            {
                // 마우스 왼쪽
                case WindowMessage.WM_LBUTTONDOWN:
                    isMouseDown = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
                case WindowMessage.WM_LBUTTONUP:
                    isMouseUp = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
                case WindowMessage.WM_LBUTTONDBLCLK:
                    isMouseDown = true;
                    button = MouseButtons.Left;
                    clickCount = 2;
                    break;

                // 마우스 오른쪽
                case WindowMessage.WM_RBUTTONDOWN:
                    isMouseDown = true;
                    button = MouseButtons.Right;
                    clickCount = 1;
                    break;
                case WindowMessage.WM_RBUTTONUP:
                    isMouseUp = true;
                    button = MouseButtons.Right;
                    clickCount = 1;
                    break;
                case WindowMessage.WM_RBUTTONDBLCLK:
                    isMouseDown = true;
                    button = MouseButtons.Right;
                    clickCount = 2;
                    break;

                // 마우스 가운데
                case WindowMessage.WM_MBUTTONDOWN:
                    isMouseDown = true;
                    button = MouseButtons.Middle;
                    clickCount = 1;
                    break;
                case WindowMessage.WM_MBUTTONUP:
                    isMouseUp = true;
                    button = MouseButtons.Middle;
                    clickCount = 1;
                    break;
                case WindowMessage.WM_MBUTTONDBLCLK:
                    isMouseDown = true;
                    button = MouseButtons.Middle;
                    clickCount = 2;
                    break;

                // 마우스 휠
                case WindowMessage.WM_MOUSEWHEEL:
                    delta = (int)mouseStruct.mouseData;
                    break;
            }

            return new MouseEventInformation(button, clickCount, timestamp, mx, my, delta, isMouseDown, isMouseUp);
        }

        private MouseEventInformation(MouseButtons buttons, int clicks, int timestamp, int mx, int my, int delta, bool isMouseButtonDown, bool isMouseButtonUp)
            : base(buttons, clicks, mx, my, delta)
        {
            Timestamp = timestamp;

            IsMouseDown = isMouseButtonDown;
            IsMouseUp = isMouseButtonUp;
        }

        internal MouseEventInformation ToDobuleClickMouseEventInformation()
        {
            return new MouseEventInformation(Button, 2, Timestamp, X, Y, Delta, IsMouseDown, IsMouseUp);
        }
    }
}
