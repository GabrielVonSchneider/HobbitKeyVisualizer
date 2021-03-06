﻿using HokeyViz.Common;
using HokeyViz.WinApi;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HokeyViz
{
    internal class MainWindowViewModel : PropertyChangedBase, IDisposable
    {
        private readonly KeyboardHook keyboardHook;
        private readonly Dictionary<VirtualKeyCode, KeyViewModel.Token> keys;

        public KeyViewModel AButton { get; }
        public KeyViewModel DButton { get; }
        public KeyViewModel SButton { get; }
        public KeyViewModel WButton { get; }
        public KeyViewModel Control { get; }
        public KeyViewModel SpaceBar { get; }
        public KeyViewModel Escape { get; }
        public KeyViewModel Enter { get; }

        private readonly KeyViewModel.Token m1Token;
        private readonly KeyViewModel.Token m2Token;
        private readonly MouseHook mouseHook;

        public bool KeyboardHookIsInstalled
        {
            get => this.keyboardHook.IsInstalled;
            set => this.Set(
                this.SwitchKeyboardHook,
                value,
                this.keyboardHook.IsInstalled);
        }

        public bool MouseHookIsInstalled
        {
            get => this.mouseHook.IsInstalled;
            set => this.Set(
                this.SwitchMouseHook,
                value,
                this.mouseHook.IsInstalled);
        }

        private void SwitchMouseHook(bool on)
        {
            if (on)
            {
                this.mouseHook.Set();
            }
            else
            {
                this.mouseHook.Unhook();
            }
        }

        private void SwitchKeyboardHook(bool on)
        {
            if (on)
            {
                this.keyboardHook.Set();
            }
            else
            {
                this.keyboardHook.Unhook();
            }
        }

        public MainWindowViewModel()
        {
            this.keyboardHook = new KeyboardHook(
                SynchronizationContext.Current);
            this.keyboardHook.KeyDown += this.KeyDown;
            this.keyboardHook.KeyUp += this.KeyUp;

            this.mouseHook = new MouseHook(
                SynchronizationContext.Current);
            this.mouseHook.M1Down += () => this.m1Token.Push();
            this.mouseHook.M2Down += () => this.m2Token.Push();
            this.mouseHook.M1Up += () => this.m1Token.Release();
            this.mouseHook.M2Up += () => this.m2Token.Release();

            this.keys = new Dictionary<VirtualKeyCode, KeyViewModel.Token>
            {
                {VirtualKeyCode.W, new KeyViewModel.Token("W")},
                {VirtualKeyCode.A, new KeyViewModel.Token("A")},
                {VirtualKeyCode.S, new KeyViewModel.Token("S")},
                {VirtualKeyCode.D, new KeyViewModel.Token("D")},
                {VirtualKeyCode.LControl, new KeyViewModel.Token("Ctrl")},
                {VirtualKeyCode.Space, new KeyViewModel.Token("Space")},
                {VirtualKeyCode.Escape, new KeyViewModel.Token("Esc")},
                {VirtualKeyCode.Return, new KeyViewModel.Token("Enter")}
            };

            this.WButton = this.keys[VirtualKeyCode.W].Vm;
            this.AButton = this.keys[VirtualKeyCode.A].Vm;
            this.SButton = this.keys[VirtualKeyCode.S].Vm;
            this.DButton = this.keys[VirtualKeyCode.D].Vm;
            this.Control = this.keys[VirtualKeyCode.LControl].Vm;
            this.SpaceBar = this.keys[VirtualKeyCode.Space].Vm;
            this.Escape = this.keys[VirtualKeyCode.Escape].Vm;
            this.Enter = this.keys[VirtualKeyCode.Return].Vm;

            this.m1Token = new KeyViewModel.Token("M1");
            this.m2Token = new KeyViewModel.Token("M2");
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

        public KeyViewModel M1 => this.m1Token.Vm;
        public KeyViewModel M2 => this.m2Token.Vm;

        private IEnumerable<Hook> Hooks => new Hook[]
        {
            this.keyboardHook,
            this.mouseHook
        };

        public void Clear()
        {
            foreach (var hook in this.Hooks)
            {
                if (hook.IsInstalled)
                {
                    hook.Unhook();
                }
            }
        }

        public void Dispose()
        {
            this.Clear();
        }
    }
}