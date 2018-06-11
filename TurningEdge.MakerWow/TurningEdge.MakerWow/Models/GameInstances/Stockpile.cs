using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    [Serializable]
    public class Stockpile : Inventory
    {
        public Stockpile(
            int id,
            int userId,
            int slotCount)
            : base(id, userId, slotCount)
        {
        }
        public Stockpile()
        {

        }

        public Stockpile(int id, int userId, string data) 
            : base(id, userId, data)
        {
        }

        public Stockpile(int id, int userId, Slot[] slots) 
            : base(id, userId, slots)
        {
        }

        // override object.Equals
        public new bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as Stockpile;

            return (model.GetHashCode() == GetHashCode());
        }

        public new object Clone()
        {
            return new Stockpile(_id,
                _userId,
                _slots);
        }
    }
}
