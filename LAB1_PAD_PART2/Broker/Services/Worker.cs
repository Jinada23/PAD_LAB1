using Broker.Storage.Interfaces;
using Grpc.Core;
using GrpcMessager;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Broker.Services
{
    public class Worker : IHostedService
    {
        private Timer _timer;
        private readonly IConnectionStorage _connectionStorage;
        private readonly IMessageStorage _messageStorage;
        public Worker(IMessageStorage messageStorage, IConnectionStorage connectionStorage)
        {
            _connectionStorage = connectionStorage;
            _messageStorage = messageStorage;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoSendWork, null, 0, 500);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoSendWork(object state)
        {
            while (!_messageStorage.IsEmpty() && _connectionStorage != null && _messageStorage != null)
            {
                var message = _messageStorage.GetNext();
                if (message != null)
                {

                    var connections = _connectionStorage.GetConnectionByTopic(message.Topic);

                    foreach (var connection in connections)
                    {
                        var notifyClient = new NotifierProto.NotifierProtoClient(connection.Channel);
                        var notifyRequest = new NotifierRequest { Message = message.Message };

                        try
                        {
                            var reply = notifyClient.NotifyAsync(notifyRequest);
                        }
                        catch (RpcException ex)
                        {
                            if (ex.StatusCode == StatusCode.Internal)
                            {
                                _connectionStorage.Remove(connection.Address);
                            }
                        }
                    }

                    if (connections.Count == 0)
                    {
                        _messageStorage.Add(message);
                    }
                }

            }
        }
    }
}
