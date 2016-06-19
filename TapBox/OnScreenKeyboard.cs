namespace TapBox
{
    using System.Diagnostics;

    public static class OnScreenKeyboard
    {
        private static ProcessInfo _processInfo = ProcessInfo.Create(@"C:\Program Files\Common Files\Microsoft Shared\Ink\TabTip.exe");

        public static string TouchKeyboardPath
        {
            get { return _processInfo?.ProcessStartInfo.FileName; }
            set { _processInfo = ProcessInfo.Create(value); }
        }

        public static void Show()
        {
            if (_processInfo == null)
            {
                return;
            }

            Debug.WriteLine("show");
            Process.Start(_processInfo.ProcessStartInfo);
        }

        public static void Hide()
        {
            if (_processInfo == null)
            {
                return;
            }

            Debug.WriteLine("hide");
            foreach (var process in Process.GetProcessesByName(_processInfo.ProcessName))
            {
                process.Kill();
            }
        }

        private class ProcessInfo
        {
            private ProcessInfo(string path)
            {
                ProcessStartInfo = new ProcessStartInfo(path);
                ProcessName = System.IO.Path.GetFileNameWithoutExtension(path);
            }

            public ProcessStartInfo ProcessStartInfo { get; }

            public string ProcessName { get; }

            public static ProcessInfo Create(string path)
            {
                if (path == null)
                {
                    return null;
                }

                if (System.IO.File.Exists(path))
                {
                    return new ProcessInfo(path);
                }

                return null;
            }

        }
    }
}