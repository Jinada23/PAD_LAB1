using Broker.Storage.Interfaces;
using Grpc.Core;
using GrpcMessager;
using System.Threading.Tasks;
using Utilities.Models;

namespace Broker.Services
{
    public class SubscriberService : SubscriberProto.SubscriberProtoBase
    {
        private readonly IConnectionStorage _connectionStorage;
        public SubscriberService(IConnectionStorage connectionStorage)
        {
            _connectionStorage = connectionStorage;
        }

        public override Task<SubscribeReply> Subscribe(SubscribeRequest request, ServerCallContext context)
        {
            var connection = new Connection(request.Address, request.Topic);

            _connectionStorage.Add(connection);

            return Task.FromResult(new SubscribeReply
            {
                IsConnected = true
            });
        }
    }
}
