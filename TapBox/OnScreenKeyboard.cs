namespace TapBox
{
    using System.Diagnostics;

    public static class OnScreenKeyboard
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
            if (_touchProcessInfo == null)
            {
                return;
            }

            Debug.WriteLine("hide");
            foreach (var process in Process.GetProcessesByName(_touchProcessInfo.ProcessName))
            {
                process.Kill();
            }
        }
    }
}