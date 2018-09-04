using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using HobbitKeyVisualizer.WinApi;

namespace HobbitKeyVisualizer
{
    internal class MouseHook : Hook
    {
        private delegate long LowLevelMouseProc(
            [In] HookCode nCode,
            [In] WindowsMessage wParam,
            [In] MsLlHookStruct lParam
        );

        public event Action M1Down;
        public event Action M2Down;
        public event Action M1Up;
        public event Action M2Up;

        private Dictionary<WindowsMessage, Action> actions;

        public MouseHook(SynchronizationContext sync) : base(
            WindowsHook.LowLevelMouseHook,
            sync)
        {
            this.actions = new Dictionary<
                WindowsMessage,
                Action>
            {
                {WindowsMessage.LButtonDown, () => this.M1Down?.Invoke()},
                {WindowsMessage.RButtonDown, () => this.M2Down?.Invoke()},
                {WindowsMessage.LButtonUp, () => this.M1Up?.Invoke()},
                {WindowsMessage.RButtonUp, () => this.M2Up?.Invoke()}
            };
        }

        private long Listener(
            HookCode nCode,
            WindowsMessage wParam,
            MsLlHookStruct lParam)
        {
            if (this.actions.TryGetValue(wParam, out var action))
            {
                this.sync.Post(state => action(), null);
            }

            return User32.CallNextHookEx(
                nCode: nCode,
                wParam: wParam,
                lParam: lParam);
        }

        protected override Delegate GetHandler()
        {
            return new LowLevelMouseProc(this.Listener);
        }
    }
}