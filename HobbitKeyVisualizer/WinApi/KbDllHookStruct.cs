using System.Runtime.InteropServices;

namespace HobbitKeyVisualizer.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal class KbDllHookStruct
    {
        public VirtualKeyCode vkCode;
        public ulong scanCode;
        public KbDllHookStructFlags flags;
        public ulong time;
        public ulong dwExtraInfo;
    }
}