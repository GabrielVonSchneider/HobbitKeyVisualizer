using System;
using System.Runtime.InteropServices;

namespace HobbitKeyVisualizer.WinApi
{
    internal class User32
    {
        private const string user32 = "user32.dll";

        [DllImport(user32)]
        public static extern IntPtr SetWindowsHookEx(
            [In] int idHook,
            [In] Delegate lpfn,
            [In] IntPtr hMod,
            [In] uint dwThreaedId = 0
        );

        [DllImport(user32)]
        public static extern bool UnhookWindowsHookEx(
            [In] IntPtr hhk
        );

        [DllImport(user32)]
        public static extern long CallNextHookEx(
            [In] [Optional] IntPtr hhk,
            [In] int nCode,
            [In] WindowsMessage wParam,
            [In] KbDllHookStruct lParam
        );
    }
}