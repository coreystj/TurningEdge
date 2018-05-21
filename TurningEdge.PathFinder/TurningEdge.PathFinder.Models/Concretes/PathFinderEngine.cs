using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.Math;
using TurningEdge.Math.Structs;
using TurningEdge.PathFinder.Models.Structs;
using TurningEdge.DataStructures.PriorityQueues.Concretes;
using TurningEdge.PathFinder.Common.DataTypes;
using TurningEdge.Math.Shapes;

namespace TurningEdge.PathFinder.Models.Concretes
{
    public class PathFinderEngine
    {
        private Dictionary<Coordinate, TileType> mGrid = null;
        private PriorityQueue<Node> mOpen = new PriorityQueue<Node>(new ComparePFNode());
        private List<Node> mClose = new List<Node>();
        private bool mStop = false;
        private int mHoriz = 0;
        private int mHEstimate = 2;
        private bool mPunishChangeDirection = false;
        private bool mTieBreaker = false;
        private bool mHeavyDiagonals = false;
        private int mSearchLimit = 2000;

        public bool IsBlocked(Coordinate point)
        {
            return GetValue(point) == TileType.BLOCKED_TILE;
        }

        public bool IsOpen(Coordinate point)
        {
            return GetValue(point) == TileType.EMPTY_TILE;
        }

        public void Block(Coordinate point)
        {
            mGrid[point] = TileType.BLOCKED_TILE;
        }

        public void Unblock(Coordinate point)
        {
            mGrid[point] = TileType.EMPTY_TILE;
        }

        public bool IsBlocked(int x, int y)
        {
            return IsBlocked(new Coordinate(x, y));
        }

        public bool IsOpen(int x, int y)
        {
            return IsOpen(new Coordinate(x, y));
        }

        public void Block(int x, int y)
        {
            Block(new Coordinate(x, y));
        }

        public void Unblock(int x, int y)
        {
            Unblock(new Coordinate(x, y));
        }

        public PathFinderEngine()
        {
            mGrid = new Dictionary<Coordinate, TileType>();
        }

        public PathFinderEngine(Dictionary<Coordinate, TileType> grid)
        {
            if (grid == null)
                throw new Exception("Grid cannot be null");

            mGrid = grid;
        }
        
        private TileType GetValue(Coordinate point)
        {
            var data = TileType.BLOCKED_TILE;
            if (mGrid.TryGetValue(point, out data)) { }

            return data;
        }

        public Coordinate[] FindPath(int x1, int y1, int x2, int y2, bool findClosest = false)
        {
            return FindPath(new Coordinate(x1, y1), new Coordinate(x2, y2));
        }

        public Coordinate[] FindPath(Coordinate start, Coordinate end, bool findClosest = false)
        {
            List<Coordinate> result;

            result = (findClosest) ? GetClosestPath(start, end) : GetPath(start, end);

            return result.ToArray();
        }

        private List<Coordinate> GetPath(int x1, int y1, int x2, int y2)
        {
            return GetPath(new Coordinate(x1, y1), new Coordinate(x2, y2));
        }

