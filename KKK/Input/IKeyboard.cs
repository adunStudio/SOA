using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KKK.Input
{
    public interface IKeyboard
    {
        void HotKey(Keys keys, Action func);
        void Send(params Keys[] keys);
    }
}
