using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

using SOA.Extension;
using SOA.Interface;
using SOA.Helper;
using static PInvoke.User32;

namespace SOA.Input
{
    public sealed class Keyboard : IKeyboard
    {
        public event KeyboardEvent OnKeyDown = null;
        public event KeyboardEvent OnKeyUp = null;

        private Dictionary<Keys, Action> m_KeyboardDownHandlers = new Dictionary<Keys, Action>();
        private Dictionary<Keys, Action> m_KeyboardUpHandlers   = new Dictionary<Keys, Action>();

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

        public void DownKey(Keys keys, Action func)
        {
            m_KeyboardDownHandlers.TryAdd(keys, func);
        }

        public void UpKey(Keys keys, Action func)
        {
            m_KeyboardUpHandlers.TryAdd(keys, func);
        }

        public void SendKey(params Keys[] keys)
        {

        }

        private void HookKeyboardCallback(HookData hookData)
        {
            KeyEventInformation info = KeyEventInformation.Get(hookData);

            Keys key = info.KeyCode | 
                (info.Control ? Keys.Control : Keys.None) |
                (info.Shift ? Keys.Shift : Keys.None) |
                (info.Alt ? Keys.Alt : Keys.None);

            if (info.IsKeyDown)
            {
                OnKeyDown?.Invoke(key);
                m_KeyboardDownHandlers.TryInvoke(key);
            }

            if (info.IsKeyUp)
            {
                OnKeyUp?.Invoke(key);
                m_KeyboardUpHandlers.TryInvoke(key);
            }
        }
    }
}
