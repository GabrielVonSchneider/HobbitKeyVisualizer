using System;

namespace HobbitKeyVisualizer.WinApi
{
    internal struct WindowsMessage
    {
        private readonly IntPtr message;

        private WindowsMessage(int message)
        {
            this.message = new IntPtr(message);
        }

        public override int GetHashCode()
        {
            return this.message.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is WindowsMessage msg && this.message == msg.message;
        }

        public static readonly WindowsMessage KeyDown =
            new WindowsMessage(0x0100);
        public static readonly WindowsMessage KeyUp =
            new WindowsMessage(0x0101);
        public static readonly WindowsMessage SysKeyDown =
            new WindowsMessage(0x0104);
        public static readonly WindowsMessage SysKeyUp
            = new WindowsMessage(0x0105);
    }
}
