﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.GameInstances
{
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
    }
}