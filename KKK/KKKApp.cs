using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;
using System.Text;
using System.Globalization;

using KKK.Extension;
using KKK.WindowAPI;
using KKK.Input;
using KKK.Util;

namespace KKK
{
    public sealed class KKKApp
    {
        #region Inputs
        public IKeyboard Keyboard { get; } = new Keyboard();
        #endregion

        #region Utils
        public ICamera Camera { get; } = new Camera();
        public ICommand Command { get; } = new Command();
        #endregion


        public KKKApp()
        {
        }
    }

        
}
