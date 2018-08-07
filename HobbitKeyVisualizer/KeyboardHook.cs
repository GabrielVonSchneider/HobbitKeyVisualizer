using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using HobbitKeyVisualizer.WinApi;

namespace HobbitKeyVisualizer
{
    internal class KeyboardHook
    {
        private IntPtr? hook;
        private readonly Dictionary<
            WindowsMessage,
            Action<VirtualKeyCode>
        > actions;
        
        private const int LowLevelKeyboardHook = 13;

        private delegate long LowLevelKeyboardProc(
            [In] int nCode,
            [In] WindowsMessage wParam,
            [In] KbDllHookStruct lParam);

        private readonly SynchronizationContext sync;

        public event Action<VirtualKeyCode> KeyDown;
        public event Action<VirtualKeyCode> KeyUp;

        public KeyboardHook(SynchronizationContext sync)
        {
            this.sync = sync;
            this.actions = new Dictionary<
                WindowsMessage,
                Action<VirtualKeyCode>>
            {
                {WindowsMessage.KeyDown, this.KeyDown},
                {WindowsMessage.KeyUp, this.KeyUp},
                {WindowsMessage.SysKeyDown, this.KeyDown},
                {WindowsMessage.SysKeyUp, this.KeyUp}
            };
        }

        public void InstallHook()
        {
            if (this.IsInstalled)
            {
                throw new InvalidOperationException(
                    "Tried to set another hook when one was set already.");
            }

            var hook = User32.SetWindowsHookEx(
                LowLevelKeyboardHook,
                new LowLevelKeyboardProc(this.Listener),
                Kernel32.GetModuleHandle(null));

            if (hook == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            this.hook = hook;
        }

        private long Listener(
            int nCode,
            WindowsMessage wParam,
            KbDllHookStruct lParam)
        {
            if (this.actions.TryGetValue(wParam, out var action))
            {
                this.sync.Post(state => action?.Invoke(lParam.vkCode), null);
            }

            return User32.CallNextHookEx(
                nCode: nCode,
                wParam: wParam,
                lParam: lParam);
        }

        public void ReleaseHook()
        {
            if (!(this.hook is IntPtr hook))
            {
                throw new InvalidOperationException(
                    "Tried to release hook when none was set.");
            }

            if (!User32.UnhookWindowsHookEx(hook))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }
        }

        public bool IsInstalled => this.hook != null;
    }
}