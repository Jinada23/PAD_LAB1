using Broker.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Models;

namespace Broker.Storage
{
    public class ConnectionStorage : IConnectionStorage
    {
        private readonly List<Connection> _connections;
        private readonly object _lock;

        public ConnectionStorage()
        {
            _connections = new List<Connection>();
            _lock = new object();
        }

        public void Add(Connection connection)
        {
            lock (_lock)
            {
                _connections.Add(connection);
            }
        }
        public void Remove(string address)
        {
            lock (_lock)
            {
                _connections.RemoveAll(x => x.Address == address);
            }
        }

        public IList<Connection> GetConnectionByTopic(string topic)
        {
            var selectedConnections = new List<Connection>();
            selectedConnections = _connections.Where(x => x.Topic == topic).ToList();
            return selectedConnections;
        }

    }
}
