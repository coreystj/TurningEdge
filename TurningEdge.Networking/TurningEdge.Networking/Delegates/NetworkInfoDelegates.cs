
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Delegates
{
    public delegate void OnConnectionAttemptAction(string address, int port);
    public delegate void OnListeningAction(string address, int port);
    
    public delegate void OnConnectedAction(Session session);
    public delegate void OnConnectionFailedAction(string address, NetworkInfoException exception);
    public delegate void OnDisconnectedAction(Session session);

    public delegate void OnMessageSendAttemptAction(Session session);
    public delegate void OnMessageSentSuccessAction(Session session);

    public delegate void OnMessageReceivedSuccessAction(Session session, byte[] bytes);
    public delegate void OnErrorAction(NetworkInfoException exception);

    public delegate void OnStoppedAction(Session session);
}
