using System.Collections.Generic;
using System.Threading;
using HobbitKeyVisualizer.Common;
using HobbitKeyVisualizer.WinApi;

namespace HobbitKeyVisualizer
{
    internal class KeyboardViewModel : PropertyChangedBase
    {
        private readonly KeyboardHook keyboardHook;
        private readonly Dictionary<VirtualKeyCode, KeyViewModel.Token> keys;

        public KeyViewModel AButton { get; }
        public KeyViewModel DButton { get; }
        public KeyViewModel SButton { get; }
        public KeyViewModel WButton { get; }
        public KeyViewModel SpaceBar { get; }
        public KeyViewModel M1 { get; }
        public KeyViewModel M2 { get; }


        public bool HookIsInstalled
        {
            get => this.keyboardHook.IsInstalled;
            set => this.Set(
                this.SwitchHook,
                value,
                this.keyboardHook.IsInstalled);
        }

        private void SwitchHook(bool on)
        {
            if (on)
            {
                this.keyboardHook.InstallHook();
            }
            else
            {
                this.keyboardHook.ReleaseHook();
            }
        }

        public KeyboardViewModel()
        {
            this.keyboardHook = new KeyboardHook(
                SynchronizationContext.Current);

            this.keyboardHook.KeyDown += this.KeyDown;
            this.keyboardHook.KeyUp += this.KeyUp;

            this.keys = new Dictionary<VirtualKeyCode, KeyViewModel.Token>
            {
                {VirtualKeyCode.W, new KeyViewModel.Token("W")},
                {VirtualKeyCode.A, new KeyViewModel.Token("A")},
                {VirtualKeyCode.S, new KeyViewModel.Token("S")},
                {VirtualKeyCode.D, new KeyViewModel.Token("D")},
                {VirtualKeyCode.Space, new KeyViewModel.Token("Space")},
                {VirtualKeyCode.LeftButton, new KeyViewModel.Token("M1")},
                {VirtualKeyCode.RightButton, new KeyViewModel.Token("M2")}
            };

            this.WButton = this.keys[VirtualKeyCode.W].Vm;
            this.AButton = this.keys[VirtualKeyCode.A].Vm;
            this.SButton = this.keys[VirtualKeyCode.S].Vm;
            this.DButton = this.keys[VirtualKeyCode.D].Vm;
            this.SpaceBar = this.keys[VirtualKeyCode.Space].Vm;
            this.M1 = this.keys[VirtualKeyCode.LeftButton].Vm;
            this.M2 = this.keys[VirtualKeyCode.RightButton].Vm;
        }

        private void KeyDown(VirtualKeyCode keyCode)
        {
            if (this.keys.TryGetValue(keyCode, out var token))
            {
                token.Push();
            }
        }

        private void KeyUp(VirtualKeyCode keyCode)
        {
            if (this.keys.TryGetValue(keyCode, out var token))
            {
                token.Release();
            }
        }
    }
}