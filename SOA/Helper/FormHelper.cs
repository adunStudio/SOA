using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using static PInvoke.User32;
using static PInvoke.Kernel32;

namespace SOA.Helper
{
    class FormHelper : SingletonForm<FormHelper>
    {
        private Rectangle m_FullScreenBounds = Rectangle.Empty;

        public FormHelper()
        {
            InitFullScreenInformation();
            InitializeComponent();
        }

        private void InitFullScreenInformation()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                m_FullScreenBounds = Rectangle.Union(m_FullScreenBounds, screen.Bounds);
            }             
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "SOA";
            this.Name = "SOA";

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(m_FullScreenBounds.Left, m_FullScreenBounds.Top);
            this.ClientSize = new Size(m_FullScreenBounds.Width, m_FullScreenBounds.Height);

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;

            this.Opacity = 0.5;
            this.TopMost = true;

            this.Load += new EventHandler(this.OnLoad);

            this.ResumeLayout(false);
        }

        private void Run()
        {
            Invalidate(true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Run();
        }

        public void MsgBox(string text, string caption = "")
        {
            System.Windows.Forms.MessageBox.Show(text, caption);
        }
    }
}
