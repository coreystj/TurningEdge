
using TurningEdge.Networking.Models.Abstracts;

namespace TurningEdge.Networking.Models.Concretes
{
    public class Server<T> : NetworkInfo<T>
        where T : Session
    {

        public Server(string ipAddress, int port) 
            : base(ipAddress, port)
        {
            //OnConnected += Server_OnConnected;
        }

        public override void Connect()
        {
            //IPAddress parsedIpAddress = AddressHelper.Parse(_ipAddress);
            //IPEndPoint localEndPoint = new IPEndPoint(parsedIpAddress, _port);

            FireOnConnectionAttempt(_ipAddress, _port);

            _currentSession.Bind(_ipAddress, _port);
            Listen();
        }

        private void Listen()
        {
            _currentSession.Listen();
        }
    }
}
