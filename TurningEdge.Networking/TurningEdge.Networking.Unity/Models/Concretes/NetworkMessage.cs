using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace TurningEdge.Networking.Unity.Models.Concretes
{
    /// <summary>
    /// This class allows the network easily manipulate messages from and between connections.
    /// </summary>
    public class NetworkMessage : MessageBase
    {

        private byte[] _data;
        /// <summary>
        /// The data associated to this message.
        /// </summary>
        public byte[] Data
        {
            get
            {
                return _data;
            }
            internal set
            {
                _data = value;
            }
        }

        public const short SEND_MESSAGE = 400;

        /// <summary>
        /// The current message protocol. BROADCAST_ALL
        /// </summary>
        public uint protocol = 1;

        /// <summary>
        /// The current message command.
        /// </summary>
        public uint command = 0;

        /// <summary>
        /// The current message recipient ID.
        /// </summary>
        public uint connectionId = 0;

        /// <summary>
        /// Clones the current reference.
        /// </summary>
        /// <returns>Returns the cloned reference.</returns>
        public NetworkMessage Clone()
        {
            NetworkMessage tmpNMRef = new NetworkMessage();
            tmpNMRef.Data = _data;
            tmpNMRef.protocol = protocol;
            tmpNMRef.command = command;
            tmpNMRef.connectionId = connectionId;
            return tmpNMRef;
        }

        /// <summary>
        /// Deserializes the data.
        /// </summary>
        /// <param name="reader">The network reader object.</param>
        public override void Deserialize(NetworkReader reader)
        {
            protocol = reader.ReadPackedUInt32();
            command = reader.ReadPackedUInt32();
            connectionId = reader.ReadPackedUInt32();
            Data = reader.ReadBytesAndSize();
        }

        /// <summary>
        /// Serializes the data.
        /// </summary>
        /// <param name="writer">The network reader object.</param>
        public override void Serialize(NetworkWriter writer)
        {
            writer.WritePackedUInt32(protocol);
            writer.WritePackedUInt32(command);
            writer.WritePackedUInt32(connectionId);
            writer.WriteBytesFull(Data);
        }
    }
}
