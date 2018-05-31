using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TurningEdge.Math.Structs
{
    [Serializable]
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Vector2 otherCoordinate = (Vector2)obj;
            return GetHashCode().Equals(otherCoordinate.GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString()
        {
            return "Vector2{" + x + ", " + y + "}";
        }
    }
}
