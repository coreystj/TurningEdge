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
            private static ManualResetEvent tcpSessionWaitHandler = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            // Declare all variables.
            NetworkInfo networker;
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
            networker = networkFactory.Create(networkerType);

            // Bind the networker to the current process.
            Bind(networker);

            // Start the networker.
            networker.Start();

            tcpSessionWaitHandler.WaitOne();
        }

        private static void Bind(NetworkInfo networker)
        {
            networker.OnStarted += Networker_OnStarted;
            networker.OnStartedFailed += Networker_OnStartedFailed;
            networker.OnMessageSentSuccess += Networker_OnMessageSentSuccess;
            networker.OnMessageSentFailed += Networker_OnMessageSentFailed;
            networker.OnMessageReceivedSuccess += Networker_OnMessageReceivedSuccess;
            networker.OnMessageReceivedFailed += Networker_OnMessageReceivedFailed;
            networker.OnStopped += Networker_OnStopped;
    }

        private static void Networker_OnStopped(Session session)
        {
            tcpSessionWaitHandler.Set();
            Console.WriteLine("Networker_OnStopped");
        }

        private static void Networker_OnMessageReceivedFailed(NetworkInfoException exception)
        {
            Console.WriteLine(exception.ToString());
        }

        private static void Networker_OnMessageReceivedSuccess(Session session)
        {
            Console.WriteLine("Networker_OnMessageReceivedSuccess");
        }

        private static void Networker_OnMessageSentFailed(NetworkInfoException exception)
        {
            Console.WriteLine(exception.ToString());
        }

        private static void Networker_OnMessageSentSuccess(Session session)
        {
            Console.WriteLine("Networker_OnMessageSentSuccess");
        }

        private static void Networker_OnStartedFailed(Exceptions.NetworkInfoException exception)
        {
            Console.WriteLine(exception.ToString());
        }

        private static void Networker_OnStarted(Session session)
        {
            Console.WriteLine("Networker_OnStarted");
        }
    }
}
