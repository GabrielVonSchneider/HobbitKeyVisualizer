using System;

namespace HokeyViz.WinApi
{
    [Flags]
    internal enum MsLlHookStructFlags : uint
    {
        Injected = 0x01,
        IlInjected = 0x02
    }
}
