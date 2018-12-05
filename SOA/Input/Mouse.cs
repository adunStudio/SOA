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
        public event MouseEvent OnMouseDown = null;
        public event MouseEvent OnMouseUp = null;
        public event MouseEvent OnMouseClick = null;
        public event MouseEvent OnMouseDoubleClick = null;
        public event MouseEvent OnMouseMove = null;
        public event MouseEvent OnMouseWheel = null;
        public event MouseEvent OnMouseDragStart = null;
        public event MouseEvent OnMouseDragEnd = null;

        public int X { get { return m_PreviousX; } }
        public int Y { get { return m_PreviousY; } }

        private MouseButtons m_DoubleButton = MouseButtons.None;
        private MouseButtons m_SingleButton = MouseButtons.None;

        private int m_SystemDoubleClickTime;
        private int m_SystemDragX;
        private int m_SystemDragY;

        private MouseButtons m_PreviousClickedButton;
        private int m_PreviousClickedTime;

        private const int m_DefaultPositionXY = -1;

        private int m_PreviousX = m_DefaultPositionXY;
        private int m_PreviousY = m_DefaultPositionXY;

        private int m_PreviousClickedX = m_DefaultPositionXY;
        private int m_PreviousClickedY = m_DefaultPositionXY;

        private int m_DragStartPositionX = m_DefaultPositionXY;
        private int m_DragStartPositionY = m_DefaultPositionXY;

        private bool m_dragMode = false;
         
        public void Init()
        {
            m_SystemDoubleClickTime = GetDoubleClickTime();
            m_SystemDragX = GetSystemMetrics(SystemMetric.SM_CXDRAG);
            m_SystemDragY = GetSystemMetrics(SystemMetric.SM_CYDRAG);

            PInvoke.POINT mousePoint = GetCursorPos();
            m_PreviousX = mousePoint.x;
            m_PreviousY = mousePoint.y;

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
            MouseButtons button = info.Button;

            // 마우스 다운
            if(info.IsMouseDown)
            {
                if(IsDoubleClick(info))
                {
                    info = info.ToDobuleClickMouseEventInformation();
                }

                OnMouseDown?.Invoke(mx, my, button);

                if(info.Clicks == 2)
                {
                    m_DoubleButton |= info.Button;
                }

                if(info.Clicks == 1)
                {
                    m_SingleButton |= button;
                }
            }

            // 마우스 업
            if (info.IsMouseUp)
            {
                OnMouseUp?.Invoke(mx, my, button);

                // 마우스 클릭
                if((m_SingleButton & button) != MouseButtons.None)
                {
                    OnMouseClick?.Invoke(mx, my, button);
                    m_SingleButton &= ~button;
                }

                // 마우스 더블 클릭
                if ((m_DoubleButton & button) != MouseButtons.None)
                {
                    OnMouseDoubleClick?.Invoke(mx, my, button);
                    m_DoubleButton &= ~button;
                }

                if (info.Clicks == 2)
                {
                    m_PreviousClickedButton = MouseButtons.None;
                    m_PreviousClickedTime = 0;
                    m_PreviousClickedX = m_DefaultPositionXY;
                    m_PreviousClickedY = m_DefaultPositionXY;
                }

                if (info.Clicks == 1)
                {
                    m_PreviousClickedButton = info.Button;
                    m_PreviousClickedTime = info.Timestamp;
                    m_PreviousClickedX = mx;
                    m_PreviousClickedY = my;
                }
            }

            // 마우스 스크롤
            if(info.IsMouseWheelScrolled)
            {
                OnMouseWheel?.Invoke(mx, my, button, info.Delta > 0 ? 1 : -1);
            }

            // 마우스 이동
            if(IsMoved(mx, my))
            {
                m_PreviousX = mx;
                m_PreviousY = my;

                OnMouseMove?.Invoke(mx, my, button);
            }

            // 마우스 드래그
            if ((m_SingleButton & MouseButtons.Left) != MouseButtons.None)
            {
                if (m_DragStartPositionX == m_DefaultPositionXY && m_DragStartPositionY == m_DefaultPositionXY)
                {
                    m_DragStartPositionX = mx;
                    m_DragStartPositionY = my;
                }

                // 마우스 드래그 스타트
                if (m_dragMode == false)
                {
                    bool isXDragging = Math.Abs(mx - m_DragStartPositionX) > m_SystemDragX;
                    bool isYDragging = Math.Abs(my - m_DragStartPositionY) > m_SystemDragY;

                    m_dragMode = isXDragging || isYDragging;

                    if (m_dragMode == true)
                    {
                        OnMouseDragStart?.Invoke(mx, my, button);
                    }
                }
            }
            else
            {
                m_DragStartPositionX = m_DefaultPositionXY;
                m_DragStartPositionY = m_DefaultPositionXY;

                // 마우스 드래그 엔드
                if(m_dragMode == true)
                {
                    OnMouseDragEnd?.Invoke(mx, my, button);

                    m_dragMode = false;
                }
            }
        }

        private bool IsMoved(int x, int y)
        {
            return m_PreviousX != x || m_PreviousY != y;
        }

        private bool IsDoubleClick(MouseEventInformation info)
        {
            return 
                info.Button == m_PreviousClickedButton &&
                info.X == m_PreviousClickedX &&
                info.Y == m_PreviousClickedY &&
                info.Timestamp - m_PreviousClickedTime <= m_SystemDoubleClickTime;
        }

        [DllImport("user32")]
        private static extern int GetDoubleClickTime();

        public IMouse MoveTo(int x, int y)
        {
            InputHelper.instance.SendMovementAbsolute(x, y);

            return this;
        }

        public IMouse MoveBy(int x, int y)
        {
            InputHelper.instance.SendMovementRelative(x, y);

            return this;
        }

        public IMouse Click(MouseButtons button)
        {
            InputHelper.instance.SendButtonClick(button);

            return this;
        }

        public IMouse DoubleClick(MouseButtons button)
        {
            InputHelper.instance.SendButtonDoubleClick(button);

            return this;
        }

        public IMouse Wheel(int delta, bool isVertical)
        {
            InputHelper.instance.SendWheel(delta, isVertical);

            return this;
        }

        public  IMouse Drag(int x, int y, int dx, int dy)
        {
            InputHelper.instance.SendDrag(x, y, dx, dy);

            return this;
        }

        public bool IsDragMode()
        {
            return m_dragMode;
        }
    }
}
