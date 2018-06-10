using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.Serializers;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    public class ChunkData
    {
        protected int _id;
        protected int _userId;
        protected int _x;
        protected int _y;
        protected int _layerId;

        protected Landscape _landscapeBuffer;
        protected Construction _constructionBuffer;

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

        public Landscape LandscapeBuffer
        {
            get
            {
                return _landscapeBuffer;
            }
        }
        public Construction ConstructionBuffer
        {
            get
            {
                return _constructionBuffer;
            }
        }

        public ChunkData()
        {

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

            _landscapeBuffer.Color = new byte[256];
            _landscapeBuffer.Ground = new byte[256];
            _landscapeBuffer.Height = new byte[256];

            _constructionBuffer.ConstructionId = new byte[256];
            _constructionBuffer.RotationIndex = new byte[256];
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
    }
}
