using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SOA.WindowAPI
{
    using HANDLE = IntPtr;
    using HINSTANCE = IntPtr;
    using LRESULT = IntPtr;
    using HMODULE = IntPtr;

    static partial class Win32
    {
        public delegate bool EnumWindowCallback(HANDLE hwnd, int lParam);

        public delegate LRESULT HookProc(int code, IntPtr wParam, IntPtr lParam);

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

        #region WH_ : HookType
        public const int WH_JOURNALRECORD = 0;
        public const int WH_JOURNALPLAYBACK = 1;
        public const int WH_KEYBOARD = 2;
        public const int WH_GETMESSAGE = 3;
        public const int WH_CALLWNDPROC = 4;
        public const int WH_CBT = 5;
        public const int WH_SYSMSGFILTER = 6;
        public const int WH_MOUSE = 7;
        public const int WH_HARDWARE = 8;
        public const int WH_DEBUG = 9;
        public const int WH_SHELL = 10;
        public const int WH_FOREGROUNDIDLE = 11;
        public const int WH_CALLWNDPROCRET = 12;
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;
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
        public static extern HANDLE GetConsoleWindow();
    }
    #endregion

    #region 윈도우
    static partial class Win32
    { 
        /// <summary>
        /// 지정된 윈도우의 show 상태를 설정한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(HANDLE hWnd, uint nCmdShow);

        /// <summary>
        /// 핸들을 각 창에 차례로 응용 프로그램 정의 콜백 함수로 전달하여 화면의 모든 최상위 창을 열거한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern HANDLE EnumWindows(EnumWindowCallback callback, int lparam);

        /// <summary>
        /// 클래스 이름과 창 이름이 지정된 문자열과 일치하는 최상위 창에 대한 핸들을 검색한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern HANDLE FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 지정된 윈도우의 부모 또는 소유자에 대한 핸들을 검색한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern HANDLE GetParent(HANDLE hWnd);

        /// <summary>
        /// 지정된 윈도우의 제목 표시 줄(있는 경우)의 텍스트를 버퍼에 복사한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetWindowText(HANDLE hWnd, StringBuilder text, int count);

        /// <summary>
        /// 지정된 윈도우가 속한 클래스의 이름을 버퍼에 복사한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetClassName(HANDLE hWnd, StringBuilder text, int count);

        /// <summary>
        /// 지정된 윈도우의 정보를 가져온다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern long GetWindowLong(HANDLE hWnd, int nIndex);

        /// <summary>
        /// 지정된 윈도우와 연관된 WNDCLASSEX 구조체에서 지정된 32 비트(DWORD) 값을 검색한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetClassLong(HANDLE hwnd, uint nIndex);

        /// <summary>
        /// 지정된 윈도우의 상태와 위치를 검색(restored, minimized, maximized)
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(HANDLE hWnd, ref WINDOWPLACEMENT lpwndpl);

        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;

        // The SendMessage function sends a Win32 message to the specified handle, it takes three
        // ints as parameters, the message to send, and to optional parameters (pass 0 if not required).
        [DllImport("user32.dll")]
        public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, int lParam);

        // An overload of the SendMessage function, this time taking in a StringBuilder as the lParam.
        // Through the series we'll use a lot of different SendMessage overloads as SendMessage is one
        // of the most fundamental Win32 functions.
        [DllImport("user32.dll")]
        public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);


        /// <summary>
        /// 시스템 전체 단축키를 정의한다. (핫키)
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(HANDLE hwnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// 이전 호출 스레드에서 등록한 핫키를 해제한다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(HANDLE hWnd, int id);

        [DllImport("user32.dll")]
        public static extern int SetWindowsHookEx(int hookType, HookProc func, HMODULE hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        public static extern LRESULT CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(int hhk);
    }
    #endregion

    #region 커널
    static partial class Win32
    {
        /// <summary>
        /// 문자열을 전역 atom 테이블에 추가하고, 문자열을 식별하는 고유 한 값을 반환한다.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern ushort GlobalAddAtom(string lpString);

        /// <summary>
        /// 전역 문자열 atom의 참조 횟수를 감소시킨다. 참조 계수가 0에 도달하면 전역 atom 테이블에서 atom과 관련된 문자열을 제거한다.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern ushort GlobalDeleteAtom(ushort nAtom);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern HMODULE GetModuleHandle(string lpModuleName);
    }
    #endregion
}
