using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Dots
{
    public partial class DotsGrid : UserControl
    {
        public DotsGrid()
        {
            DoubleBuffered = true;
            Game = new DotsGame();

            InitializeComponent();
        }

        public DotsGrid(DotsGame dotsGame)
        {
            DoubleBuffered = true;
            Game = dotsGame;

            InitializeComponent();
        }


        private static Color[] playersColors = { Color.FromArgb(33, 0, 150), Color.FromArgb(184, 24, 24) };


        public DotsGame Game;

        private int cellSize = 20;
        private Point nearestPoint = new Point(-1, -1);
        public Point NearestCell = new Point(-1, -1);
        private int dotSize = 8;
        private int dotEmptySize = 10;
        private Color selectionColor = playersColors[0];
        private Color player1Color = playersColors[0];
        private Color player2Color = playersColors[1];
        private bool mouseOver = false;


        [DefaultValue(15), Category("Layout"), Description("Field grid cell size")]
        public int CellSize
        {
            get => cellSize;
            set
            {
                cellSize = value;
                Invalidate();
            }
        }

        [DefaultValue(10), Category("Appearance"), Description("Dot size")]
        public int DotSize
        {
            get => dotSize;
            set
            {
                dotSize = value;
                Invalidate();
            }
        }

        [Category("Appearance"), Description("Current selection color")]
        public Color SelectionColor
        {
            get => selectionColor;
            set
            {
                selectionColor = value;
                Invalidate();
            }
        }

        [Category("Appearance"), Description("First player's dots color")]
        public Color Player1Color
        {
            get => player1Color;
            set
            {
                player1Color = value;
                Invalidate();
            }
        }

        [Category("Appearance"), Description("Second player's dots color")]
        public Color Player2Color
        {
            get => player2Color;
            set
            {
                player2Color = value;
                Invalidate();
            }
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int closestPointX = ((int)Math.Round((double)e.X / (double)cellSize)).Clamp(1, Game.Size) * cellSize;
            int closestPointY = ((int)Math.Round((double)e.Y / (double)cellSize)).Clamp(1, Game.Size) * cellSize;
            Point p = new Point(closestPointX, closestPointY);
            
            if (p != nearestPoint)
            {
                nearestPoint = p;
                NearestCell = new Point(closestPointX / 20 - 1, closestPointY / 20 - 1);
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            mouseOver = false;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            mouseOver = true;
            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            int gridLines = Game.Size;
            int gridSize = (Game.Size + 1) * cellSize;

            for (int i = 0; i < gridLines; i++)
            {
                int x = (i + 1) * cellSize;

                using (Pen p = new Pen(ForeColor))
                {
                    pevent.Graphics.DrawLine(p, x, 0, x, gridSize);
                    pevent.Graphics.DrawLine(p, 0, x, gridSize, x);
                }
            }

            if (mouseOver && nearestPoint.X >= 0 && NearestCell.X >= 0 && Game.Grid[NearestCell.X, NearestCell.Y].PlayerDot == null)
            {
                using (Pen p = new Pen(selectionColor))
                {
                    pevent.Graphics.DrawEllipse(p, nearestPoint.X - dotEmptySize/2, nearestPoint.Y - dotEmptySize/2, dotEmptySize, dotEmptySize); 
                }
            }

            for (int i = 0; i < gridLines; i++)
            {
                for (int j = 0; j < gridLines; j++)
                {
                    if (Game.Grid[i, j].PlayerDot == null)
                        continue;

                    Point dotLocation = new Point((i + 1) * cellSize, (j + 1) * cellSize);

                    if (Game.Grid[i, j].PlayerDot.Owner == DotsGame.Player.Player1)
                    {
                        using (SolidBrush b = new SolidBrush(player1Color))
                        {
                            pevent.Graphics.FillEllipse(b, dotLocation.X - dotSize / 2, dotLocation.Y - dotSize / 2, dotSize, dotSize);
                        }
                    }
                    else
                    {
                        using (SolidBrush b = new SolidBrush(player2Color))
                        {
                            pevent.Graphics.FillEllipse(b, dotLocation.X - dotSize / 2, dotLocation.Y - dotSize / 2, dotSize, dotSize);
                        }
                    }
                }
            }

            for (int playerIndex = 0; playerIndex < 2; playerIndex++)
            {
                for (int i = 0; i < Game.Contours[playerIndex].Count; i++)
                {
                    using (Pen p = new Pen(playersColors[playerIndex]))
                    {
                        using (SolidBrush b = new SolidBrush(Color.FromArgb(80, playersColors[playerIndex])))
                        {
                            var points = Game.Contours[playerIndex][i].Dots.Select(x => new Point((x.Cell.Location.X + 1) * 20, (x.Cell.Location.Y + 1) * 20)).ToArray();
                            pevent.Graphics.DrawPolygon(p, points);
                            pevent.Graphics.FillPolygon(b, points);
                        }
                    }
                }
            }
        }
    }
}