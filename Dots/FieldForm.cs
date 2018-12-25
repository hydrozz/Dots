using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dots
{
    public partial class FieldForm : Form
    {
        public FieldForm()
        {
            Field = new DotsGrid(dots)
            {
                BackColor = Color.Transparent,
                CellSize = 20,
                DotSize = 8,
                ForeColor = Color.LightSteelBlue,
                Location = new Point(0, 0),
                Name = "Field",
                Size = new Size(520, 520),
            };
            Field.MouseClick += new MouseEventHandler(Field_MouseClick);

            Controls.Add(Field);

            InitializeComponent();
        }


        DotsGame.Player Turn = DotsGame.Player.Player1;
        DotsGame dots = new DotsGame(DotsGame.GridSize.Medium);
        private DotsGrid Field;


        private void Field_MouseClick(object sender, MouseEventArgs e)
        {
            Point coords = Field.NearestCell;

            if (dots.Grid[coords.X, coords.Y].PlayerDot == null)
            {
                dots.AddDot(Turn, coords);

                if (Turn == DotsGame.Player.Player1)
                {
                    Turn = DotsGame.Player.Player2;
                    Field.SelectionColor = Field.Player2Color;
                }
                else
                {
                    Turn = DotsGame.Player.Player1;
                    Field.SelectionColor = Field.Player1Color;
                }

                Field.Refresh();
            }
        }
    }
}
