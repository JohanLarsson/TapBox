namespace TapBox
{
    using System;
    using System.Diagnostics;
    using System.IO;

    internal class TouchProcessInfo
    {
        private TouchProcessInfo(string path)
        {
            ProcessStartInfo = new ProcessStartInfo(path);
            ProcessName = Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// Default is C:\Program Files\Common Files\Microsoft Shared\Ink\TabTip.exe
        /// </summary>
        public static TouchProcessInfo Default { get; } = CreateDefault();

        public ProcessStartInfo ProcessStartInfo { get; }

        public string ProcessName { get; }

        public static TouchProcessInfo Create(string path)
        {
            if (path == null || Path.GetExtension(path) != ".exe")
            {
                return null;
            }

            if (File.Exists(path))
            {
                return new TouchProcessInfo(path);
            }

            return null;
        }

        private static TouchProcessInfo CreateDefault()
        {
            const string microsoftSharedInkTabtipExe = @"Microsoft Shared\ink\TabTip.exe";

            return
                Create(@"C:\Program Files\Common Files\Microsoft Shared\Ink\TabTip.exe") ??
                Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles),
                    microsoftSharedInkTabtipExe)) ??
                Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    microsoftSharedInkTabtipExe));
        }
    }
}