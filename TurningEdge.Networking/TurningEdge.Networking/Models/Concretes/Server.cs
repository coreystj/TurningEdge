using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TurningEdge.Networking.Models.Abstracts;

namespace TurningEdge.Networking.Models.Concretes
{
    public class Server : NetworkInfo
    {
        public Server(string ipAddress, int port) 
            : base(ipAddress, port)
        {
        }

        public override void Start()
        {
            IPAddress parsedIpAddress = IPAddress.Parse(_ipAddress);
            IPEndPoint localEndPoint = new IPEndPoint(parsedIpAddress, _port);

            _currentSession.CurrentSocket.Bind(localEndPoint);
            Listen();
            FireOnStarted(_currentSession);
        }

        private void Listen()
        {
            // Start an asynchronous socket to listen for connections.  
            Console.WriteLine("Waiting for a connection...");
            _currentSession.CurrentSocket.Listen(100);
            _currentSession.CurrentSocket.BeginAccept(
                new AsyncCallback(AcceptCallback),
                _currentSession);
        }


        public void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.  
            Session session = (Session)ar.AsyncState;
            Socket clientSocket = session.CurrentSocket.EndAccept(ar);

            // Create the state object.  
            Session clientSession = new Session(clientSocket);
            clientSocket.BeginReceive(clientSession.InBuffer, 0, Session.BUFFER_SIZE, 0,
                new AsyncCallback(ReadCallback), clientSession);

            FireOnStarted(clientSession);

            Listen();
        }
    }
}
