using System;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using KKK.WindowAPI;

namespace KKK.Helper
{
    using HANDLE = IntPtr;
    using HINSTANCE = IntPtr;

    public enum HotKeyModifiers
    {
        ALT = 1, // MOD_ALT
        CONTROL = 2, // MOD_CONTROL
        SHIFT = 3, // MOD_SHIFT
        WIN = 4, // MOD_WIN 
    }

    class HotKeyHelper2 : IDisposable, IMessageFilter
    {
        private const uint Lower16BitsMask = 0xFFFF;

        public const int WM_HOTKEY = 0x312;

        private ushort m_HotKeyID = 0;

        private int m_HookID = 0;

        private readonly HANDLE m_Handle;

        private readonly Action<int> m_OnHotKeyPressed;

        public HotKeyHelper2(HANDLE handle, Action<int> func)
        {
            Application.AddMessageFilter(this);

            string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + this.GetType().FullName;
            m_HotKeyID = Win32.GlobalAddAtom(atomName);
            m_Handle = handle;
            m_OnHotKeyPressed = func;

            Win32.HookProc h = new Win32.HookProc(HwndHook);
            m_HookID = Win32.SetWindowsHookEx(Win32.WH_KEYBOARD_LL, HwndHook, IntPtr.Zero, 0);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == (int)WM_HOTKEY && m.WParam.ToInt32() == m_HotKeyID)
            {
                m_OnHotKeyPressed?.Invoke(m.LParam.ToInt32());
            }

            return true;
        }

        private IntPtr HwndHook(int code, IntPtr wParam, IntPtr lParam)
        {

           // Console.WriteLine(Marshal.ReadInt32(lParam));
            Console.WriteLine(wParam);
         
            
            if (wParam == (IntPtr)WM_HOTKEY)
            {
                Console.WriteLine("asdf");
            }


            return  Win32.CallNextHookEx(0, code, wParam, lParam);
        }

        public uint AddListening(Keys key, HotKeyModifiers modifiers)
        {
            Console.WriteLine(Win32.RegisterHotKey(IntPtr.Zero, m_HotKeyID, (uint)modifiers, Lower16BitsMask & (uint)key));
            return (uint)modifiers | (((uint)key) << 16);
        }

        public void StopListening()
        {
            if (this.m_HotKeyID != 0)
            {
                Win32.UnhookWindowsHookEx(m_HookID);
                Win32.UnregisterHotKey(IntPtr.Zero, m_HotKeyID);
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
