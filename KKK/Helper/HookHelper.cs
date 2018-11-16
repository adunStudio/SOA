using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using static PInvoke.User32;
using static PInvoke.Kernel32;
using KKK.WindowAPI;
namespace KKK.Helper
{
    public enum HookState
    {
        Down = 0,
        Press = 1,
        Up = 2
    }

    /// <summary>
    /// wParam: 메시지가 현재 스레드에 의해 전송되었는지 여부
    /// lParam: 메시지에 대한 세부 사항이 들어있는 CWPRETSTRUCT 구조체에 대한 포인터
    /// </summary>
    public struct HookData
    {
        public IntPtr wParam;
        public IntPtr lParam;
    }

    public delegate void HookCallback(HookState state, Keys key);

    public sealed class HookHelper : Singleton<HookHelper>, IDisposable
    {
        private WindowsHookDelegate m_AppHookProc;
        private WindowsHookDelegate m_GlobalHookProc;

        const uint KEY_DOWN_MASK   = 0x40000000; // for bit 30
        const uint KEY_UP_MASK     = 0x80000000; // for bit 31
        const uint KEY_EXTEND_MASK = 0x1000000;  // for bit 24

        public HookHelper()
        {

        }

        public void Dispose()
        {

        }

        public void HookAppKeyboard(HookCallback callback)
        {
            HookApp(WindowsHookType.WH_KEYBOARD, callback);
        }

        public void HookAppMouse(HookCallback callback)
        {
            HookApp(WindowsHookType.WH_MOUSE, callback);
        }

        public void HookGlobalKeyboard(HookCallback callback)
        {
            HookGlobal(WindowsHookType.WH_KEYBOARD_LL, callback);
        }

        public void HookGlobalMouse(HookCallback callback)
        {
            HookGlobal(WindowsHookType.WH_MOUSE_LL, callback);
        }

        private void HookApp(WindowsHookType hookType, HookCallback callback)
        {
            m_AppHookProc = (int nCode, IntPtr wParam, IntPtr lParam) => HookProc(nCode, wParam, lParam, callback); 

            SafeHookHandle handle = SetWindowsHookEx(hookType, m_AppHookProc, IntPtr.Zero, GetCurrentThreadId());
        }

        private void HookGlobal(WindowsHookType hookType, HookCallback callback)
        {
            m_GlobalHookProc = (int nCode, IntPtr wParam, IntPtr lParam) => HookProc(nCode, wParam, lParam, callback);

            SafeHookHandle handle = SetWindowsHookEx(hookType, m_GlobalHookProc, Process.GetCurrentProcess().MainModule.BaseAddress, 0);
        }

        public int HookProc(int nCode, IntPtr wParam, IntPtr lParam, HookCallback callback)
        {
            if(nCode < 0)
            {
                return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
            }

            WindowMessage state = (WindowMessage)wParam;
            KEYBDINPUT keyboardStruct;

            switch (state)
            {
                case WindowMessage.WM_KEYDOWN:
                    keyboardStruct = Marshal.PtrToStructure<KEYBDINPUT>(lParam);
                    callback(HookState.Down, (Keys)keyboardStruct.wVk); break;
                case WindowMessage.WM_KEYUP:
                    keyboardStruct = Marshal.PtrToStructure<KEYBDINPUT>(lParam);
                    callback(HookState.Up, (Keys)keyboardStruct.wVk); break;
            }

            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
    }
}
