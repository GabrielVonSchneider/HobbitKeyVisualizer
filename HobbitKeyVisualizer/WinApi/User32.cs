using System;
using System.Runtime.InteropServices;

namespace HobbitKeyVisualizer.WinApi
{
    internal class User32
    {
        private const string user32 = "user32.dll";

        [DllImport(user32)]
        public static extern int SetWindowsHookEx(
            [In] int idHook,
            [In] Delegate lpfn,
            [In] IntPtr hMod,
            [In] uint dwThreaedId = 0);
    }
}
