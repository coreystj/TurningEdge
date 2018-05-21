using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TurningEdge.Networking.Delegates;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Models.Abstracts
{
    public abstract class NetworkInfo
    {
        public event OnStartedAction OnStarted = delegate { };
        public event OnStartedFailedAction OnStartedFailed = delegate { };
        public event OnMessageSentSuccessAction OnMessageSentSuccess  = delegate { };
        public event OnMessageSentFailedAction OnMessageSentFailed  = delegate { };
        public event OnMessageReceivedSuccessAction OnMessageReceivedSuccess = delegate { };
        public event OnMessageReceivedFailedAction OnMessageReceivedFailed  = delegate { };
        public event OnStoppedAction OnStopped  = delegate { };

        protected Session _currentSession;
        protected string _ipAddress;
        protected int _port;

        public Session CurrentSession
        {
            get { return _currentSession; }
            set { _currentSession = value; }
        }

        public NetworkInfo(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            _currentSession = new Session(_ipAddress, _port);
        }

        public abstract void Start();
        public void Send(byte[] bytes)
        {
            // Begin sending the data to the remote device.  
            _currentSession.CurrentSocket.BeginSend(bytes, 0, bytes.Length, 0,
                new AsyncCallback(SendCallback), _currentSession);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            Session session = (Session)ar.AsyncState;
            Socket socket = session.CurrentSocket;

            // Read data from the client socket.   
            int bytesRead = socket.EndReceive(ar);

            if (bytesRead > 0)
            {
                // Not all data received. Get more.  
                session.CurrentSocket.BeginReceive(session.InBuffer, 0, Session.BUFFER_SIZE, 0,
                new AsyncCallback(ReadCallback), session);

                FireOnMessageReceivedSuccess(_currentSession);
                Send(_currentSession.InBuffer);
            }
        }

        protected void SendCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.  
            Session session = (Session)ar.AsyncState;
            try
            {
                // Complete sending the data to the remote device.  
                int bytesSent = session.CurrentSocket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                FireOnMessageSentSuccess(session);
            }
            catch (Exception e)
            {
                FireOnMessageSentFailed(
                    new NetworkInfoException(
                        "Could not send to local end poin: " + session, e));
            }
        }

        public void FireOnStarted(Session session)
        {
            OnStarted(session);
        }

        public void FireOnStartedFailed(NetworkInfoException exception)
        {
            OnStartedFailed(exception);
        }

        public void FireOnMessageSentSuccess(Session session)
        {
            OnMessageSentSuccess(session);
        }

        public void FireOnMessageSentFailed(NetworkInfoException exception)
        {
            OnMessageSentFailed(exception);
        }

        public void FireOnMessageReceivedSuccess(Session session)
        {
            OnMessageReceivedSuccess(session);
        }

        public void FireOnMessageReceivedFailed(NetworkInfoException exception)
        {
            OnMessageReceivedFailed(exception);
        }

        public void FireOnStopped(Session session)
        {
            OnStopped(session);
        }

        public void Stop()
        {
            _currentSession.CurrentSocket.Shutdown(SocketShutdown.Both);
            _currentSession.CurrentSocket.Close();
            FireOnStopped(_currentSession);
        }

        public override string ToString()
        {
            return _currentSession.ToString();
        }

    }
}
