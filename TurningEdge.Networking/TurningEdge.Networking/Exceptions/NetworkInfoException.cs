using System;

namespace TurningEdge.Networking.Exceptions
{

    [Serializable]
    public class NetworkInfoException : Exception
    {
        public NetworkInfoException() { }
        public NetworkInfoException(string message) : base(message) { }
        public NetworkInfoException(string message, Exception inner) : base(message, inner) { }
        protected NetworkInfoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
