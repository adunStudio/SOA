using System;
using System.Windows.Forms;

namespace SOA.Interface
{
    public delegate void KeyboardEvent(Keys key);

    public interface IKeyboard : IInit
    {
        event KeyboardEvent OnDown;
        event KeyboardEvent OnUp;

        void HotKey(Keys keys, Action func);

        void HotKey(string keys, Action func);

        void Send(params Keys[] keys);
    }
}
