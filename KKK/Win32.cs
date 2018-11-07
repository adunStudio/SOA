using System;
using System.Text;
using System.Runtime.InteropServices;

namespace KKK.WindowAPI
{
    using HWND = IntPtr;

    static partial class Win32
    {
        public delegate bool EnumWindowCallback(HWND hwnd, int lParam);

        #region WINDOWPLACEMENT struct
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }
        #endregion
    }

    #region ENUM & FLAG
    static partial class Win32
    {
        #region GWL_ : GetWindowLong
        public const int GWL_WNDPROC    = -4;
        public const int GWL_HINSTANCE  = -6;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_ID         = -12;
        public const int GWL_STYLE      = -16;
        public const int GWL_EXSTYLE    = -20;
        public const int GWL_USERDATA   = -21;
        #endregion

        #region WS_ : WindowStyels
        public const uint WS_BORDER = 0x800000;
        public const uint WS_CAPTION = 0xc00000;
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_CLIPCHILDREN = 0x2000000;
        public const uint WS_CLIPSIBLINGS = 0x4000000;
        public const uint WS_DISABLED = 0x8000000;
        public const uint WS_DLGFRAME = 0x400000;
        public const uint WS_GROUP = 0x20000;
        public const uint WS_HSCROLL = 0x100000;
        public const uint WS_MAXIMIZE = 0x1000000;
        public const uint WS_MAXIMIZEBOX = 0x10000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_MINIMIZEBOX = 0x20000;
        public const uint WS_OVERLAPPED = 0x0;
        public const uint WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
        public const uint WS_POPUP = 0x80000000u;
        public const uint WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU;
        public const uint WS_SIZEFRAME = 0x40000;
        public const uint WS_SYSMENU = 0x80000;
        public const uint WS_TABSTOP = 0x10000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_VSCROLL = 0x200000;
        #endregion

        #region SW_ : ShowState
        public const uint SW_HIDE = 0;
        public const uint SW_SHOWNORMAL = 1;
        public const uint SW_NORMAL = 1;
        public const uint SW_SHOWMINIMIZED = 2;
        public const uint SW_SHOWMAXIMIZED = 3;
        public const uint SW_MAXIMIZE = 3;
        public const uint SW_SHOWNOACTIVATE = 4;
        public const uint SW_SHOW = 5;
        public const uint SW_MINIMIZE = 6;
        public const uint SW_SHOWMINNOACTIVE = 7;
        public const uint SW_SHOWNA = 8;
        public const uint SW_RESTORE = 9;
        public const uint SW_SHOWDEFAULT = 10;
        public const uint SW_FORCEMINIMIZE = 11;
        public const uint SW_MAX = 11;
        #endregion

        #region GCL, GCLP, GCW : GetClass.. 
        public const int GCLP_MENUNAME = -8;
        public const int GCLP_HBRBACKGROUND = -10;
        public const int GCLP_HCURSOR = -12;
        public const int GCLP_HICON = -14;
        public const int GCLP_HMODULE = -16;
        public const int GCL_CBWNDEXTRA = -18;
        public const int GCL_CBCLSEXTRA = -20;
        public const int GCLP_WNDPROC = -24;
        public const int GCL_STYLE = -26;
        public const int GCLP_HICONSM = -34;
        public const int GCW_ATOM = -32;
        #endregion
    }
    #endregion


    #region 콘솔
    static partial class Win32
    {
        /// <summary>
        /// 호출 한 프로세스와 연결된 콘솔창 핸들을 반환한다.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern HWND GetConsoleWindow();
    }
    #endregion

    #region 윈도우
    static partial class Win32
    { 
        /// <summary>
        /// 지정된 윈도우의 show 상태를 설정한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(HWND hWnd, uint nCmdShow);

        /// <summary>
        /// 핸들을 각 창에 차례로 응용 프로그램 정의 콜백 함수로 전달하여 화면의 모든 최상위 창을 열거한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern HWND EnumWindows(EnumWindowCallback callback, int lparam);

        /// <summary>
        /// 클래스 이름과 창 이름이 지정된 문자열과 일치하는 최상위 창에 대한 핸들을 검색한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern HWND FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 지정된 윈도우의 부모 또는 소유자에 대한 핸들을 검색한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern HWND GetParent(HWND hWnd);

        /// <summary>
        /// 지정된 윈도우의 제목 표시 줄(있는 경우)의 텍스트를 버퍼에 복사한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetWindowText(HWND hWnd, StringBuilder text, int count);

        /// <summary>
        /// 지정된 윈도우가 속한 클래스의 이름을 버퍼에 복사한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetClassName(HWND hWnd, StringBuilder text, int count);

        /// <summary>
        /// 지정된 윈도우의 정보를 가져온다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern long GetWindowLong(HWND hWnd, int nIndex);

        /// <summary>
        /// 지정된 윈도우와 연관된 WNDCLASSEX 구조체에서 지정된 32 비트(DWORD) 값을 검색한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetClassLong(HWND hwnd, uint nIndex);

        /// <summary>
        /// 지정된 윈도우의 상태와 위치를 검색(restored, minimized, maximized)
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(HWND hWnd, ref WINDOWPLACEMENT lpwndpl);

        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;

        // The SendMessage function sends a Win32 message to the specified handle, it takes three
        // ints as parameters, the message to send, and to optional parameters (pass 0 if not required).
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, int lParam);

        // An overload of the SendMessage function, this time taking in a StringBuilder as the lParam.
        // Through the series we'll use a lot of different SendMessage overloads as SendMessage is one
        // of the most fundamental Win32 functions.
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);
    }
    #endregion
}
