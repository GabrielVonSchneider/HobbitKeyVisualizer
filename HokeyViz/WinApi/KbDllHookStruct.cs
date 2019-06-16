using System.Runtime.InteropServices;

namespace HokeyViz.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal class KbdLlHookStruct
    {
        public VirtualKeyCode vkCode;
        public uint scanCode;
        public KbdLlHookStructFlags flags;
        public uint time;
        public ulong dwExtraInfo;
    }
}