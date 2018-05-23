using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Factories.Abstracts;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Pipeline
{
    class Program
    {
        private static ManualResetEvent _tcpSessionWaitHandler = new ManualResetEvent(false);
        private static NetworkInfo _networker;
        static void Main(string[] args)
        {
            // Declare all variables.
            Type networkerType;
            string ipAddress = "127.0.0.1";
            int port = 3456;
            byte[] bytes = new byte[] { 9, 3, 4 };
            char userInput;

            // request user input.
            Console.Write("Type Server(S) or Client(C): ");
            userInput = Console.ReadLine().ToUpper()[0];

            // Store the network type.
            networkerType = (userInput == 'S') ? typeof(Server) : typeof(Client);

            // Create the network factory.
            var networkFactory = new NetworkFactory<NetworkInfo>(ipAddress, port);

            // Create networker.
            _networker = networkFactory.Create(networkerType);

            // Bind the networker to the current process.
            Bind(_networker);

            // Connect the networker.
            _networker.Connect();

            _tcpSessionWaitHandler.WaitOne();
        }

        private static void Bind(NetworkInfo networker)
        {
            networker.OnConnected += Networker_OnConnected;
            networker.OnDisconnected += Networker_OnDisconnected; ;
            networker.OnError += Networker_OnError;
            networker.OnConnectionFailed += Networker_OnConnectionFailed;
            networker.OnMessageSentSuccess += Networker_OnMessageSentSuccess;
            networker.OnMessageReceivedSuccess += Networker_OnMessageReceivedSuccess;
            networker.OnStopped += Networker_OnStopped;
    }

        private static void Networker_OnDisconnected(Session session)
        {
            Console.WriteLine("Disconnected: " + session);
        }

        private static void Networker_OnStopped(Session session)
        {
            _tcpSessionWaitHandler.Set();
            Console.WriteLine("Networker_OnStopped");
        }

        private static void Networker_OnMessageReceivedSuccess(Session session, byte[] bytes)
        {
            Console.WriteLine("Received: " + bytes.Length + " byte(s).");
            if (_networker is Client)
                ClientSend();
            else
                _networker.Send(session, bytes);
        }

        private static void Networker_OnMessageSentSuccess(Session session)
        {
            Console.WriteLine("Networker_OnMessageSentSuccess");

        }

        private static void Networker_OnError(NetworkInfoException exception)
        {
            Console.WriteLine(exception.ToString());
        }

        private static void Networker_OnConnectionFailed(string address, NetworkInfoException exception)
        {
            Console.WriteLine("Failed to connect to: " + address);
        }

        private static void Networker_OnConnected(Session session)
        {
            Console.WriteLine("Networker_OnConnected: " + session);
            if (_networker is Client)
                ClientSend();
        }

        private static void ClientSend()
        {
            Console.Write("Send: ");
            ((Client)_networker).Send(Encoding.ASCII.GetBytes(Console.ReadLine()));
        }
    }
}
