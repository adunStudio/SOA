using System.Runtime.InteropServices;
using System.Windows.Forms;

using SOA.Helper;
using static PInvoke.User32;

namespace SOA.Input
{
    public class KeyEventInformation : KeyEventArgs
    {
        public int ScanCode { get; }
        public int Timestamp { get; }
        public bool IsDown { get; }
        public bool IsUp { get; }
        public bool IsExtended { get; }

        public static KeyEventInformation Get(HookData hookData)
        {
            WindowMessage message = (WindowMessage)hookData.wParam;

            KEYBDINPUT keyboardStruct = Marshal.PtrToStructure<KEYBDINPUT>(hookData.lParam);

            int scanCode = (int)keyboardStruct.wScan;

            int timestamp = (int)keyboardStruct.time;

            bool isKeyDown = message == WindowMessage.WM_KEYDOWN || message == WindowMessage.WM_KEYDOWN;
            bool isKeyUp = message == WindowMessage.WM_KEYUP || message == WindowMessage.WM_KEYUP;

            // If specified, the scan code was preceded by a prefix byte that has the value
            // 0xE0 (224).
            const uint maskExtendedKey = 0x1;
            bool isExtendedKey = ((uint)keyboardStruct.dwFlags & maskExtendedKey) > 0;

            Keys keyData = AppendModifierStates((Keys)keyboardStruct.wVk);

            return new KeyEventInformation(keyData, scanCode, timestamp, isKeyDown, isKeyUp, isExtendedKey);
        }

        private KeyEventInformation(Keys keyData, int scanCode, int timestamp, bool isKeyDown, bool isKeyUp, bool isExtendedKey) : base(keyData)
        {
            ScanCode = scanCode;
            Timestamp = timestamp;
            IsDown = isKeyDown;
            IsUp = isKeyUp;
            IsExtended = isExtendedKey;
        }

        private static bool IsKeyPressed(int vk)
        {
            // GetKeyState(vk): vk의 상태를 가져온다.
            // 상태는 키의 up, down, toggled
            // high-order(left) bit가 1이면 down 0이면 up
            // low-order(right) bit가 1이면 toggled

            // 0x8000 : 현재 키가 눌려진 상태
            // 0x0001 : 지난번 호출과 이번 호출 사이에 키가 눌려진 적이 있었다. 를 의미
            return (GetKeyState(vk) & 0x8000) > 0;
        }

        private static Keys AppendModifierStates(Keys key)
        {
            bool control = IsKeyPressed((int)VirtualKey.VK_CONTROL);
            bool shift   = IsKeyPressed((int)VirtualKey.VK_SHIFT);
            bool alt     = IsKeyPressed((int)VirtualKey.VK_MENU);

            return key |
                  (control ? Keys.Control : Keys.None) |
                  (shift ? Keys.Shift : Keys.None) |
                  (alt ? Keys.Alt : Keys.None);
        }
    }
}
