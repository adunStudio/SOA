using System;
using System.Linq;
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
        private event KeyboardEvent OnKeyCombo = null;

        private Dictionary<Keys, Action> m_KeyboardDownHandlers  = new Dictionary<Keys, Action>();
        private Dictionary<Keys, Action> m_KeyboardUpHandlers    = new Dictionary<Keys, Action>();

        private Dictionary<Keys, bool> m_KeyState = new Dictionary<Keys, bool>();

        public void Init()
        {
            HookHelper.instance.HookGlobalKeyboard(HookKeyboardCallback);
        }

        public IKeyboard Delay(int ms)
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

        public void HotKey(Keys keys, Action func)
        {
            HotKeyHelper.instance.RegisterHotKey(keys, func);
        }

        public void DownKey(Keys keys, Action func)
        {
            m_KeyboardDownHandlers.TryAdd<Action>(keys, func);
        }

        public void UpKey(Keys keys, Action func)
        {
            m_KeyboardUpHandlers.TryAdd<Action>(keys, func);
        }

        public void ComboKey(Keys key1, Keys key2, Action func)
        {
            ComboKey(func, key1, key2);
        }

        public void ComboKey(Keys key1, Keys key2, Keys key3, Action func)
        {
            ComboKey(func, key1, key2, key3);
        }

        public void ComboKey(Keys key1, Keys key2, Keys key3, Keys key4, Action func)
        {
            ComboKey(func, key1, key2, key3, key4);
        }

        private void ComboKey(Action func, params Keys[] keys)
        {
            OnKeyCombo += (k) =>
            { 
                foreach(Keys key in keys)
                {
                    if (key > Keys.OemClear)
                    {
                        Console.WriteLine("ComboKey: 이 메서드에서 Control, Shift, Alt 특수문자는 사용 불가능합니다.");
                        return;
                    }

                    if (IsKeyDown(key) == false)
                    {
                        return;
                    }
                }

                func();
            };
        }

        public IKeyboard Send(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (key > Keys.OemClear)
                {
                    Console.WriteLine("isKeyDown(Keys key): 이이 메서드에서 Control, Shift, Alt 특수문자는 사용 불가능합니다.");
                    return this;
                }
            }

            VirtualKey[] virtualKeyArray = Array.ConvertAll(keys, key => (VirtualKey)key);

            InputHelper.instance.SendKeys(virtualKeyArray);

            return this;
        }

        public IKeyboard Send(string text)
        {
            InputHelper.instance.SendText(text);

            return this;
        }

        public IKeyboard SendCombo(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (key > Keys.OemClear)
                {
                    Console.WriteLine("isKeyDown(Keys key): 이이 메서드에서 Control, Shift, Alt 특수문자는 사용 불가능합니다.");
                    return this;
                }
            }

            VirtualKey[] virtualKeyArray = Array.ConvertAll(keys, key => (VirtualKey)key);

            InputHelper.instance.SendComboKeys(virtualKeyArray);

            return this;
        }

        public IKeyboard SendCombo(string text)
        {
            InputHelper.instance.SendComboText(text);

            return this;
        }

        public bool IsKeyDown(Keys key)
        {
            if(key > Keys.OemClear)
            {
                Console.WriteLine("isKeyDown(Keys key): 이이 메서드에서 Control, Shift, Alt 특수문자는 사용 불가능합니다.");
                return false;
            }

            bool result = false;

            KeyEventArgs info = new KeyEventArgs(key);

            if(m_KeyState.TryGetValue(info.KeyCode, out result) == false)
            {
                return false;
            }

            return result;
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
                m_KeyState[info.KeyCode] = true;

                OnKeyCombo?.Invoke(key);

                OnKeyDown?.Invoke(key);

                m_KeyboardDownHandlers.TryInvoke(key);
            }

            if (info.IsKeyUp)
            {
                m_KeyState[info.KeyCode] = false;

                OnKeyUp?.Invoke(key);

                m_KeyboardUpHandlers.TryInvoke(key);
            }
        }
    }
}
