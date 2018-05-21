using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurningEdge.PathFinder.Models.Structs
{
    public struct Node
    {
        #region Variables Declaration
        public int F;
        public int G;
        public int H;  // f = gone + heuristic
        public int X;
        public int Y;
        public int PX; // Parent
        public int PY;
        #endregion

        public override string ToString()
        {
            return "(node: " + this.X + ", " + this.Y + ")";
        }
    }
}
