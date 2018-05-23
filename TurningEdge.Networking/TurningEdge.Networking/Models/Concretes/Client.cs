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

        public override void Connect()
        {
            DoTry(() => {
                IPAddress parsedIpAddress = IPAddress.Parse(_ipAddress);
                IPEndPoint localEndPoint = new IPEndPoint(parsedIpAddress, _port);

                // Connect to the remote endpoint.  
                _currentSession.CurrentSocket.BeginConnect(localEndPoint,
                    new AsyncCallback(ConnectCallback), _currentSession);
            });
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.  
            Session session = (Session)ar.AsyncState;
            DoTry(() => {
                // Complete the connection.  
                session.CurrentSocket.EndConnect(ar);

                session.CurrentSocket.BeginReceive(session.InBuffer, 0, Session.BUFFER_SIZE, 0,
                    new AsyncCallback(ReadCallback), session);

                FireOnConnected(session);
            });
        }

        public void Send(byte[] bytes)
        {
            Send(_currentSession, bytes);
        }
    }
}
