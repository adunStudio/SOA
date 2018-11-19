using System;
using System.Windows.Forms;

namespace KKK.Interface
{
    public delegate void KeyboardEvent(Keys key);

    public interface IKeyboard : IInit
    {
        event KeyboardEvent OnKeyDown;
        event KeyboardEvent OnKeyUp;

        void HotKey(Keys keys, Action func);

        void HotKey(string keys, Action func);

        void Send(params Keys[] keys);
    }
}
