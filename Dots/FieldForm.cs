using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        DotsGame dots = new DotsGame();
        bool endGame = false;
        private DotsGrid Field;


        private void Field_MouseClick(object sender, MouseEventArgs e)
        {
            Point coords = Field.NearestCell;

            if (dots.Grid[coords.X, coords.Y].PlayerDot == null)
            {
                dots.AddDot(Turn, coords);

                Player1Score.Text = dots.Score[0].ToString();
                Player2Score.Text = dots.Score[1].ToString();

                if (!endGame)
                {
                    if (Turn == DotsGame.Player.Player1)
                    {
                        Turn = DotsGame.Player.Player2;
                        Field.SelectionColor = Field.Player2Color;
                        Player2EndGameBttn.Enabled = true;
                        Player1EndGameBttn.Enabled = false;
                    }
                    else
                    {
                        Turn = DotsGame.Player.Player1;
                        Field.SelectionColor = Field.Player1Color;
                        Player2EndGameBttn.Enabled = false;
                        Player1EndGameBttn.Enabled = true;
                    }
                }

                Field.Refresh();
            }
        }

        private void Player1EndGameBttn_Click(object sender, EventArgs e)
        {
            if (!endGame)
            {
                endGame = true;

                Turn = DotsGame.Player.Player2;
                Field.SelectionColor = Field.Player2Color;
                Player2EndGameBttn.Enabled = true;
                Player1EndGameBttn.Enabled = false;
            }
            else
            {
                DeclareWinner();
            }
        }

        private void Player2EndGameBttn_Click(object sender, EventArgs e)
        {
            if (!endGame)
            {
                endGame = true;

                Turn = DotsGame.Player.Player1;
                Field.SelectionColor = Field.Player1Color;
                Player2EndGameBttn.Enabled = false;
                Player1EndGameBttn.Enabled = true;
            }
            else
            {
                DeclareWinner();
            }
        }


        private void DeclareWinner()
        {
            int winner = dots.Score[0] > dots.Score[1] ? 1 : 2;
            int winnerScore = dots.Score[winner - 1];
            int loserScore = dots.Score[2 - winner];

            string endGameMessage = String.Format("Игрок {0} победил со счётом {1}:{2}.\nИграть ещё раз?", winner, winnerScore, loserScore);

            var dialogResult = MessageBox.Show(endGameMessage, "Игра окончена", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
            else
            {
                dots = new DotsGame();
                Field.Game = dots;
                Field.Refresh();

                Player1Score.Text = dots.Score[0].ToString();
                Player2Score.Text = dots.Score[1].ToString();

                Turn = DotsGame.Player.Player1;
                Field.SelectionColor = Field.Player1Color;
                Player2EndGameBttn.Enabled = false;
                Player1EndGameBttn.Enabled = true;

                endGame = false;
            }
        }
    }
}
