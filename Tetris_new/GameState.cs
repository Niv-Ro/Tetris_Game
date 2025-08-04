using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_new
{
    [Serializable]
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock //crates a block currentBlock, gets its value, resets the block for its original rotation state
                                  //and sets its start position in the grid
        {
            get => currentBlock;
            private set
            {
                int i;
                currentBlock = value;
                currentBlock.Reset();
                if(currentBlock.Id!=8)
                {
                    for (i = 0; i < 1; i++)
                    {
                        currentBlock.Move(1, 0);
                        if (!Blockfits())
                        {
                            currentBlock.Move(-1, 0);
                        }
                    }
                }
            }
            

        }

        public Game_Grid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool Gameover { get; private set; }
        public int Score { get; private set; }

        public GameState() //constructor for gamestate, sets the gamegrid size, gets the blockqueue, the current block.
        {
            GameGrid = new Game_Grid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.Get_And_Update();
        }

        private bool Blockfits() //check if all the tiles that build the block fits in the grid.
        {
            foreach (Position p in CurrentBlock.Tile_Positions())
            {
                if (!GameGrid.Is_Empty(p.Rows, p.Columms))
                {
                    return false;
                }
            }
            return true;
        }

        public void RotateBlock_Clockwise() //rotates the block clockwise if possible
        {
            CurrentBlock.Rotate_Clockwise();

            if (!Blockfits())
            {
                CurrentBlock.Rotate_Counter_Clockwise();
            }
        }

        public void RotateBlock_Counter_Clockwise() //rotates the block counter clockwise if possible
        {
            CurrentBlock.Rotate_Counter_Clockwise();

            if (!Blockfits())
            {
                CurrentBlock.Rotate_Clockwise();
            }
        }

        public void Move_Block_Left() // moves the block left
        {
            CurrentBlock.Move(0, -1);
            if (!Blockfits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void Move_Block_Right() //moves the block right
        {
            CurrentBlock.Move(0, 1);
            if (!Blockfits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool Is_Game_over() //checks if the game is over(the block rached the top rows)
        {
            return !(GameGrid.Is_Row_Empty(0) && GameGrid.Is_Row_Empty(1));
        }

        private void Place_Block() //placing the block and setting the spots on the grid to the blocks' id value.
                                   // adds 1 point for each full row, checks if the game is over and if not sets next block.
        {
            foreach (Position p in CurrentBlock.Tile_Positions())
            {
                GameGrid[p.Rows, p.Columms] = CurrentBlock.Id;
            }
           
            Score +=GameGrid.Clear_Full_Rows();

            if (Is_Game_over())
            {
                Gameover = true;
            }
            else
            {
                CurrentBlock = BlockQueue.Get_And_Update();
            }
        }

        public void Move_Block_Down() //move the block down 
        {
            CurrentBlock.Move(1, 0);
            if (!Blockfits())
            {
                CurrentBlock.Move(-1, 0);
                Place_Block();
            }
        }
    }
}
