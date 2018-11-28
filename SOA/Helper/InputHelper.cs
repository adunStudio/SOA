using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using static PInvoke.User32;

namespace SOA.Helper
{
    public sealed class InputHelper : Singleton<InputHelper>
    {
        public void SendText(string text)
        {
            int length = text.Length;

            if (length > UInt32.MaxValue / 2)
            {
                return;
            }

            List<INPUT> inputList = new List<INPUT>();
     
            foreach(char ch in text)
            {
                inputList.Add(GetKeyboardDownInput(ch));
                inputList.Add(GetKeyboardUpInput(ch));
            }

            Send(inputList);
        }

        public void SendComboText(string text)
        {
            int length = text.Length;

            if (length > UInt32.MaxValue / 2)
            {
                return;
            }

            List<INPUT> inputList = new List<INPUT>();

            foreach (char ch in text)
            {
                inputList.Add(GetKeyboardDownInput(ch));
            }

            foreach (char ch in text)
            {
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

        public void SendKeys(VirtualKey[] virtualKeyArray)
        {
            int length = virtualKeyArray.Length;

            if (length > UInt32.MaxValue / 2)
            {
                return;
            }

            List<INPUT> inputList = new List<INPUT>();

            foreach(VirtualKey key in virtualKeyArray)
            {
                inputList.Add(GetKeyboardDownInput(key));
                inputList.Add(GetKeyboardUpInput(key));
            }

            Send(inputList);
        }

        public void SendComboKeys(VirtualKey[] virtualKeyArray)
        {
            int length = virtualKeyArray.Length;

            if (length > UInt32.MaxValue / 2)
            {
                return;
            }

            List<INPUT> inputList = new List<INPUT>();

            foreach (VirtualKey key in virtualKeyArray)
            {
                inputList.Add(GetKeyboardDownInput(key));
            }

            foreach (VirtualKey key in virtualKeyArray)
            {
                inputList.Add(GetKeyboardUpInput(key));
            }

            Send(inputList);
        }

        private INPUT GetKeyboardDownInput(VirtualKey virtualKey)
        {
            INPUT keyInput = new INPUT
            {
                type = InputType.INPUT_KEYBOARD,
                Inputs =
                {
                    ki = new KEYBDINPUT
                    {
                        wScan = 0,
                        wVk = virtualKey,
                        dwFlags = IsExtendedkey(virtualKey) ? KEYEVENTF.KEYEVENTF_EXTENDED_KEY : 0,
                        time = 0,
                        dwExtraInfo_IntPtr = IntPtr.Zero
                    }
                }
            };

            return keyInput;
        }

        private INPUT GetKeyboardUpInput(VirtualKey virtualKey)
        {
            INPUT keyInput = GetKeyboardDownInput(virtualKey);

            keyInput.Inputs.ki.dwFlags |= KEYEVENTF.KEYEVENTF_KEYUP;

            return keyInput;
        }

        private static bool IsExtendedkey(VirtualKey keyCode)
        {
            if (keyCode == VirtualKey.VK_MENU ||
                keyCode == VirtualKey.VK_LMENU ||
                keyCode == VirtualKey.VK_RMENU ||
                keyCode == VirtualKey.VK_CONTROL ||
                keyCode == VirtualKey.VK_RCONTROL ||
                keyCode == VirtualKey.VK_INSERT ||
                keyCode == VirtualKey.VK_DELETE ||
                keyCode == VirtualKey.VK_HOME ||
                keyCode == VirtualKey.VK_END ||
                keyCode == VirtualKey.VK_PRIOR ||
                keyCode == VirtualKey.VK_NEXT ||
                keyCode == VirtualKey.VK_RIGHT ||
                keyCode == VirtualKey.VK_UP ||
                keyCode == VirtualKey.VK_LEFT ||
                keyCode == VirtualKey.VK_DOWN ||
                keyCode == VirtualKey.VK_NUMLOCK ||
                keyCode == VirtualKey.VK_CANCEL ||
                keyCode == VirtualKey.VK_SNAPSHOT ||
                keyCode == VirtualKey.VK_DIVIDE)
            {
                return true;
            }

            return false;
        }

        private void Send(List<INPUT> inputList)
        {
            INPUT[] inputArray = inputList.ToArray();

            SendInput(inputArray.Length, inputArray, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
