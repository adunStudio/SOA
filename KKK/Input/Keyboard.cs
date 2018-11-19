using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using KKK.Interface;
using KKK.Helper;
using static PInvoke.User32;

namespace KKK.Input
{
    public sealed class Keyboard : IKeyboard
    {
        public event KeyboardEvent OnKeyDown = null;
        public event KeyboardEvent OnKeyPress = null;
        public event KeyboardEvent OnKeyUp = null;

        public void Init()
        {
            HookHelper.instance.HookGlobalKeyboard(HookKeyboardCallback);
        }

        public void HotKey(Keys keys, Action func)
        {
            HotKeyHelper.instance.RegisterHotKey(keys, func);
        }

        public void HotKey(string keys, Action func)
        {

        }

        public void Send(params Keys[] keys)
        {

        }

        private void HookKeyboardCallback(HookData hookData)
        {

            WindowMessage state = (WindowMessage)hookData.wParam;

            KEYBDINPUT keyboardStruct = Marshal.PtrToStructure<KEYBDINPUT>(hookData.lParam);
            Keys key = (Keys)keyboardStruct.wVk;

            switch (state)
            {
                case WindowMessage.WM_KEYDOWN:
                    OnKeyDown?.Invoke(key); break;
                case WindowMessage.WM_KEYUP:
                    OnKeyUp?.Invoke(key); break;
            }
        }
    }
}
