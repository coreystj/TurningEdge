using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Delegates
{
    public delegate void OnStartedAction(Session session);
    public delegate void OnStartedFailedAction(NetworkInfoException exception);

    public delegate void OnMessageSentSuccessAction(Session session);
    public delegate void OnMessageSentFailedAction(NetworkInfoException exception);

    public delegate void OnMessageReceivedSuccessAction(Session session);
    public delegate void OnMessageReceivedFailedAction(NetworkInfoException exception);

    public delegate void OnStoppedAction(Session session);
}
