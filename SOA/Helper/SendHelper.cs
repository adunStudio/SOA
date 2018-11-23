using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static PInvoke.User32;

namespace SOA.Helper
{
    public sealed class SendHelper : Singleton<SendHelper>, IDisposable
    {
        public SendHelper()
        {

        }

        public void Dispose()
        {

        }

        private IntPtr Send(IntPtr hWnd, WindowMessage wMsg, IntPtr wParam, IntPtr lParam)
        {
            return SendMessage(hWnd, wMsg, wParam, lParam);
        }

        public IntPtr CloseMessage(IntPtr hWnd)
        {
            return Send(hWnd, WindowMessage.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
