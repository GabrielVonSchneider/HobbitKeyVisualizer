using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using HokeyViz.WinApi;

namespace HokeyViz
{
    internal class KeyboardHook : Hook
    {
        private IntPtr? hook;
        private readonly Dictionary<
            WindowsMessage,
            Action<VirtualKeyCode>
        > actions;
        

        private delegate long LowLevelKeyboardProc(
            [In] int nCode,
            [In] WindowsMessage wParam,
            [In] KbdLlHookStruct lParam);


        public event Action<VirtualKeyCode> KeyDown;
        public event Action<VirtualKeyCode> KeyUp;

        public KeyboardHook(SynchronizationContext sync) : base(
            WindowsHook.LowLevelKeyboardHook,
            sync)
        {
            this.actions = new Dictionary<
                WindowsMessage,
                Action<VirtualKeyCode>>
            {
                {WindowsMessage.KeyDown, vkc => this.KeyDown?.Invoke(vkc)},
                {WindowsMessage.KeyUp, vkc => this.KeyUp?.Invoke(vkc)},
                {WindowsMessage.SysKeyDown, vkc => this.KeyUp?.Invoke(vkc)},
                {WindowsMessage.SysKeyUp, vkc => this.KeyUp?.Invoke(vkc)}
            };
        }

        private long Listener(
            int nCode,
            WindowsMessage wParam,
            KbdLlHookStruct lParam)
        {
            if (this.actions.TryGetValue(wParam, out var action))
            {
                this.sync.Post(state => action(lParam.vkCode), null);
            }

            return User32.CallNextHookEx(
                nCode: nCode,
                wParam: wParam,
                lParam: lParam);
        }

        protected override Delegate GetHandler()
        {
            return new LowLevelKeyboardProc(this.Listener);
        }
    }
}