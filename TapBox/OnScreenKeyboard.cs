namespace TapBox
{
    using System.Diagnostics;

    public static class OnScreenKeyboard
    {
        private const string TouchKeyboardPath = @"C:\Program Files\Common Files\Microsoft Shared\Ink\TabTip.exe";
        private static readonly string ProcessName = System.IO.Path.GetFileNameWithoutExtension(TouchKeyboardPath);
        private static readonly ProcessStartInfo ProcessStartInfo = new ProcessStartInfo(TouchKeyboardPath);

        public static void Show()
        {
            Debug.WriteLine("show");
            Process.Start(ProcessStartInfo);
        }

        public static void Hide()
        {
            Debug.WriteLine("hide");
            foreach (var process in Process.GetProcessesByName(ProcessName))
            {
                process.Kill();
            }
        }
    }
}