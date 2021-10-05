using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Publisher
{
    class PublisherSocket
    {
        private Socket _socket;

        public bool isConnected;

        public PublisherSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), ConnectedCallback, null);
            Thread.Sleep(2000);
        }

        public void Send(byte[] data)
        {
            try
            {
                _socket.Send(data);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while sending data. " + e.Message);
            }
        }

        private void ConnectedCallback(IAsyncResult asyncResult)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("Publisher connected to broker");
            }
            else
            {
                Console.WriteLine("Error: Publisher is not connected to broker.");
            }
            isConnected = _socket.Connected;
        }

    }
}
