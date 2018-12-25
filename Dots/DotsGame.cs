using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dots
{
    public class DotsGame
    {
        public DotsGame(GridSize size)
        {
            Size = size;
            Grid = new Cell[(int)Size, (int)Size];

            for (int i = 0; i < (int)Size; i++)
            {
                for (int j = 0; j < (int)Size; j++)
                {
                    Grid[i, j] = new Cell();
                }
            }
        }


        public enum GridSize
        {
            Small = 15,
            Medium = 25,
            Large = 35
        }

        public enum Player
        {
            Player1 = 0,
            Player2 = 1
        }


        public readonly Cell [,] Grid;

        public GridSize Size { get; }


        public class Cell
        {
            public Cell()
            {
                PlayerDot = null;
                Surrounded = false;
                Empty = true;
            }

            public Dot PlayerDot;
            public bool Surrounded;
            public bool Empty;
        }

        public class Dot
        {
            public Dot(Point coordinates, Player owner)
            {
                Location = coordinates;
                Neighbors = new List<Dot>();
                ParentSpline = null;
                Owner = owner;
                Index = 0;
            }

            public Point Location { get; }
            public Player Owner { get; }
            public List<Dot> Neighbors;
            public Spline ParentSpline;
            public int Index;
        }

        public class Spline
        {
            public Spline(Dot dot)
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


        public List<Spline>[] Splines = { new List<Spline>(), new List<Spline>() };
        public List<Contour>[] Contours = { new List<Contour>(), new List<Contour>() };

        public void AddDot(Player player, Point location)
        {
            location = new Point(location.X.Clamp(0, (int)Size - 1), location.Y.Clamp(0, (int)Size - 1));
            var newDot = new Dot(location, player);
            Grid[location.X, location.Y].PlayerDot = newDot;

            List<Point> adjacentPoints = new List<Point>()
            {
                new Point(location.X - 1, location.Y - 1),
                new Point(location.X,     location.Y - 1),
                new Point(location.X + 1, location.Y - 1),
                new Point(location.X - 1, location.Y),
                new Point(location.X + 1, location.Y),
                new Point(location.X - 1, location.Y + 1),
                new Point(location.X,     location.Y + 1),
                new Point(location.X + 1, location.Y + 1)
            };

            List<Dot> neighbors = new List<Dot>();
            for (int i = 0; i < adjacentPoints.Count && adjacentPoints[i].X >= 0 && adjacentPoints[i].X < (int)Size &&
                                                        adjacentPoints[i].Y >= 0 && adjacentPoints[i].Y < (int)Size; i++)
            {
                if (!Grid[adjacentPoints[i].X, adjacentPoints[i].Y].Empty &&
                    Grid[adjacentPoints[i].X, adjacentPoints[i].Y].PlayerDot.Owner == Player.Player1)
                {
                    neighbors.Add(Grid[adjacentPoints[i].X, adjacentPoints[i].Y].PlayerDot);
                }
            }

            newDot.Neighbors = neighbors;

            if (neighbors.Count == 0)
            {
                var spline = new Spline(newDot);
                Splines[(int)player].Add(spline);
                newDot.ParentSpline = spline;
            }
            else if (neighbors.Count == 1)
            {
                neighbors[0].ParentSpline.Dots.Add(newDot);
                newDot.ParentSpline = neighbors[0].ParentSpline;
                newDot.Index = neighbors[0].Index + 1;
            }
            else if (neighbors.Count == 2)
            {
                if (neighbors[0].ParentSpline.ID == neighbors[1].ParentSpline.ID)
                {
                    neighbors[0].ParentSpline.Dots.Add(newDot);
                    newDot.ParentSpline = neighbors[0].ParentSpline;
                    newDot.Index = Math.Max(neighbors[0].Index + 1, neighbors[1].Index + 1);

                    List<Dot> contour = new List<Dot>();

                    //
                    // finding the contour in newDot.ParentSpline
                    //

                }
                else
                {

                }
            }
            else
            {

            }
        }
    }
}
