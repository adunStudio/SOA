using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PInvoke.User32;
using static PInvoke.Kernel32;

namespace KKK.Input
{
    public sealed class Command : ICommand
    {
        private bool m_visible = true;

        public bool visible
        {
            get { return m_visible; }
            set { SetVisible(value); }
        }

        public void SetVisible(bool visible)
        {
            if(m_visible == visible)
            {
                return;
            }

            IntPtr consoleHandle = GetConsoleWindow();

            if(consoleHandle == IntPtr.Zero)
            {
                return;
            }

            m_visible = visible;

            ShowWindow(consoleHandle, m_visible ? WindowShowStyle.SW_SHOW : WindowShowStyle.SW_HIDE);
        }
    }
}
