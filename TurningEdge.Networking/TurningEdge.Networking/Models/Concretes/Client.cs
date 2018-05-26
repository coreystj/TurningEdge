
using TurningEdge.Networking.Models.Abstracts;

namespace TurningEdge.Networking.Models.Concretes
{
    public class Client<T> : NetworkInfo<T>
        where T : Session
    {

        public Client(string ipAddress, int port) 
            : base(ipAddress, port)
        {
        }

        public override void Connect()
        {
            _currentSession.Connect(_ipAddress, _port);
        }

        public void Send(byte[] bytes)
        {
            Send(_currentSession, bytes);
        }
    }
}
