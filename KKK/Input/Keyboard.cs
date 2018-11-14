using System;
using System.Windows.Forms;
using KKK.Helper;

namespace KKK.Input
{
    public sealed class Keyboard : IKeyboard
    {
        private HotKeyHelper m_HotKeyHelper = new HotKeyHelper();

        public void HotKey(Keys keys, Action func)
        {
            AddHotKey(keys, func);
        }

        public void AddHotKey(Keys keys, Action func)
        {
            m_HotKeyHelper.RegisterHotKey(keys, func);
        }

        public void Send(params Keys[] keys)
        {

        }
    }
}
