using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

using SOA.Interface;
using SOA.Helper;
using static PInvoke.User32;
using static PInvoke.Kernel32;

namespace SOA.Util
{
    public class Clip : IClip
    {
        private const int CF_UNICODETEXT = 13;

        public IClip Delay(int ms)
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

        public void Copy()
        {
            VirtualKey[] virtualKeyArray = new VirtualKey[] { VirtualKey.VK_CONTROL, VirtualKey.VK_C };

            InputHelper.instance.SendComboKeys(virtualKeyArray);
        }

        public void Copy(string text)
        {
            SetText(text);
        }

        public void Paste()
        {
            VirtualKey[] virtualKeyArray = new VirtualKey[] { VirtualKey.VK_CONTROL, VirtualKey.VK_V };

            InputHelper.instance.SendComboKeys(virtualKeyArray);
        }

        public void SetText(string text)
        {
            text += " ";

            try
            {
                if (OpenClipboard(IntPtr.Zero) == false)
                {
                    return;
                }

                IntPtr length = (IntPtr)((text.Length) * sizeof(char));

                IntPtr handle = GlobalAlloc_IntPtr(GlobalAllocFlags.GMEM_MOVEABLE | GlobalAllocFlags.GMEM_DDESHARE, length);
                if (handle == IntPtr.Zero)
                {
                    return;
                }

                IntPtr pointer = IntPtr.Zero;

                try
                {
                    pointer = GlobalLock(handle);
                    if (pointer == IntPtr.Zero)
                    {
                        return;
                    }

                    byte[] buff = Encoding.Unicode.GetBytes(text);

                    Marshal.Copy(buff, 0, pointer, (int)length);

                    EmptyClipboard();

                    SetClipboardData(CF_UNICODETEXT, handle);
                }
                finally
                {
                    if (pointer != IntPtr.Zero)
                    {
                        GlobalUnlock(handle);
                    }
                }
            }
            finally
            {
                CloseClipboard();
            }
        }

        public string GetText()
        {
            try
            {
                if (OpenClipboard(IntPtr.Zero) == false)
                {
                    return string.Empty;
                }

                IntPtr handle = GetClipboardData_IntPtr(CF_UNICODETEXT);
                if (handle == IntPtr.Zero)
                {
                    return string.Empty;
                }

                IntPtr pointer = IntPtr.Zero;

                try
                {
                    pointer = GlobalLock(handle);
                    if (pointer == IntPtr.Zero)
                    {
                        return string.Empty;
                    }

                    int length = GlobalSize(handle);

                    byte[] buff = new byte[length];

                    Marshal.Copy(pointer, buff, 0, length);

                    return Encoding.Unicode.GetString(buff).TrimEnd('\0');
                }
                finally
                {
                    if (pointer != IntPtr.Zero)
                    {
                        GlobalUnlock(handle);
                    }
                }
            }
            finally
            {
                CloseClipboard();
            }
        }

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern int GlobalSize(IntPtr hMem);
    }
}
