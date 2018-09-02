using System.Runtime.InteropServices;

namespace HobbitKeyVisualizer.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal class KbDllHookStruct
    {
        public VirtualKeyCode vkCode;
        public uint scanCode;
        public KbDllHookStructFlags flags;
        public uint time;
        public ulong dwExtraInfo;
    }
}