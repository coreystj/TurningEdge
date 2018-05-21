using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.Math.Structs;
using TurningEdge.PathFinder.Models.Concretes;

namespace TurningEdge.PathFinder.Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new PathFinderEngine();

            engine.Unblock(0, 0);
            engine.Unblock(0, 1);
            engine.Unblock(0, 2);
            engine.Unblock(0, 3);

            Coordinate start;
            start.x = 0;
            start.y = 0;

            Coordinate end;
            end.x = 0;
            end.y = 3;

            var points = engine.FindPath(start, end, true);

            foreach (var point in points)
            {
                Console.WriteLine(point);
            }
        }
    }
}
