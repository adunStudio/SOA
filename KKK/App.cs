using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;

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

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private bool m_IsMouseDowned = false;
        private Point m_MousePoint;

        public App()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, 0); // 콘솔 숨기기

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
            Pen pen = new Pen(Color.Red, 9);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            e.Graphics.DrawRectangle(pen, rect);
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
            AutoCapture();
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
