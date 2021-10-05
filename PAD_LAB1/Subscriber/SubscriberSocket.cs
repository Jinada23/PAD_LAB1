using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Utilities;

namespace Subscriber
{
    class SubscriberSocket
    {
        private Socket _socket;
        private string _topic;

        public SubscriberSocket(string topic)
        {
            _topic = topic;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), ConnectedCallback, null);
            Console.WriteLine("An connection has been established.");
        }

        private void ConnectedCallback(IAsyncResult asyncResult)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("Subscriber has been succesfully connected.");
                Subscribe();
                StartReceive();
            }
            else
            {
                Console.WriteLine("Error: Subscriber has not been connected.");
            }
        }

        private void Subscribe()
        {
            var data = Encoding.UTF8.GetBytes(Settings.SUBSCRIBER_DELIMITATOR + _topic);
            Send(data);
        }

        private void Send(byte[] data)
        {
            try
            {
                _socket.Send(data);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not send data. " + e.Message);
            }
        }

        private void StartReceive()
        {
            var connection = new ConnectionInfo();
            connection.Socket = _socket;

            _socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallback, connection);
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            var connection = (ConnectionInfo)asyncResult.AsyncState;
            try
            {
                SocketError response;
                int buffSize = _socket.EndReceive(asyncResult, out response);
                if(response == SocketError.Success)
                {
                    byte[] data = new byte[buffSize];
                    Array.Copy(connection.Data, data, buffSize);

                    DataHandler.Handle(data);
                }
            }
            catch (Exception e)
            {
                    Console.WriteLine("Could not to receive data. " + e.Message );
            }
            finally
            {
                try
                {
                    connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallback, connection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    connection.Socket.Close();
                }
            }
        }

    }
}
