using System;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using static PInvoke.User32;
using static PInvoke.Kernel32;

namespace SOA.Helper
{
    public sealed class InputHelper : Singleton<InputHelper>
    {
        public InputHelper Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);

            return this;
        }

        public InputHelper Sleep(TimeSpan timeout)
        {
            Thread.Sleep(timeout);

            return this;
        }

        public void SendText(string text)
        {
            int length = text.Length;

            if (length > UInt32.MaxValue / 2)
            {
                return;
            }

            List<INPUT> inputList = new List<INPUT>();

            char ch;
     
            for(int i = 0; i < length; ++i)
            {
                ch = text[i];
                inputList.Add(GetKeyboardDownInput(ch));
                inputList.Add(GetKeyboardUpInput(ch));
            }

            Send(inputList);
        }

        private INPUT GetKeyboardDownInput(char character)
        {
            ScanCode scanCode = (ScanCode)character;

            INPUT keyInput = new INPUT
            {
                type = InputType.INPUT_KEYBOARD,
                Inputs =
                {
                    ki = new KEYBDINPUT
                    {
                        wScan = scanCode,
                        wVk = 0,
                        dwFlags = KEYEVENTF.KEYEVENTF_UNICODE,
                        time = 0,
                        dwExtraInfo_IntPtr = IntPtr.Zero
                    }
                }
            };

            if (((int)scanCode & 0xFF00) == 0xE000)
            {
                keyInput.Inputs.ki.dwFlags |= KEYEVENTF.KEYEVENTF_EXTENDED_KEY;
            }

            return keyInput;
        }

        private INPUT GetKeyboardUpInput(char character)
        {
            INPUT keyInput = GetKeyboardDownInput(character);

            keyInput.Inputs.ki.dwFlags |= KEYEVENTF.KEYEVENTF_KEYUP;

            return keyInput;
        }

        private void Send(List<INPUT> inputList)
        {
            INPUT[] inputArray = inputList.ToArray();

            SendInput(inputArray.Length, inputArray, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
