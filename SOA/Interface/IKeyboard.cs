using System;
using System.Windows.Forms;

namespace SOA.Interface
{
    public delegate void KeyboardEvent(Keys key);

    public interface IKeyboard : IInit, IDelay<IKeyboard>
    {
        event KeyboardEvent OnKeyDown;
        event KeyboardEvent OnKeyUp;

        void HotKey(Keys keys, Action func);

        void DownKey(Keys keys, Action func);

        void UpKey(Keys keys, Action func);

        void ComboKey(Keys key1, Keys key2, Action func);
       
        void ComboKey(Keys key1, Keys key2, Keys key3, Action func);

        void ComboKey(Keys key1, Keys key2, Keys key3, Keys key4, Action func);
    
        IKeyboard Send(params Keys[] keys);

        IKeyboard Send(string text);

        IKeyboard SendCombo(params Keys[] keys);

        IKeyboard SendCombo(string text);

        bool IsKeyDown(Keys key);
    }
}
