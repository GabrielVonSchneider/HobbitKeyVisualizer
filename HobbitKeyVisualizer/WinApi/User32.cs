using System;
using System.Runtime.InteropServices;

namespace HobbitKeyVisualizer.WinApi
{
    internal class User32
    {
        private const string user32 = "user32.dll";

        [DllImport(user32)]
        public static extern IntPtr SetWindowsHookEx(
            [In] WindowsHook idHook,
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
            [In] [Optional] IntPtr hHk,
            [In] int nCode,
            [In] WindowsMessage wParam,
            [In] KbdLlHookStruct lParam
        );

        [DllImport(user32)]
        public static extern long CallNextHookEx(
            [In] [Optional] IntPtr hHk,
            [In] HookCode nCode,
            [In] WindowsMessage wParam,
            [In] MsLlHookStruct lParam
        );
    }
}