        private List<Coordinate> GetPath(Coordinate start, Coordinate end)
        {
            //HighResolutionTime.Start();

            Node parentNode;
            bool found = false;
            //int  gridX  = mGrid.GetUpperBound(0);
            //int  gridY  = mGrid.GetUpperBound(1);

            mStop = false;
            mOpen.Clear();
            mClose.Clear();

            sbyte[,] direction;

            direction = new sbyte[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

            parentNode.G = 0;
            parentNode.H = mHEstimate;
            parentNode.F = parentNode.G + parentNode.H;
            parentNode.X = (int)start.x;
            parentNode.Y = (int)start.y;
            parentNode.PX = parentNode.X;
            parentNode.PY = parentNode.Y;
            mOpen.Push(parentNode);
            while (mOpen.Count > 0 && !mStop)
            {
                parentNode = mOpen.Pop();

                if (parentNode.X == end.x && parentNode.Y == end.y)
                {
                    mClose.Add(parentNode);
                    found = true;
                    break;
                }

                if (mClose.Count > mSearchLimit)
                {
                    return new List<Coordinate>();
                }

                if (mPunishChangeDirection)
                    mHoriz = (parentNode.X - parentNode.PX);

                //Lets calculate each successors
                for (int i = 0; i < 4; i++)
                {
                    Node newNode;
                    newNode.X = parentNode.X + direction[i, 0];
                    newNode.Y = parentNode.Y + direction[i, 1];

                    //if (newNode.X < 0 || newNode.Y < 0 || newNode.X >= gridX || newNode.Y >= gridY)
                    //    continue;

                    int newG;
                    if (mHeavyDiagonals && i > 3)
                        newG = parentNode.G + (int)((byte)GetValue(new Coordinate(newNode.X, newNode.Y)) * 2.41);
                    else
                        newG = parentNode.G + (byte)GetValue(new Coordinate(newNode.X, newNode.Y));


                    if (newG == parentNode.G)
                    {
                        //Unbrekeable
                        continue;
                    }

                    if (mPunishChangeDirection)
                    {
                        if ((newNode.X - parentNode.X) != 0)
                        {
                            if (mHoriz == 0)
                                newG += 20;
                        }
                        if ((newNode.Y - parentNode.Y) != 0)
                        {
                            if (mHoriz != 0)
                                newG += 20;

                        }
                    }

                    int foundInOpenIndex = -1;
                    for (int j = 0; j < mOpen.Count; j++)
                    {
                        if (mOpen[j].X == newNode.X && mOpen[j].Y == newNode.Y)
                        {
                            foundInOpenIndex = j;
                            break;
                        }
                    }
                    if (foundInOpenIndex != -1 && mOpen[foundInOpenIndex].G <= newG)
                        continue;

                    int foundInCloseIndex = -1;
                    for (int j = 0; j < mClose.Count; j++)
                    {
                        if (mClose[j].X == newNode.X && mClose[j].Y == newNode.Y)
                        {
                            foundInCloseIndex = j;
                            break;
                        }
                    }
                    if (foundInCloseIndex != -1 && mClose[foundInCloseIndex].G <= newG)
                        continue;

                    newNode.PX = parentNode.X;
                    newNode.PY = parentNode.Y;
                    newNode.G = newG;

                    newNode.H = mHEstimate * (System.Math.Max(System.Math.Abs(newNode.X - (int)end.x), System.Math.Abs(newNode.Y - (int)end.y)));
                    if (mTieBreaker)
                    {
                        int dx1 = (int)(parentNode.X - end.x);
                        int dy1 = (int)(parentNode.Y - end.y);
                        int dx2 = (int)(start.x - end.x);
                        int dy2 = (int)(start.y - end.y);
                        int cross = System.Math.Abs(dx1 * dy2 - dx2 * dy1);
                        newNode.H = (int)(newNode.H + cross * 0.001);
                    }
                    newNode.F = newNode.G + newNode.H;

                    mOpen.Push(newNode);
                }

                mClose.Add(parentNode);
            }

            //mCompletedTime = HighResolutionTime.GetTime();
            if (found)
            {
                Node fNode = mClose[mClose.Count - 1];
                for (int i = mClose.Count - 1; i >= 0; i--)
                {
                    if (fNode.PX == mClose[i].X && fNode.PY == mClose[i].Y || i == mClose.Count - 1)
                        fNode = mClose[i];
                    else
                        mClose.RemoveAt(i);
                }

                var nodes = new List<Coordinate>();

                foreach (var node in mClose)
                {
                    nodes.Add(new Coordinate(node.X, node.Y));
                }

                return FormatPath(nodes);
            }
            return new List<Coordinate>();
        }

        private List<Coordinate> GetClosestPath(int x1, int y1, int x2, int y2)
        {
            return GetClosestPath(new Coordinate(x1, y1), new Coordinate(x2, y2));
        }

        private List<Coordinate> GetClosestPath(Coordinate start, Coordinate end)
        {
            List<Coordinate> result;
            var points = new List<Coordinate>();
            Drawing.DrawLine(start, end, points);

            for (int i = points.Count - 1; i >= 0; i--)
            {
                result = GetPath(start, points[i]);

                if (result.Count > 0)
                    return result;
            }

            return null;
        }


        private List<Coordinate> FormatPath(List<Coordinate> points)
        {
            var superPath = new List<Coordinate>();
            for (int i = points.Count - 1; i >= 0; i--)
            {
                bool add = false;

                if (i - 2 >= 0)
                {
                    if
                    (
                        System.Math.Abs(points[i].x - points[i - 2].x) == 1
                        && System.Math.Abs(points[i].y - points[i - 2].y) == 1
                    )
                    {
                        var diagonalOffset = new Coordinate(points[i].x - points[i - 2].x, points[i].y - points[i - 2].y);
                        if
                        (
                            IsOpen((int)(points[i].x - diagonalOffset.x), (int)(points[i].y))
                            && IsOpen((int)(points[i].x), (int)(points[i].y - diagonalOffset.y))
                        )
                            add = true;
                    }
                }
                superPath.Insert(0, new Coordinate(points[i].x, points[i].y));
                if (add)
                    i -= 1;
            }

            return superPath;
        }

    }
}
