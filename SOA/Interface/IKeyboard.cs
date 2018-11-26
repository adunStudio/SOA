using System;
using System.Windows.Forms;

namespace SOA.Interface
{
    public delegate void KeyboardEvent(Keys key);

    public interface IKeyboard : IInit
    {
        event KeyboardEvent OnKeyDown;
        event KeyboardEvent OnKeyUp;

        void HotKey(Keys keys, Action func);

        void HotKey(string keys, Action func);

        void DownKey(Keys keys, Action func);

        void UpKey(Keys keys, Action func);

        void SendKey(params Keys[] keys);
    }
}
