using System;
using System.Runtime.InteropServices;
namespace HeNuo.Network
{
    [StructLayout(LayoutKind.Auto)]
    public struct NetworkChannelInfo
    {
        public string Name;
        public string IPAddress;
        public Type ChannelType;
        public readonly static NetworkChannelInfo None = new NetworkChannelInfo();
    }
}
