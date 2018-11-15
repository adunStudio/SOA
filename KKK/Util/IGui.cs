using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKK.Util
{
    class IGui
    {
        //private Win32.WINDOWPLACEMENT targetPlacement;

        /*protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "KKK";
            this.Name = "KKK";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.ClientSize = new Size(100, 100);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.TransparencyKey = Color.Black;
            this.TopMost = true;

            this.Load += new EventHandler(this.OnLoad);
            this.MouseDown += new MouseEventHandler(this.OnMouseDown);
            this.MouseUp += new MouseEventHandler(this.OnMouseUp);
            this.MouseMove += new MouseEventHandler(this.OnMouseMove);

            this.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (targetPlacement.length != 0)
            {
                Size size = new Size(targetPlacement.rcNormalPosition.Right - (targetPlacement.rcNormalPosition.Left * 2), targetPlacement.rcNormalPosition.Bottom - (targetPlacement.rcNormalPosition.Top * 2));
                Point point = new Point(targetPlacement.rcNormalPosition.Left, targetPlacement.rcNormalPosition.Top);

                Pen pen = new Pen(Color.Red, 6);
                Rectangle rect = new Rectangle(point.X, point.Y, size.Width, size.Height);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Run();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            m_IsMouseDowned = true;
            m_MousePoint.X = -e.X;
            m_MousePoint.Y = -e.Y;

            Console.WriteLine("mouseDown");
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            m_IsMouseDowned = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_IsMouseDowned)
            {
                this.Location = new Point(
                    x: Location.X + m_MousePoint.X + e.X,
                    y: Location.Y + m_MousePoint.Y + e.Y);
            }
        }

        private void Run()
        {
            //AutoCapture();
            //  PrintWindow();

            IntPtr handle = FindWindow("Adunis_Console");
            // targetPlacement = GetWindowPlacement(handle);


            Invalidate(true);
        }


        private Win32.WINDOWPLACEMENT GetWindowPlacement(IntPtr handle)
        {
            Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);

            if (Win32.GetWindowPlacement(handle, ref placement) == false)
            {
                placement.length = 0;
            }

            return placement;
        }

        private IntPtr FindWindow(string name)
        {
            IntPtr handle = Win32.FindWindow(null, name);

            if (handle != (IntPtr)0)
            {
                return handle;
            }

            return (IntPtr)0;
        }

        private void PrintWindow()
        {
            Win32.EnumWindowCallback callback = new Win32.EnumWindowCallback(EnumWindowsProc);
            Win32.EnumWindows(callback, 0);
        }

        private bool EnumWindowsProc(IntPtr hWnd, int lParam)
        {
            uint style = (uint)Win32.GetWindowLong(hWnd, Win32.GWL_STYLE);

            if ((style & Win32.WS_VISIBLE) == Win32.WS_VISIBLE && (style & Win32.WS_CAPTION) == Win32.WS_CAPTION)
            {
                if (Win32.GetParent(hWnd) == (IntPtr)0)
                {
                    StringBuilder Buf = new StringBuilder(256);

                    string hexOutput = String.Format("{0:X}", hWnd);

                    Console.WriteLine(hexOutput);
                    if (Win32.GetWindowText(hWnd, Buf, 256) > 0)
                    {
                        Console.WriteLine(Buf.ToString());
                    }

                    if (Win32.GetClassName(hWnd, Buf, 256) > 0)
                    {
                        Console.WriteLine(Buf.ToString());
                    }
                    Console.WriteLine("");

                    /*
                     * 다른 방법
                     * // TODO: 비교
                    int handle = int.Parse(Console.ReadLine());
            
                    int txtLength = Win32.SendMessage(handle, Win32.WM_GETTEXTLENGTH, 0, 0);

                    StringBuilder sb = new StringBuilder(txtLength + 1);

                    Win32.SendMessage(handle, Win32.WM_GETTEXT, sb.Capacity, sb);

                    Console.Write(sb.ToString());
                     */
        /*  }
      }

      return true;
  }
}*/
    }
}
