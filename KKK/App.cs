using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;
using System.Text;

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

            IntPtr handle = WindowImport.GetConsoleWindow();
            //WindowImport.ShowWindow(handle, 0); // 콘솔 숨기기

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
            PrintProcess();
            //AutoCapture();
        }

        private void PrintProcess()
        {
            WindowImport.EnumWindowCallback callback = new WindowImport.EnumWindowCallback(EnumWindowsProc);
            WindowImport.EnumWindows(callback, 0);
        }

        private bool EnumWindowsProc(int hWnd, int lParam)
        {
            // https://jeongbaek.wordpress.com/2017/08/14/c-%ED%98%84%EC%9E%AC-%EC%8B%A4%ED%96%89%EC%A4%91%EC%9D%B8-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-%EB%AA%A9%EB%A1%9D-%EC%96%BB%EC%96%B4%EC%98%A4%EA%B8%B0/
            
            //윈도우 핸들로 그 윈도우의 스타일을 얻어옴
            UInt32 style = (UInt32)WindowImport.GetWindowLong(hWnd, WindowImport.GCL_HMODULE);
            //해당 윈도우의 캡션이 존재하는지 확인
            if ((style & 0x10000000L) == 0x10000000L && (style & 0x00C00000L) == 0x00C00000L)
            {
                //부모가 바탕화면인지 확인
                if (WindowImport.GetParent(hWnd) == 0)
                {
                    StringBuilder Buf = new StringBuilder(256);
                    //응용프로그램의 이름을 얻어온다
                    if (WindowImport.GetWindowText(hWnd, Buf, 256) > 0)
                    {
                        Console.WriteLine(Buf.ToString());
                        /**try
                        {
                            //HICON 아이콘 핸들을 얻어온다
                            IntPtr hIcon = WindowImport.GetClassLong((IntPtr)hWnd, WindowImport.GCL_HICON);
                            //아이콘 핸들로 Icon 객체를 만든다
                            Icon icon = Icon.FromHandle(hIcon);
                            imgList.Images.Add(icon);
                        }
                        catch (Exception)
                        {
                            //예외의 경우는 자기 자신의 윈도우인 경우이다.
                            imgList.Images.Add(this.Icon);
                        }
                        listView1.Items.Add(new ListViewItem(Buf.ToString(), listView1.Items.Count));**/
                    }
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
