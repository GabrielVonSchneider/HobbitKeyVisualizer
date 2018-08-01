using System;
using System.Runtime.InteropServices;
using HobbitKeyVisualizer.WinApi;

namespace HobbitKeyVisualizer
{
    internal class KeyboardHook
    {
        private const int lowLevelKeyboardHook = 13;
        private IntPtr hook;

        private delegate long LowLevelKeyboardProc(
            [In] int nCode,
            [In] UIntPtr wParam,
            [In] long lParam);

        public void InstallHook()
        {
            this.hook = User32.SetWindowsHookEx(
                lowLevelKeyboardHook,
                new LowLevelKeyboardProc(this.Listener),
                Kernel32.GetModuleHandle(null));
        }

        private long Listener(
            int nCode,
            UIntPtr wParam,
            long lParam)
        {
            throw new NotImplementedException();
        }

        public void ReleaseHook()
        {
            User32.UnhookWindowsHookEx(this.hook);
        }
    }
}