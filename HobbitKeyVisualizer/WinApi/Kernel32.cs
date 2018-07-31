using System;
using System.Runtime.InteropServices;

namespace HobbitKeyVisualizer.WinApi
{
    internal static class Kernel32
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
