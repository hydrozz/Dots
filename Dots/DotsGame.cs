using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Dots
{
    public class DotsGame
    {
        public DotsGame()
        {
            Size = 25;
            Grid = new Cell[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell(new Point(i, j));
                }
            }
        }

        public enum Player
        {
            Player1 = 0,
            Player2 = 1
        }


        public readonly Cell [,] Grid;

        public int Size { get; }


        public class Cell
        {
            public Cell(Point coordinates)
            {
                Location = coordinates;
                PlayerDot = null;
                Surrounded = false;
                Empty = true;
            }

            public Dot PlayerDot;
            public bool Surrounded;
            public bool Empty;
            public Point Location;
        }

        public class Dot
        {
            public Dot(Cell cell, Player owner)
            {
                Cell = cell;
                Neighbors = new List<Dot>();
                ParentCluster = null;
                Owner = owner;
            }

            public Cell Cell { get; }
            public Player Owner { get; }
            public List<Dot> Neighbors;
            public Cluster ParentCluster;
        }

        public class Cluster
        {
            public Cluster(Dot dot)
            {
                Dots = new List<Dot> { dot };
                ID = ++instanceCounter;
            }

            public List<Dot> Dots;
            private static int instanceCounter;
            
            public int ID { get; }
        }

        public class Contour
        {
            public Contour(List<Dot> dots)
            {
                Dots = dots;
            }

            public List<Dot> Dots;
        }


        public class Wave
        {
            public Wave(Dot start)
            {
                wave = new Queue<Dot>();
                Start = start;
                wave.Enqueue(start);
                cameFrom = new Dictionary<Dot, Dot>();
                cameFrom.Add(start, null);
            }

            private Queue<Dot> wave;
            private Dictionary<Dot, Dot> cameFrom;
            public readonly Dot Start;

            public List<Dot> GetShortestPath(Dot destination)
            {
                var path = new List<Dot>();
                Dot previous = null, current = null;

                while (wave.Count != 0)
                {
                    current = wave.Peek();
                    wave.Dequeue();

                    if (current == destination)
                        break;

                    foreach (Dot next in current.Neighbors)
                    {
                        if (next != previous && !next.Cell.Surrounded && !cameFrom.ContainsKey(next))
                        {
                            wave.Enqueue(next);
                            cameFrom.Add(next, current);
                        }
                    }

                    previous = current;
                }

                if (current != destination)
                    return null;

                while (current != Start)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }

                path.Add(Start);

                return path;
            }
        }


        public List<Cluster>[] Clusters = { new List<Cluster>(), new List<Cluster>() };
        public List<Contour>[] Contours = { new List<Contour>(), new List<Contour>() };


        public int[] Score = { 0, 0 };


        public void AddDot(Player player, Point location)
        {
            location = new Point(location.X.Clamp(0, Size - 1), location.Y.Clamp(0, Size - 1));
            var cell = Grid[location.X, location.Y];
            var newDot = new Dot(cell, player);
            cell.PlayerDot = newDot;
            Grid[location.X, location.Y].Empty = false;

            List<Point> adjacentPoints = Get8AdjacentPoints(location);

            List<Dot> neighbors = new List<Dot>();
            for (int i = 0; i < adjacentPoints.Count; i++)
            {
                if (adjacentPoints[i].X >= 0 && adjacentPoints[i].X < Size &&
                    adjacentPoints[i].Y >= 0 && adjacentPoints[i].Y < Size)
                {
                    var neighborCell = Grid[adjacentPoints[i].X, adjacentPoints[i].Y];

                    if (!neighborCell.Empty && !neighborCell.Surrounded && neighborCell.PlayerDot.Owner == player)
                    {
                        neighbors.Add(neighborCell.PlayerDot);
                    }
                }
            }

            if (neighbors.Count == 0)
            {
                var Cluster = new Cluster(newDot);
                Clusters[(int)player].Add(Cluster);
                newDot.ParentCluster = Cluster;
            }
            else if (neighbors.Count == 1)
            {
                neighbors[0].ParentCluster.Dots.Add(newDot);
                newDot.ParentCluster = neighbors[0].ParentCluster;
            }
            else
            {
                Dictionary<Cluster, List<Dot>> neighborClusters = new Dictionary<Cluster, List<Dot>>();
                Cluster superCluster = null;

                foreach (Dot neighbor in neighbors)
                {
                    if (neighborClusters.ContainsKey(neighbor.ParentCluster))
                    {
                        neighborClusters[neighbor.ParentCluster].Add(neighbor);
                    }
                    else
                    {
                        neighborClusters.Add(neighbor.ParentCluster, new List<Dot>() { neighbor });
                        if (superCluster == null)
                            superCluster = neighbor.ParentCluster;
                    }
                }

                foreach (Cluster cluster in neighborClusters.Keys)
                {
                    var clusterDots = neighborClusters[cluster];

                    if (clusterDots.Count > 1)
                    {
                        for (int i = 0; i < clusterDots.Count - 1; i++)
                        {
                            if (clusterDots[i].Neighbors.Contains(clusterDots[i + 1]))
                                continue;

                            var path = new Wave(clusterDots[i]).GetShortestPath(clusterDots[i + 1]);

                            if (path == null)
                                continue;

                            path.Add(newDot);

                            var contour = new Contour(path);
                            var surroundedCells = SurroundCells(contour, player);

                            if (surroundedCells.Count > 0)
                                Contours[(int)player].Add(contour);
                        }
                    }

                    if (cluster != superCluster)
                    {
                        foreach (Dot dot in cluster.Dots)
                        {
                            dot.ParentCluster = superCluster;
                            superCluster.Dots.Add(dot);
                        }

                        Clusters[(int)player].Remove(cluster);
                    }
                }

                newDot.ParentCluster = superCluster;
                superCluster.Dots.Add(newDot);
            }

            newDot.Neighbors = neighbors;
            foreach (Dot neighbor in neighbors)
                neighbor.Neighbors.Add(newDot);
        }

        private List<Cell> SurroundCells(Contour contour, Player player)
        {
            var surroundedCells = new List<Cell>();
            var adjacentPoints = Get8AdjacentPoints(contour.Dots[0].Cell.Location);
            Cell startPoint = null;

            foreach (Point point in adjacentPoints)
            {
                if (IsInsideContour(contour, point))
                {
                    startPoint = Grid[point.X, point.Y];
                    surroundedCells.Add(startPoint);
                    break;
                }
            }

            if (startPoint != null)
            {
                Queue<Cell> flood = new Queue<Cell>();
                Cell previous = null, current = null;
                List<Point> contourPoints = contour.Dots.Select(x => x.Cell.Location).ToList();
                List<Cell> opponentDots = new List<Cell>();
                List<Cell> playerDots = new List<Cell>();

                flood.Enqueue(startPoint);

                while (flood.Count != 0)
                {
                    current = flood.Peek();
                    flood.Dequeue();

                    adjacentPoints = Get4AdjacentPoints(current.Location);

                    foreach (Point next in adjacentPoints)
                    {
                        if (next.X >= 0 && next.X < Size &&
                            next.Y >= 0 && next.Y < Size)
                        {
                            Cell nextCell = Grid[next.X, next.Y];

                            if (nextCell == previous)
                                continue;

                            if (nextCell != previous && !surroundedCells.Contains(nextCell) && !contourPoints.Contains(next))
                            {
                                flood.Enqueue(nextCell);
                                surroundedCells.Add(nextCell);
                            }
                        }
                    }

                    previous = current;
                }

                foreach (Cell cell in surroundedCells)
                {
                    if (!cell.Empty)
                    {
                        if (cell.PlayerDot.Owner == player)
                        {
                            playerDots.Add(cell);

                            if (cell.Surrounded)
                            {
                                cell.Surrounded = false;
                                Score[1 - (int)player]--;
                            }
                        }
                        else
                        {
                            opponentDots.Add(cell);
                            
                            if (!cell.Surrounded)
                            {
                                cell.Surrounded = true;
                                Score[(int)player]++;
                            }
                        }
                    }
                    else
                    {
                        cell.Surrounded = true;
                    }
                }
            }

            return surroundedCells;
        }

        private bool IsInsideContour(Contour contour, Point point)
        {
            if (contour.Dots.Exists(d => d.Cell.Location == point))
                return false;

            int corners = contour.Dots.Count;
            int i, j =  corners - 1;
            int x = point.X, y = point.Y;
            bool inside = false;
            int[] xCoord = contour.Dots.Select(t => t.Cell.Location.X).ToArray();
            int[] yCoord = contour.Dots.Select(t => t.Cell.Location.Y).ToArray();

            for (i = 0; i < corners; i++)
            {
                if (yCoord[i] < y && yCoord[j] >= y || yCoord[j] < y && yCoord[i] >= y)
                {
                    if (xCoord[i] + (y - yCoord[i]) / (yCoord[j] - yCoord[i]) * (xCoord[j] - xCoord[i]) < x)
                    {
                        inside = !inside;
                    }
                }

                j = i;
            }

            return inside;
        }

        private List<Point> Get8AdjacentPoints(Point point)
        {
            List<Point> adjacentPoints = new List<Point>()
            {
                new Point(point.X - 1, point.Y - 1),
                new Point(point.X,     point.Y - 1),
                new Point(point.X + 1, point.Y - 1),
                new Point(point.X + 1, point.Y),
                new Point(point.X + 1, point.Y + 1),
                new Point(point.X,     point.Y + 1),
                new Point(point.X - 1, point.Y + 1),
                new Point(point.X - 1, point.Y)
            };

            return adjacentPoints;
        }

        private List<Point> Get4AdjacentPoints(Point point)
        {
            List<Point> adjacentPoints = new List<Point>()
            {
                new Point(point.X,     point.Y - 1),
                new Point(point.X + 1, point.Y),
                new Point(point.X,     point.Y + 1),
                new Point(point.X - 1, point.Y)
            };

            return adjacentPoints;
        }
    }
}
