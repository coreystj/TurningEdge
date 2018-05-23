using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Delegates
{
    public delegate void OnConnectedAction(Session session);
    public delegate void OnConnectionFailedAction(string address, NetworkInfoException exception);
    public delegate void OnDisconnectedAction(Session session);

    public delegate void OnMessageSentSuccessAction(Session session);

    public delegate void OnMessageReceivedSuccessAction(Session session, byte[] bytes);
    public delegate void OnErrorAction(NetworkInfoException exception);

    public delegate void OnStoppedAction(Session session);
}
