using System.Runtime.InteropServices;

namespace HokeyViz.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal class MsLlHookStruct
    {
        public Point Pt { get; }
        public MouseData MouseData { get; }
        public MsLlHookStructFlags Flags { get; }
        public uint Time { get; }
        public ulong DwExtraInfo { get; }
    }
}
