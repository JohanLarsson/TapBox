using System.Diagnostics;

namespace TapBox
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public static class TouchKeyboard
    {
        private static TouchProcessInfo _touchProcessInfo = TouchProcessInfo.Default;

        public static string TouchKeyboardPath
        {
            get { return _touchProcessInfo?.ProcessStartInfo.FileName; }
            set { _touchProcessInfo = TouchProcessInfo.Create(value); }
        }

        public static void Show()
        {
            if (_touchProcessInfo?.ProcessStartInfo == null)
            {
                return;
            }

            Debug.WriteLine("show");
            Process.Start(_touchProcessInfo.ProcessStartInfo);
        }

        public static void Hide()
        {
            if (_touchProcessInfo?.ProcessStartInfo == null)
            {
                return;
            }

            // http://mheironimus.blogspot.se/2015/05/adding-touch-keyboard-support-to-wpf.html
            var keyboardWnd = FindWindow("IPTip_Main_Window", null);
            var nullIntPtr = new IntPtr(0);
            const uint wmSyscommand = 0x0112;
            var scClose = new IntPtr(0xF060);

            if (keyboardWnd != nullIntPtr)
            {
                Debug.WriteLine("hide");
                SendMessage(keyboardWnd, wmSyscommand, scClose, nullIntPtr);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string sClassName, string sAppName);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        private static bool HasTouchInput()
        {
            return Tablet.TabletDevices.Cast<TabletDevice>().Any(tabletDevice => tabletDevice.Type == TabletDeviceType.Touch);
        }
    }
}