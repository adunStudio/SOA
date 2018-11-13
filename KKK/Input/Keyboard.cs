using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KKK.Input
{
    public sealed class Keyboard : IKeyboard
    {
        public void HotKey(Keys keys, Action func)
        {
            AddHotKey(keys, func);
        }

        public void AddHotKey(Keys keys, Action func)
        {

        }

        public void Send(params Keys[] keys)
        {

        }

    }

    public static class Win32KeybardHelper
    {

    }

}
