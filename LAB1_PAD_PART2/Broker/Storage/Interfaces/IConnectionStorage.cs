using System.Collections.Generic;
using Utilities.Models;

namespace Broker.Storage.Interfaces
{
    public interface IConnectionStorage
    {
        void Add(Connection connection);
        void Remove(string address);
        IList<Connection> GetConnectionByTopic(string topic);
    }
}
