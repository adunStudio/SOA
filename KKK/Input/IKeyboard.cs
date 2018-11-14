using System;
using System.Windows.Forms;

namespace KKK.Input
{
    public interface IKeyboard
    {
        void HotKey(Keys keys, Action func);
        void Send(params Keys[] keys);
    }
}
