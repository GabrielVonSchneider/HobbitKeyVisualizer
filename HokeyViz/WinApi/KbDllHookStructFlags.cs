using System;

namespace HokeyViz.WinApi
{
    [Flags]
    internal enum KbdLlHookStructFlags : uint
    {
        Extended = 0x01,
        Injected = 0x10,
        AltDown = 0x20,
        /// <summary>
        /// The transition state. When 1, release. Otherwise pressed.
        /// </summary>
        Up = 0x80,
    }
}
