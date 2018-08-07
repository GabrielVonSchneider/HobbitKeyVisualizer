namespace HobbitKeyVisualizer.WinApi
{
    internal enum VirtualKeyCode : ulong
    {
        /// <summary>
        /// Left Mouse Button
        /// </summary>
        LeftButton = 0x01,

        /// <summary>
        /// Right Mouse Button
        /// </summary>
        RightButton = 0x02,

        Escape = 0x1B,
        
        /*Regular number keys*/
        N0 = 0x30,
        N1 = 0x31,
        N2 = 0x32,
        N3 = 0x33,
        N4 = 0x34,
        N5 = 0x35,
        N6 = 0x36,
        N7 = 0x37,
        N8 = 0x38,
        N9 = 0x39,

        /// <summary>
        /// Enter
        /// </summary>
        Return = 0x0D,

        /*Letters*/
        A = 0x41,
        D = 0x44,
        S = 0x53,
        W = 0x57,
        I = 0x49,
    }
}
