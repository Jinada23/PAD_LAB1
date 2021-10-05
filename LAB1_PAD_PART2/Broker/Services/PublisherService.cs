using GrpcMessager;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using Utilities.Models;
using Broker.Storage.Interfaces;

namespace Broker.Services
{
    public class PublisherService : PublisherProto.PublisherProtoBase
    {
        private IMessageStorage _messageStorage;
        public PublisherService(IMessageStorage messageStorage)
        {
            _messageStorage = messageStorage;
        }
        public override Task<PublishReply> Publish(PublishRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Received: {request.Topic} : {request.Message}");

            _messageStorage.Add(new MessageData
            {
                Topic = request.Topic,
                Message = request.Message,
            });

            return Task.FromResult(new PublishReply
            {
                IsConnected = true
            });
        }
    }
}