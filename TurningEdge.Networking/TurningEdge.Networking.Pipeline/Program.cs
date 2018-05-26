using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Factories.Abstracts;
using TurningEdge.Networking.Helpers;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.WindowsSocket.Models.Concretes;

namespace TurningEdge.Networking.Pipeline
{
    class Program
    {
        private static ManualResetEvent _tcpSessionWaitHandler = new ManualResetEvent(false);
        private static NetworkInfo<SocketSession> _networker;
        static void Main(string[] args)
        {
            // Declare all variables.
            Type networkerType;
            string hostname = "turningedge.ddns.net";
            int port = 3389;
            char userInput;

            // request user input.
            Console.Write("Type Server(S) or Client(C): ");
            userInput = Console.ReadLine().ToUpper()[0];

            // Store the network type.
            //networkerType = (userInput == 'S') ? typeof(Server<SocketSession>) : typeof(Client<SocketSession>);

            string ipAddress = (userInput == 'S' 
                || AddressHelper.CheckIfSamePublic(hostname))
                ? AddressHelper.GetLocalAddress() 
                : hostname;

            if(userInput == 'S')
                _networker = NetworkFactory.CreateServer<SocketSession>(ipAddress, port);
            else
                _networker = NetworkFactory.CreateClient<SocketSession>(ipAddress, port);

            // Bind the networker to the current process.
            Bind(_networker);

            // Connect the networker.
            _networker.Connect();


            while(true)
            {
                
                if (_networker is Client<SocketSession> && _networker.IsConnected)
                    ClientSend();
                else
                    Thread.Sleep(1000);
            }

            _tcpSessionWaitHandler.WaitOne();
        }

        private static void Bind(NetworkInfo<SocketSession> networker)
        {
            networker.OnListening += Networker_OnListening;
            networker.OnConnectionAttempt += Networker_OnConnectionAttempt;
            networker.OnConnected += Networker_OnConnected;
            networker.OnDisconnected += Networker_OnDisconnected; ;
            networker.OnError += Networker_OnError;
            networker.OnConnectionFailed += Networker_OnConnectionFailed;
            networker.OnMessageSentSuccess += Networker_OnMessageSentSuccess;
            networker.OnMessageReceivedSuccess += Networker_OnMessageReceivedSuccess;
            networker.OnStopped += Networker_OnStopped;
    }

        private static void Networker_OnListening(string address, int port)
        {
            Console.WriteLine("Listening on: " + address + ":" + port);
        }

        private static void Networker_OnConnectionAttempt(string address, int port)
        {
            Console.WriteLine("Attempting to connect to: " + address + ":" + port);
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
            if (_networker is Client<SocketSession>)
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
        }

        private static void ClientSend()
        {
            Console.Write("Send: ");
            ((Client<SocketSession>)_networker).Send(Encoding.ASCII.GetBytes(Console.ReadLine()));
            //((Client<SocketSession>)_networker).Send(new byte[] { 1,3,5});
        }
    }
}
