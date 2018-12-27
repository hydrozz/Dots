namespace Dots
{
    partial class FieldForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FieldForm));
            this.ScorePanel = new System.Windows.Forms.Panel();
            this.Player2EndGameBttn = new System.Windows.Forms.Button();
            this.Player1EndGameBttn = new System.Windows.Forms.Button();
            this.Player2Label = new System.Windows.Forms.Label();
            this.Player1Label = new System.Windows.Forms.Label();
            this.Player2Score = new System.Windows.Forms.Label();
            this.Player1Score = new System.Windows.Forms.Label();
            this.ScorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScorePanel
            // 
            this.ScorePanel.BackColor = System.Drawing.Color.LightGray;
            this.ScorePanel.Controls.Add(this.Player2EndGameBttn);
            this.ScorePanel.Controls.Add(this.Player1EndGameBttn);
            this.ScorePanel.Controls.Add(this.Player2Label);
            this.ScorePanel.Controls.Add(this.Player1Label);
            this.ScorePanel.Controls.Add(this.Player2Score);
            this.ScorePanel.Controls.Add(this.Player1Score);
            this.ScorePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ScorePanel.Location = new System.Drawing.Point(0, 520);
            this.ScorePanel.Name = "ScorePanel";
            this.ScorePanel.Size = new System.Drawing.Size(520, 60);
            this.ScorePanel.TabIndex = 0;
            // 
            // Player2EndGameBttn
            // 
            this.Player2EndGameBttn.Enabled = false;
            this.Player2EndGameBttn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Player2EndGameBttn.Location = new System.Drawing.Point(428, 7);
            this.Player2EndGameBttn.Name = "Player2EndGameBttn";
            this.Player2EndGameBttn.Size = new System.Drawing.Size(85, 47);
            this.Player2EndGameBttn.TabIndex = 5;
            this.Player2EndGameBttn.Text = "Завершить игру";
            this.Player2EndGameBttn.UseVisualStyleBackColor = true;
            this.Player2EndGameBttn.Click += new System.EventHandler(this.Player2EndGameBttn_Click);
            // 
            // Player1EndGameBttn
            // 
            this.Player1EndGameBttn.Location = new System.Drawing.Point(7, 7);
            this.Player1EndGameBttn.Name = "Player1EndGameBttn";
            this.Player1EndGameBttn.Size = new System.Drawing.Size(85, 47);
            this.Player1EndGameBttn.TabIndex = 4;
            this.Player1EndGameBttn.Text = "Завершить игру";
            this.Player1EndGameBttn.UseVisualStyleBackColor = true;
            this.Player1EndGameBttn.Click += new System.EventHandler(this.Player1EndGameBttn_Click);
            // 
            // Player2Label
            // 
            this.Player2Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Player2Label.Location = new System.Drawing.Point(269, 4);
            this.Player2Label.Name = "Player2Label";
            this.Player2Label.Size = new System.Drawing.Size(108, 20);
            this.Player2Label.TabIndex = 3;
            this.Player2Label.Text = "Игрок 2";
            this.Player2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player1Label
            // 
            this.Player1Label.Location = new System.Drawing.Point(132, 4);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(108, 20);
            this.Player1Label.TabIndex = 2;
            this.Player1Label.Text = "Игрок 1";
            this.Player1Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Player2Score
            // 
            this.Player2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Player2Score.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Player2Score.Location = new System.Drawing.Point(269, 17);
            this.Player2Score.Name = "Player2Score";
            this.Player2Score.Size = new System.Drawing.Size(114, 34);
            this.Player2Score.TabIndex = 1;
            this.Player2Score.Text = "0";
            this.Player2Score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player1Score
            // 
            this.Player1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Player1Score.Location = new System.Drawing.Point(132, 17);
            this.Player1Score.Name = "Player1Score";
            this.Player1Score.Size = new System.Drawing.Size(114, 34);
            this.Player1Score.TabIndex = 0;
            this.Player1Score.Text = "0";
            this.Player1Score.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(520, 580);
            this.Controls.Add(this.ScorePanel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(0)))), ((int)(((byte)(150)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FieldForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dots";
            this.ScorePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ScorePanel;
        private System.Windows.Forms.Label Player1Score;
        private System.Windows.Forms.Label Player2Score;
        private System.Windows.Forms.Label Player2Label;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.Button Player2EndGameBttn;
        private System.Windows.Forms.Button Player1EndGameBttn;
    }
}

