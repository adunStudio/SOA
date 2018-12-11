using System;
using System.Windows.Forms;

namespace SOA.Interface
{
    public interface IMsgBox : IInit
    {
        DialogResult Show(string text, string caption = "");
        DialogResult ShowOK(string text, string caption = "");
        DialogResult ShowOKCancel(string text, string caption = "");
        DialogResult ShowYesNo(string text, string cpation = "");
        DialogResult ShowYesNoCancel(string text, string cpation = "");
    }
}
