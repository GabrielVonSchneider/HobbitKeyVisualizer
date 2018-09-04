using System;
using System.Runtime.InteropServices;
using System.Threading;
using HobbitKeyVisualizer.WinApi;

namespace HobbitKeyVisualizer
{
    internal abstract class Hook
    {
        private IntPtr? hook;
        private WindowsHook hookType;
        protected readonly SynchronizationContext sync;

        protected abstract Delegate GetHandler();

        /// <summary>
        /// The delegate needs to be stored to avoid it being
        /// garbage collected.
        /// </summary>
        private readonly Lazy<Delegate> handler;

        protected Hook(WindowsHook hookType, SynchronizationContext sync)
        {
            this.hookType = hookType;
            this.sync = sync;
            this.handler = new Lazy<Delegate>(this.GetHandler);
        }

        public void Unhook()
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

            this.hook = null;
        }

        public void Set()
        {
            if (this.IsInstalled)
            {
                throw new InvalidOperationException(
                    "Tried to set another hook when one was set already.");
            }

            var hook = User32.SetWindowsHookEx(
                this.hookType,
                this.handler.Value,
                Kernel32.GetModuleHandle());

            if (hook == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            this.hook = hook;
        }

        public bool IsInstalled => this.hook != null;
    }
}
