using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.Serializers;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    [Serializable]
    public class ChunkData : ICloneable
    {
        protected int _id;
        protected int _userId;
        protected int _x;
        protected int _y;
        protected int _layerId;

        protected byte[] _heights;
        protected short[] _materials;
        protected short[] _constructions;
        protected byte[] _rotations;
        protected byte[] _states;

        public int Id
        {
            get
            {
                return _id;
            }
        }
        public int UserId
        {
            get
            {
                return _userId;
            }
        }
        public int X
        {
            get
            {
                return _x;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
        }

        public int LayerId
        {
            get
            {
                return _layerId;
            }
        }


        public byte[] Heights
        {
            get
            {
                return _heights;
            }
            set
            {
                _heights = value;
            }
        }
        public short[] Materials
        {
            get
            {
                return _materials;
            }
            set
            {
                _materials = value;
            }
        }
        public short[] Constructions
        {
            get
            {
                return _constructions;
            }
            set
            {
                _constructions = value;
            }
        }
        public byte[] Rotations
        {
            get
            {
                return _rotations;
            }
            set
            {
                _rotations = value;
            }
        }
        public byte[] States
        {
            get
            {
                return _states;
            }
            set
            {
                _states = value;
            }
        }

        public ChunkData()
        {
            _id = -1;
            _userId = -1;
            _x = 0;
            _y = 0;
            _layerId = -1;

            _heights = new byte[15*15];
            _materials = new short[15 * 15];
            _constructions = new short[15 * 15];
            _rotations = new byte[15 * 15];
            _states = new byte[15 * 15];
        }

        public ChunkData(
            int id,
            int userId,
            int x,
            int y,
            int layerId,
            byte[] heights,
            short[] materials,
            short[] constructions,
            byte[] rotations,
            byte[] states
            )
            : base()
        {
            _id = id;
            _userId = userId;
            _x = x;
            _y = y;
            _layerId = layerId;

            _heights = heights;
            _materials = materials;
            _constructions = constructions;
            _rotations = rotations;
            _states = states;
        }

        public ChunkData(     
            int id,
            int userId,
            int x,
            int y,
            int layerId)
            : base()
        {
            _id = id;
            _userId = userId;
            _x = x;
            _y = y;
            _layerId = layerId;

            _heights = new byte[15 * 15];
            _materials = new short[15 * 15];
            _constructions = new short[15 * 15];
            _rotations = new byte[15 * 15];
            _states = new byte[15 * 15];
        }
        
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as ChunkData;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public object Clone()
        {
            return new ChunkData(
                _id,
                _userId,
                _x,
                _y,
                _layerId,
                _heights,
                _materials,
                _constructions,
                _rotations,
                _states);
        }
    }
}
