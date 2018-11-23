using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

using SOA.Interface;
using SOA.Helper;

namespace SOA.Util
{
    public sealed partial class Program : IProgram
    {
        public void Init()
        {

        }

        public void Close(string processName)
        {
            Console.WriteLine(1);
            Process[] processes = Process.GetProcessesByName(processName);

            if(processes.Length == 0)
            {
                Console.WriteLine(string.Format("{0}은 현재 실행중인 프로그램이 아닙니다.", processName));
                return;
            }

            foreach(Process process in processes)
            {
                try
                {
                    CloseWindow(process);
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Program.Close(string processName) Exception:");
                    Debug.WriteLine(e.Message);
                }
            }
        }

        private void CloseWindow(Process process)
        {
            Debug.WriteLine(process.ProcessName);
            foreach (ProcessThread thread in process.Threads)
            {
                EnumThreadWindows(thread.Id, (hWnd, lParam)=> {

                    EnumChildWindows(hWnd, (hChildWnd, lChileParam) => {
                        SendHelper.instance.CloseMessage((IntPtr)hChildWnd);
                        return true;

                    }, IntPtr.Zero);

                    SendHelper.instance.CloseMessage((IntPtr)hWnd);
                    return true;

                }, IntPtr.Zero);
            }

            process.WaitForExit(1000);
            process.Kill();
        }
    }

    public sealed partial class Program : IProgram
    {
        private delegate bool EnumWindowsDelegate(uint hWnd, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumWindowsDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(uint window, EnumWindowsDelegate callback, IntPtr lParam);
    }
}
