using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using static PInvoke.User32;

namespace SOA.Helper
{
    public sealed class InputHelper : Singleton<InputHelper>
    {
        private readonly int m_System_CoordinateX;
        private readonly int m_System_CoordinateY;

        private const int m_MappingValue = 65536;

        public InputHelper()
        {
            m_System_CoordinateX = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            m_System_CoordinateY = GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }
        /* 키보드 */
        #region 키보드

        #region String
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
        #endregion

        #region VirtualKey
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

        private bool IsExtendedkey(VirtualKey keyCode)
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
        #endregion

        #endregion

        /* 마우스 */
        public void SendButtonClick(MouseButtons button)
        {
            List<INPUT> inputList = new List<INPUT>();

            inputList.Add(GetMouseButtonDownInput(button));
            inputList.Add(GetMouseButtonUpInput(button));

            Send(inputList);
        }

        public void SendButtonDoubleClick(MouseButtons button)
        {
            List<INPUT> inputList = new List<INPUT>();

            inputList.Add(GetMouseButtonDownInput(button));
            inputList.Add(GetMouseButtonUpInput(button));

            inputList.Add(GetMouseButtonDownInput(button));
            inputList.Add(GetMouseButtonUpInput(button));

            Send(inputList);
        }

        private INPUT GetMouseButtonDownInput(MouseButtons button)
        {
            MOUSEEVENTF buttonFlag = MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN;

            switch (button)
            {
                case MouseButtons.Left:
                    buttonFlag = MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN; break;
                case MouseButtons.Right:
                    buttonFlag = MOUSEEVENTF.MOUSEEVENTF_RIGHTDOWN; break;
                case MouseButtons.Middle:
                    buttonFlag = MOUSEEVENTF.MOUSEEVENTF_MIDDLEDOWN; break;
            }

            INPUT buttonInput = new INPUT
            {
                type = InputType.INPUT_MOUSE,
            };

            buttonInput.Inputs.mi.dwFlags = buttonFlag;

            return buttonInput;
        }

        private INPUT GetMouseButtonUpInput(MouseButtons button)
        {
            MOUSEEVENTF buttonFlag = MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN;

            switch (button)
            {
                case MouseButtons.Left:
                    buttonFlag = MOUSEEVENTF.MOUSEEVENTF_LEFTUP; break;
                case MouseButtons.Right:
                    buttonFlag = MOUSEEVENTF.MOUSEEVENTF_RIGHTUP; break;
                case MouseButtons.Middle:
                    buttonFlag = MOUSEEVENTF.MOUSEEVENTF_MIDDLEUP; break;
            }

            INPUT buttonInput = new INPUT
            {
                type = InputType.INPUT_MOUSE,
            };

            buttonInput.Inputs.mi.dwFlags = buttonFlag;

            return buttonInput;
        }

        public void SendMovementAbsolute(int x, int y)
        {
            List<INPUT> inputList = new List<INPUT>();

            inputList.Add(GetMouseMovementInput(x, y, isAbsolute: true));

            Send(inputList);
        }

        public void SendMovementRelative(int x, int y)
        {
            List<INPUT> inputList = new List<INPUT>();

            inputList.Add(GetMouseMovementInput(x, y, isAbsolute: false));

            Send(inputList);
        }

        private INPUT GetMouseMovementInput(int x, int y, bool isAbsolute)
        {
            MOUSEEVENTF moventFlag = MOUSEEVENTF.MOUSEEVENTF_MOVE;

            if(isAbsolute)
            {
                // MOUSEEVENTF_ABSOLUTE 값을 지정하면
                // 좌표가 (0, 0) ~ (65535, 65535)로 맵핑된다.
                x = (x * m_MappingValue) / m_System_CoordinateX + 1;
                y = (y * m_MappingValue) / m_System_CoordinateY + 1;

                moventFlag |= MOUSEEVENTF.MOUSEEVENTF_ABSOLUTE;
            }

            INPUT movementInput = new INPUT
            {
                type = InputType.INPUT_MOUSE,
            };

            movementInput.Inputs.mi.dwFlags = moventFlag;
            movementInput.Inputs.mi.dx = x;
            movementInput.Inputs.mi.dy = y;

            return movementInput;
        }


        /* Send */
        private void Send(List<INPUT> inputList)
        {
            INPUT[] inputArray = inputList.ToArray();

            SendInput(inputArray.Length, inputArray, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
