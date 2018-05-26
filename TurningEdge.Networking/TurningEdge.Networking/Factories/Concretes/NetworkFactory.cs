using System;
using TurningEdge.Networking.DataTypes;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Factories.Abstracts
{
    public static class NetworkFactory
    {
        public static Server<T> CreateServer<T>(string ipAddress, int port)
            where T : Session
        {
            return Create<Server<T>>(ipAddress, port);
        }

        public static Client<T> CreateClient<T>(string ipAddress, int port)
            where T : Session
        {
            return Create<Client<T>>(ipAddress, port);
        }

        public static Packet CreatePacket(PacketType type, byte[] bytes)
        {
            return new Packet(type, bytes);
        }

        public static T CreateSession<T>(params object[] args)
            where T : Session
        {
            return (T)Activator.CreateInstance(typeof(T), args);
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
