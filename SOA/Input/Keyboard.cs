﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using SOA.Interface;
using SOA.Helper;
using static PInvoke.User32;

namespace SOA.Input
{
    public sealed class Keyboard : IKeyboard
    {
        public event KeyboardEvent OnKeyDown = null;
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
            KeyEventInformation info = KeyEventInformation.Get(hookData);

            Keys key = info.KeyCode | 
                (info.Control ? Keys.Control : Keys.None) |
                (info.Shift ? Keys.Shift : Keys.None) |
                (info.Alt ? Keys.Alt : Keys.None);

            if (info.IsKeyDown)
            {
                OnKeyDown?.Invoke(key);
            }

            if (info.IsKeyUp)
            {
                OnKeyUp?.Invoke(key);
            }
        }
    }
}