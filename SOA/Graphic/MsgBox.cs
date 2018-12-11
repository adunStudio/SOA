using System;
using System.Windows.Forms;

using SOA.Interface;

namespace SOA.Graphic
{
    public class MsgBox : IMsgBox
    {
        public void Init()
        {

        }

        public DialogResult Show(string text, string caption = "")
        {
            return MessageBox.Show(text, caption);
        }

        public DialogResult ShowOK(string text, string caption = "")
        {
            return Show(text, caption);
        }

        public DialogResult ShowOKCancel(string text, string caption = "")
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.OKCancel);
        }

        public DialogResult ShowYesNo(string text, string cpation = "")
        {
            return MessageBox.Show(text, cpation, MessageBoxButtons.YesNo);
        }

        public DialogResult ShowYesNoCancel(string text, string cpation = "")
        {
            return MessageBox.Show(text, cpation, MessageBoxButtons.YesNoCancel);
        }
    }
}
