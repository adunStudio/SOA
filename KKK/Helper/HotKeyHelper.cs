using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static PInvoke.User32;

namespace KKK.Helper
{
    public sealed class HotKeyHelper : Singleton<HotKeyHelper>, IDisposable, IMessageFilter 
    {
        // https://docs.microsoft.com/en-us/windows/desktop/inputdev/user-input
        // https://docs.microsoft.com/ko-kr/windows/desktop/inputdev/wm-hotkey
        //                                                 LOW: key            HIGH: MOD_
        private const uint LOWER_BIT_MASK     = 0xFFFF; // 0000 0000 0000 0000 1111 1111 1111 1111

        private const uint MOD_NONE           = 0x0000; // 0000 0000 0000 0000 0000 0000 0000 0000  
        private const uint MOD_ALT            = 0x0001; // 0000 0000 0000 0000 0000 0000 0000 0001
        private const uint MOD_CONTROL        = 0x0002; // 0000 0000 0000 0000 0000 0000 0000 0010
        private const uint MOD_SHIFT          = 0x0004; // 0000 0000 0000 0000 0000 0000 0000 0100
        private const uint MOD_WIN            = 0x0008; // 0000 0000 0000 0000 0000 0000 0000 1000

        private Dictionary<Keys, Action> m_Handlers = new Dictionary<Keys, Action>();

        private int m_CurrentHotkeyId = 0;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

        public HotKeyHelper()
        {
            Application.AddMessageFilter(this);
        }

        public void Dispose()
        {
            Application.RemoveMessageFilter(this);

            for (int index = m_CurrentHotkeyId; index > 0; --index)
            {
                UnregisterHotKey(IntPtr.Zero, index);
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != (int)WindowMessage.WM_HOTKEY)
            {
                return false;
            }

            OnHotKey(m.LParam);

            return true;
        }

        private void OnHotKey(IntPtr lParam)
        {
            uint modifiers = TranslateModifiers(lParam);
            long keys = modifiers + (((int)lParam >> 16) & LOWER_BIT_MASK);

            Action handler = null;

            if (m_Handlers.TryGetValue((Keys)keys, out handler) == false)
            {
                return;
            }

            handler?.Invoke();
        }

        private uint TranslateModifiers(IntPtr lParam)
        {
            // & : 비트 AND 연산자를 이용해서 비트의 상태를 알 수 있다. 
            // | : 비트  OR 연산자를 이용해서 비트를 켤 수 있다. 
            uint inputModifiers  = (uint)lParam & LOWER_BIT_MASK;
            uint outputModifiers = 0;

            if ((inputModifiers & MOD_ALT) == MOD_ALT)
            {
                outputModifiers |= (uint)Keys.Alt;
            }

            if ((inputModifiers & MOD_CONTROL) == MOD_CONTROL)
            {
                outputModifiers |= (uint)Keys.Control;
            }

            if ((inputModifiers & MOD_SHIFT) == MOD_SHIFT)
            {
                outputModifiers |= (uint)Keys.Shift;
            }

            return outputModifiers;
        }

        private  uint TranslateModifiers(Keys hotkey)
        {
            uint modifiers = 0;

            if ((hotkey & Keys.Alt) == Keys.Alt)
            {
                modifiers |= MOD_ALT;
            }

            if ((hotkey & Keys.Control) == Keys.Control)
            {
                modifiers |= MOD_CONTROL;
            }

            if ((hotkey & Keys.Shift) == Keys.Shift)
            {
                modifiers |= MOD_SHIFT;
            }

            return modifiers;
        }

        public void RegisterHotKey(Keys hotkey, Action handler)
        {
            uint modifiers = TranslateModifiers(hotkey);
            uint key       = (uint)hotkey & LOWER_BIT_MASK;

            m_Handlers.Add(hotkey, handler);

            m_CurrentHotkeyId++;

            RegisterHotKey(IntPtr.Zero, m_CurrentHotkeyId, modifiers, key);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
