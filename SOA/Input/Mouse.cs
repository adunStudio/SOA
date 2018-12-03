using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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

        private MouseButtons m_DoubleButton = MouseButtons.None;
        private MouseButtons m_SingleButton = MouseButtons.None;

        private int m_SystemDoubleClickTime;

        private MouseButtons m_PreviousClicked;

        private const int m_DefaultPositionXY = -1;

        private int m_PreviousX = m_DefaultPositionXY;
        private int m_PreviousY = m_DefaultPositionXY;

        private int m_DragStartPositionX = m_DefaultPositionXY;
        private int m_DragStartPositionY = m_DefaultPositionXY;

        private bool m_IsDragging = false;
         
        private int m_PreviousClickedTime;

        public void Init()
        {
            m_SystemDoubleClickTime = GetDoubleClickTime();
            HookHelper.instance.HookGlobalMouse(HookMouseCallback);
        }

        public IMouse Delay(int ms)
        {
            DateTime now = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);

            DateTime future = now.Add(duration);

            while (future >= now)
            {
                Application.DoEvents();

                now = DateTime.Now;
            }

            return this;
        }

        private void HookMouseCallback(HookData hookData)
        {
            MouseEventInformation info = MouseEventInformation.Get(hookData);

            int mx = info.X;
            int my = info.Y;

            if(info.IsMouseDown)
            {
                OnMouseDown?.Invoke(mx, my);

                if(info.Clicks == 2)
                {
                    m_DoubleButton |= info.Button;
                }

                if(info.Clicks == 1)
                {
                    m_SingleButton |= info.Button;
                }
            }

            if(info.IsMouseUp)
            {
                OnMouseUp?.Invoke(mx, my);

                if((m_SingleButton & info.Button) != MouseButtons.None)
                {
                    OnMouseClick?.Invoke(mx, my);
                    m_SingleButton &= ~info.Button;
                }

                if ((m_DoubleButton & info.Button) != MouseButtons.None)
                {
                    OnMouseDoubleClick?.Invoke(mx, my);
                    m_DoubleButton &= ~info.Button;
                }
            }

            if(info.IsMouseWheelScrolled)
            {
                OnMouseWheel?.Invoke(mx, my);
            }

            if(IsMoved(mx, my))
            {
                m_PreviousX = mx;
                m_PreviousY = my;

                OnMouseMove?.Invoke(mx, my);
            }

           /* if ((m_SingleButton & info.Button) != MouseButtons.None)
            {
                if(m_DragStartPositionX == m_DefaultPositionXY && m_DragStartPositionY == m_DefaultPositionXY)
                {
                    m_DragStartPositionX = mx;
                    m_DragStartPositionY = my;

                    if(m_IsDragging == false)
                    {
                        var isXDragging = 
                    }
                }
            }
            else
            {

            }*/
        }

        private bool IsMoved(int x, int y)
        {
            return m_PreviousX != x || m_PreviousY != y;
        }

        /*private bool IsDoubleClick(MouseEventInformation info)
        {
            return info.Button == m_PreviousClicked &&
                info.Location == m_PreviousClickedLocation &&
                info.Timestamp - m_PreviousClickedTime <= m_SystemDoubleClickTime;
        }*/

        [DllImport("user32")]
        private static extern int GetDoubleClickTime();
    }
}
