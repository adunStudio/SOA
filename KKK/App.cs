using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;
using System.Text;
using System.Globalization;
using KKK.WindowAPI;
using KKK.Helper;

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

        private Win32.WINDOWPLACEMENT targetPlacement;

        private HotKeyHelper m_HotKeyHelper = null;

        public App()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            //SetConsoleVisible(false);

            targetPlacement.length = 0;

            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "KakaoKungKuotta";
            this.Name = "KakaoKungKuotta";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.ClientSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
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

            if(targetPlacement.length != 0)
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
            //AutoCapture();
          //  PrintWindow();
//
            IntPtr handle = FindWindow("Adunis_Console");
           // targetPlacement = GetWindowPlacement(handle);


            Invalidate(true);

            m_HotKeyHelper = new HotKeyHelper(handle, (int a) => {
                Console.WriteLine("핫키!");
            });

            m_HotKeyHelper.AddListening(Keys.A, HotKeyModifiers.CONTROL);
            m_HotKeyHelper.AddListening(Keys.B, HotKeyModifiers.CONTROL);
        }


        private Win32.WINDOWPLACEMENT GetWindowPlacement(IntPtr handle)
        {
            Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);

            if(Win32.GetWindowPlacement(handle, ref placement) == false)
            {
                placement.length = 0;
            }

            return placement;
        }

        private IntPtr FindWindow(string name)
        {
            IntPtr handle = Win32.FindWindow(null, name);

            if(handle != (IntPtr)0)
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

        private void SetConsoleVisible(bool visible)
        {
            IntPtr consoleHandle = Win32.GetConsoleWindow();
            Win32.ShowWindow(consoleHandle, visible ? Win32.SW_SHOW : Win32.SW_HIDE);
        }
    }
}
