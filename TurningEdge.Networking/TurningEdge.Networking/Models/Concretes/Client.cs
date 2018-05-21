using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Models.Abstracts;

namespace TurningEdge.Networking.Models.Concretes
{
    public class Client : NetworkInfo
    {

        public Client(string ipAddress, int port) 
            : base(ipAddress, port)
        {
        }

        public override void Start()
        {
            IPAddress parsedIpAddress = IPAddress.Parse(_ipAddress);

            IPEndPoint localEndPoint = new IPEndPoint(parsedIpAddress, _port);

            // Connect to the remote endpoint.  
            _currentSession.CurrentSocket.BeginConnect(localEndPoint,
                new AsyncCallback(ConnectCallback), _currentSession);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.  
            Session client = (Session)ar.AsyncState;
            try
            {

                // Complete the connection.  
                client.CurrentSocket.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.CurrentSocket.RemoteEndPoint.ToString());
                FireOnStarted(client);
            }
            catch (Exception e)
            {
                FireOnStartedFailed(
                    new NetworkInfoException(
                        "Could not connect to the local end point: " + client, e));
            }

            Send(new byte[] { 9, 1, 5 });
        }
    }
}
