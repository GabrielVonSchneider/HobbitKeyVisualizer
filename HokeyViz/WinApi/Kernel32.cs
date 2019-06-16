using System;
using System.Runtime.InteropServices;

namespace HokeyViz.WinApi
{
    internal static class Kernel32
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(
            [In] [Optional] string lpModuleName);
    }
}
