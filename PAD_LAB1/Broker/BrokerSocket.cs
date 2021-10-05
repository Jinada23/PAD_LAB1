using System;
using System.Net;
using System.Net.Sockets;
using Utilities;

namespace Broker
{
    public class BrokerSocket
    {
        private Socket _socket;

        public BrokerSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start(string ip, int port)
        {
            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            _socket.Listen();
            Accept();
        }

        private void Accept()
        {
            _socket.BeginAccept(AcceptedCallback, null);
        }

        private void AcceptedCallback(IAsyncResult asyncResult)
        {
            var connection = new ConnectionInfo();

            try
            {
                connection.Socket = _socket.EndAccept(asyncResult);
                connection.Address = connection.Socket.RemoteEndPoint.ToString(); 
                connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallback, connection);
                Console.WriteLine("An connection has been established with " + connection.Address);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unacceptable connection." + e.Message);
            }
            finally
            {
                Accept();
            }
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            var connection = (ConnectionInfo)asyncResult.AsyncState ;

            try
            {
                Socket senderSocket = connection.Socket;
                SocketError response;
                var buffSize = senderSocket.EndReceive(asyncResult, out response);
                
                if (response == SocketError.Success)
                {
                    byte[] data = new byte[buffSize];
                    Array.Copy(connection.Data, data, buffSize);

                    DataHandler.Handle(data, connection);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Data were not received. " + e.Message);
            }
            finally
            {
                try
                {
                    if (!connection.Socket.Connected) 
                    { 
                        throw new SocketException((int)SocketError.AddressNotAvailable); 
                    }

                    connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallback, connection);
                }
                catch (Exception e)
                {
                    var address = connection.Socket.RemoteEndPoint.ToString();
                    Console.WriteLine($"Receiving stopped for: {address}. Connection disconnected.");
                 
                    ConnectionStorage.Remove(address);
                    connection.Socket.Close();
                }
            }
        }
    }
}
