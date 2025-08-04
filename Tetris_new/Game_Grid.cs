using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_new
{
    [Serializable]
    public class Game_Grid
    {
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columms { get; }

        public int this[int r, int c] // get and set for spesific positions on the grid
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public Game_Grid(int rows, int columms) //constructor gets rows, colomms and new grid size
        {
            Rows = rows;
            Columms = columms;
            grid = new int[rows, columms];
        }

        public bool Is_Inside(int r, int c) //checks if the position is inside the grid
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columms;
        }

        public bool Is_Empty(int r, int c) //checks if the position is empty of blocks
        {
            return Is_Inside(r, c) && grid[r, c] == 0;
        }

        public bool Is_Row_Full(int r) //checks if the row if full of blocks
        {
            int c;
            for (c = 0; c < Columms; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Is_Row_Empty(int r) //checks if the row is empty of blocks
        {
            int c;
            for (c = 0; c < Columms; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void Clear_Row(int r) //clears a row from its blocks
        {
            int c;
            for (c = 0; c < Columms; c++)
            {
                grid[r, c] = 0;
            }
        }

        private void Move_Down(int r, int numofRows) //for each row that is empty in the grid(numofRows) move the blocks down accordingly
        {
            int c;
            for (c = 0; c < Columms; c++)
            {
                grid[r + numofRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

        public int Clear_Full_Rows() //clear all the rows that are full of blocks
        {
            int r;
            int cleared = 0;
            for (r = Rows - 1; r >= 0; r--)
            {
                if (Is_Row_Full(r))
                {
                    Clear_Row(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    Move_Down(r, cleared);
                }
            }
            return cleared;
        }

    }
}
