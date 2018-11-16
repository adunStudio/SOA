using System;
using System.Windows.Forms;

using KKK.Interface;
using KKK.Helper;

namespace KKK.Input
{
    public sealed class Keyboard : IKeyboard
    {
        public event KeyboardEvent OnKeyDown = null;
        public event KeyboardEvent OnKeyPress = null;
        public event KeyboardEvent OnKeyUp = null;

        public void Init()
        {
            HookKeyboard();
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

        private void HookKeyboard()
        {
            HookHelper.instance.HookGlobalKeyboard(HookKeyboardCallback);
        }

        private void HookKeyboardCallback(HookState state, Keys key)
        {
            switch(state)
            {
                case HookState.Down: OnKeyDown?.Invoke(key); break;
                case HookState.Press: OnKeyPress?.Invoke(key); break;
                case HookState.Up: OnKeyUp?.Invoke(key); break;
            }
        }
    }
}
