using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Serializers;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    public class Inventory
    {
        protected int _id;
        protected int _userId;
        protected string _data;

        protected Slot[] _slots;

        public int Id
        {
            get { return _id; }
        }

        public int UserId
        {
            get { return _userId; }
        }

        public string Data
        {
            get { return _data; }
        }

        public Slot[] Slots
        {
            get { return _slots; }
        }
        public Inventory()
        {

        }

        public Inventory(
            int id,
            int userId,
            int slotCount)
            : base()
        {
            _id = id;
            _userId = userId;
            _slots = new Slot[slotCount];
            _data = _slots.ToBytes().Encode();
        }

        public Inventory(
            int id,
            int userId,
            string data)
            : base()
        {
            _id = id;
            _userId = userId;
            _data = data;
            _slots = _data.Decode().ToObject<Slot[]>();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as Inventory;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode()
                    ^ UserId.GetHashCode();
        }
    }
}
