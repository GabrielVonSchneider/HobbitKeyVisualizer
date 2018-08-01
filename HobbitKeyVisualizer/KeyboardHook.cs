using System;
using System.Runtime.InteropServices;
using HobbitKeyVisualizer.WinApi;

namespace HobbitKeyVisualizer
{
    internal class KeyboardHook
    {
        private const int lowLevelKeyboardHook = 13;
        private IntPtr? hook;

        private delegate long LowLevelKeyboardProc(
            [In] int nCode,
            [In] UIntPtr wParam,
            [In] long lParam);

        public void InstallHook()
        {
            if(this.IsInstalled)
            {
                throw new InvalidOperationException(
                    "Tried to set another hook when one was set already.");
            }

            var hook = User32.SetWindowsHookEx(
                lowLevelKeyboardHook,
                new LowLevelKeyboardProc(this.Listener),
                Kernel32.GetModuleHandle(null));

            if(hook == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            this.hook = hook;
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
            if(!(this.hook is IntPtr hook))
            {
                throw new InvalidOperationException(
                    "Tried to release hook when none was set.");
            }
            if (!User32.UnhookWindowsHookEx(hook))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }
        }

        public bool IsInstalled => this.hook is IntPtr;
    }
}