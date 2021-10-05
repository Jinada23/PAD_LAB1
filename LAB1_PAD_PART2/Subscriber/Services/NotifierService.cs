using Grpc.Core;
using GrpcMessager;
using System;
using System.Threading.Tasks;

namespace Subscriber.Services
{
    public class NotifierService : NotifierProto.NotifierProtoBase
    {
        public override Task<NotifierReply> Notify(NotifierRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Message: {request.Message}");
            return Task.FromResult(new NotifierReply
            {
                IsSucces = true
            });
        }
    }
}
