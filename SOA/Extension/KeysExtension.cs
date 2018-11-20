using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOA.Extension
{
    public static class KeysExtension
    {
        public static Keys Normalize(this Keys key)
        {
            if ((key & Keys.LControlKey) == Keys.LControlKey || (key & Keys.RControlKey) == Keys.RControlKey)
            {
                return Keys.Control;
            }

            if ((key & Keys.LShiftKey) == Keys.LShiftKey || (key & Keys.RShiftKey) == Keys.RShiftKey)
            {
                return Keys.Shift;
            }

            // Menu = Alt
            if ((key & Keys.LMenu) == Keys.LMenu || (key & Keys.RMenu) == Keys.RMenu)
            {
                return Keys.Alt;
            }

            return key;
        }
    }
}
