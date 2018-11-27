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

        void DownKey(Keys keys, Action func);

        void UpKey(Keys keys, Action func);

        void ComboKey(Keys keys1, Keys keys2, Action func);
       
        void ComboKey(Keys keys1, Keys keys2, Keys keys3, Action func);

        void ComboKey(Keys keys1, Keys keys2, Keys keys3, Keys keys4, Action func);
    
        void Send(string text);

        bool IsKeyDown(Keys key);
    }
}
