using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;
using System.Text;
using KKK.WindowAPI;

namespace KKK
{
    class App : Form
    {
        static App Instance = null;

        static Point GetLocation()
        {
            return Instance.Location;
        }

        static Size GetSize()
        {
            return Instance.ClientSize;
        }

  
        private bool m_IsMouseDowned = false;
        private Point m_MousePoint;

        public App()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            //IntPtr handle = Win32.GetConsoleWindow();
            //Win32.ShowWindow(handle, Win32.SW_HIDE); // 콘솔 숨기기

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "KakaoKungKuotta";
            this.Name = "KakaoKungKuotta";
            this.ClientSize = new Size(350, 550);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
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
            //Pen pen = new Pen(Color.Red, 9);
            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            //e.Graphics.DrawRectangle(pen, rect);
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
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            m_IsMouseDowned = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if(m_IsMouseDowned)
            {
                this.Location = new Point(
                    x: Location.X + m_MousePoint.X + e.X,
                    y: Location.Y + m_MousePoint.Y + e.Y);
            }
        }

        private void Run()
        {
            FindWindow();
            PrintWindow();
            //AutoCapture();
        }

        private void FindWindow()
        {
            string name = "Zulip";
            IntPtr handle = Win32.FindWindow(null, name);

            if(handle != (IntPtr)0)
            {
                Win32.ShowWindow(handle, Win32.SW_SHOWMAXIMIZED);
            }
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

                    if (Win32.GetWindowText(hWnd, Buf, 256) > 0)
                    {
                        Console.WriteLine(Buf.ToString());
                    }

                    if (Win32.GetClassName(hWnd, Buf, 256) > 0)
                    {
                        Console.WriteLine(Buf.ToString());
                    }
                    Console.WriteLine("");
                }
            }

            return true;
        }

        private void AutoCapture()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;

            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                {
                    Rectangle rect = new Rectangle(
                        x: Location.X,
                        y: Location.Y,
                        width: ClientSize.Width,
                        height: ClientSize.Height
                        );

                    using (Image img = ScreenCapture.Capture(rect))
                    {
                        img.Save(Path.Combine(Environment.CurrentDirectory, "screenShoot.png"), ImageFormat.Png);
                    }
                };

            timer.Start();
        }
    }
}
