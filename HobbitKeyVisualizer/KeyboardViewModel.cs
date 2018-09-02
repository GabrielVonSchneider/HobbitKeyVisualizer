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

        public TokenBasedDelegateCommand InstallHookCommand { get; }
        public TokenBasedDelegateCommand ReleaseHookCommand { get; }

        public KeyViewModel AButton { get; }
        public KeyViewModel DButton { get; }
        public KeyViewModel SButton { get; }
        public KeyViewModel WButton { get; }
        public KeyViewModel SpaceBar { get; set; }

        public KeyboardViewModel()
        {
            this.keyboardHook = new KeyboardHook(
                SynchronizationContext.Current);

            var token = TokenBasedDelegateCommand
                .Token.Create(obj => this.keyboardHook.InstallHook());
            this.InstallHookCommand = token.Command;

            token = TokenBasedDelegateCommand
                .Token.Create(obj => this.keyboardHook.ReleaseHook());
            this.ReleaseHookCommand = token.Command;

            this.keyboardHook.KeyDown += this.KeyDown;
            this.keyboardHook.KeyUp += this.KeyUp;

            this.keys = new Dictionary<VirtualKeyCode, KeyViewModel.Token>
            {
                {VirtualKeyCode.W, new KeyViewModel.Token()},
                {VirtualKeyCode.A, new KeyViewModel.Token()},
                {VirtualKeyCode.S, new KeyViewModel.Token()},
                {VirtualKeyCode.D, new KeyViewModel.Token()},
                {VirtualKeyCode.Space, new KeyViewModel.Token()}
            };

            this.WButton = this.keys[VirtualKeyCode.W].Vm;
            this.AButton = this.keys[VirtualKeyCode.A].Vm;
            this.SButton = this.keys[VirtualKeyCode.S].Vm;
            this.DButton = this.keys[VirtualKeyCode.D].Vm;
            this.SpaceBar = this.keys[VirtualKeyCode.Space].Vm;
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