using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.PathFinder.Models.Structs;

namespace TurningEdge.PathFinder.Models.Concretes
{
    public class ComparePFNode : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            if (x.F > y.F)
                return 1;
            else if (x.F < y.F)
                return -1;
            return 0;
        }
    }
}
