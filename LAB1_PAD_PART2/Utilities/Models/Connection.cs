using Grpc.Net.Client;

namespace Utilities.Models
{
    public class Connection
    {
        public Connection(string ip, string topic)
        {
            Address = ip;
            Topic = topic;
            Channel = GrpcChannel.ForAddress(Address);
        }
        public string Address { get; set; }
        public string Topic { get; set; }
        public GrpcChannel Channel { get; }

    }
}
