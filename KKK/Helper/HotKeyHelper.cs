using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interactivity;
using KKK.WindowAPI;

namespace KKK.Helper
{
    using HANDLE = IntPtr;

    public enum HotKeyModifiers
    {
        ALT = 1, // MOD_ALT
        CONTROL = 2, // MOD_CONTROL
        SHIFT = 3, // MOD_SHIFT
        WIN = 4, // MOD_WIN 
    }

    class HotKeyHelper : IDisposable
    {
        public const int WM_HOTKEY = 0x312;

        private ushort m_HotKeyID = 0;
        
        private readonly HANDLE m_Handle;

        private readonly Action<int> m_OnHotKeyPressed;

        public HotKeyHelper(HANDLE handle, Action<int> func)
        {
            string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + this.GetType().FullName;
            m_HotKeyID = Win32.GlobalAddAtom(atomName);

            m_Handle = handle;
            m_OnHotKeyPressed = func;

            Win32.SetWindowsHookEx(Win32.WH_KEYBOARD_LL, m_OnHotKeyPressed, m_Handle, 1);
        }

        public uint AddListening(Keys key, HotKeyModifiers modifiers)
        {
            Win32.RegisterHotKey(m_Handle, m_HotKeyID, (uint)modifiers, (uint)key);
            return (uint)modifiers | (((uint)key) << 16);
        }

        public void StopListening()
        {
            if (this.m_HotKeyID != 0)
            {
                Win32.UnregisterHotKey(m_Handle, m_HotKeyID);
                Win32.GlobalDeleteAtom(m_HotKeyID);

                m_HotKeyID = 0;
            }
        }

        public void Dispose()
        {
            StopListening();
        }
    }
}
