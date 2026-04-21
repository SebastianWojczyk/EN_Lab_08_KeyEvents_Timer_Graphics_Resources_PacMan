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
        private Point PacMan;
        public Form1()
        {
            InitializeComponent();

            OpenMap(Properties.Resources.Map_1);
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
                                  PacMan = new Point(col, row);
                                  break;
                    }
                }
            }

        }
    }
}
