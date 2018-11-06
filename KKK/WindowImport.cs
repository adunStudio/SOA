using System;
using System.Text;
using System.Runtime.InteropServices;

namespace KKK
{
    static class WindowImport
    {
        public delegate bool EnumWindowCallback(int hwnd, int lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowCallback callback, int y);

        [DllImport("user32.dll")]
        public static extern int GetParent(int hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern long GetWindowLong(int hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetClassLong(IntPtr hwnd, int nIndex);

        public const int GCL_HICON = -14; //GetWindowLong을 호출할 때 쓸 인자
        public const int GCL_HMODULE = -16;
    }
}
