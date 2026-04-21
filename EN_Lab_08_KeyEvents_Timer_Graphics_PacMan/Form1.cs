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
        private Keys pacManDirection = Keys.None;

        private int score = 0;

        private const int FIELD_SIZE = 50;
        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;

            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;

            OpenMap(Properties.Resources.Map_1);

            Timer gameTimer = new Timer();
            gameTimer.Interval = 200;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            switch(pacManDirection)
            {
                case Keys.Down:
                    if (board[pacMan.Y + 1, pacMan.X] != FieldType.Wall) pacMan.Y++;
                    break;
                case Keys.Up:
                    if (board[pacMan.Y - 1, pacMan.X] != FieldType.Wall) pacMan.Y--; 
                    break;
                case Keys.Left:
                    if (board[pacMan.Y , pacMan.X - 1] != FieldType.Wall) pacMan.X--; 
                    break;
                case Keys.Right:
                    if (board[pacMan.Y, pacMan.X + 1] != FieldType.Wall) pacMan.X++;
                    break;
            }
            if(board[pacMan.Y, pacMan.X] == FieldType.Dot)
            {
                board[pacMan.Y, pacMan.X] = FieldType.Empty;
                score++;
            }
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right)
            {
                pacManDirection = e.KeyCode;
            }
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
            e.Graphics.DrawString("Score: " + score,
                                  new Font(Font.FontFamily, FIELD_SIZE/2),
                                  Brushes.White,
                                  0,
                                  0);
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
