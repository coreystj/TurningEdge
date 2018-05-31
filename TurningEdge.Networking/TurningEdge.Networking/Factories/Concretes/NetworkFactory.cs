using System;
using TurningEdge.Networking.DataTypes;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Factories.Abstracts
{
    public static class NetworkFactory
    {
        public static NetworkInfo<T> CreateServer<T>(string ipAddress, int port)
            where T : Session
        {
            var networkInfo = Create<Server<T>>(ipAddress, port);
            return networkInfo as NetworkInfo<T>;
        }

        public static NetworkInfo<T> CreateClient<T>(string ipAddress, int port)
            where T : Session
        {
            var networkInfo = Create<Client<T>>(ipAddress, port);
            return networkInfo as NetworkInfo<T>;
        }

        public static Packet CreatePacket(PacketType type, byte[] bytes)
        {
            return new Packet(type, bytes);
        }

        public static Session CreateSession<T>(params object[] args)
            where T : Session
        {
            return (Session)Activator.CreateInstance(typeof(T), args);
        }

        private static T Create<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        private static T Create<T>(string ipAddress, int port)
        {
            return Create<T>(new object[] { ipAddress , port});
        }
    }
}
