using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EN_Lab_08_KeyEvents_Timer_Graphics_PacMan
{
    public partial class Form1 : Form
    {
        private enum FieldType { Empty, Wall, Dot };
        private FieldType [,]board;
        private Point pacMan;

        private const int FIELD_SIZE = 50;
        public Form1()
        {
            InitializeComponent();

            this.Paint += Form1_Paint;

            OpenMap(Properties.Resources.Map_1);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if(board[row, col] == FieldType.Wall)
                    {
                        e.Graphics.DrawImage(Properties.Resources.FieldWall,
                                                 col * FIELD_SIZE,
                                                 row * FIELD_SIZE,
                                                 FIELD_SIZE,
                                                 FIELD_SIZE);
                    }
                    else if (board[row, col] == FieldType.Dot)
                    {
                        e.Graphics.FillEllipse(Brushes.White,
                                                 col * FIELD_SIZE,
                                                 row * FIELD_SIZE,
                                                 FIELD_SIZE,
                                                 FIELD_SIZE);
                    }
                }
            }
            e.Graphics.DrawImage(Properties.Resources.PacMan,
                                 pacMan.X * FIELD_SIZE,
                                 pacMan.Y * FIELD_SIZE,
                                 FIELD_SIZE,
                                 FIELD_SIZE);
        }

        private void OpenMap(string map)
        {
            //remove '\r' character
            map = map.Replace("\r", "");
            //split file into lines
            string [] lines = map.Split('\n');
            board = new FieldType[lines.Length, lines[0].Length];

            for(int row=0; row< lines.Length; row++)
            {
                for(int col=0; col< lines[row].Length; col++)
                {
                    switch(lines[row][col])
                    {
                        case '#': board[row, col] = FieldType.Wall; break;
                        case '.': board[row, col] = FieldType.Dot; break;
                        case ' ': board[row, col] = FieldType.Empty; break;
                        case 'P': board[row, col] = FieldType.Empty;
                                  pacMan = new Point(col, row);
                                  break;
                    }
                }
            }
            //set size of the window and block resizing
            this.ClientSize = new Size(lines[0].Length * FIELD_SIZE, lines.Length * FIELD_SIZE);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            Invalidate();
        }
    }
}
