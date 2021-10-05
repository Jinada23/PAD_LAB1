using System.Net.Sockets;

namespace Utilities
{
    public class ConnectionInfo
    {
        public const int BUFFER_SIZE = 1024;
        public Socket Socket { get; set; }
        public string Address { get; set; } //127.0.0.1:9000
        public string Topic { get; set; }
        public byte[] Data { get; set; }

        public ConnectionInfo()
        {
            Data = new byte[BUFFER_SIZE];   
        }
    }
}
