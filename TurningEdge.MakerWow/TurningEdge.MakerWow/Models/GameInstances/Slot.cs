using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    [Serializable]
    public class Slot
    {
        protected int _itemId;
        protected int _amount;

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public Slot()
        {

        }

        public Slot(int itemId, int amount)
        {
            _itemId = itemId;
            _amount = amount;
        }

    }
}
