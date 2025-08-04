using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_new
{
    [Serializable]
    public abstract class Block 
    {
        protected abstract Position[][] Tiles { get; }
        
        public abstract int Id { get; }
       

        private int rotation_state;
        protected Position offset;

        public IEnumerable<Position> Tile_Positions() //sets all the tiles for the block based on its rotation state
        {
            foreach (Position p in Tiles[rotation_state])
            {
                yield return new Position(p.Rows + offset.Rows, p.Columms + offset.Columms);
            }
        }

        public void Rotate_Clockwise() //rotates block clockwise
        {
            rotation_state = (rotation_state + 1) % Tiles.Length;
        }

        public void Rotate_Counter_Clockwise() //rotate block counter clockwise
        {
            if (rotation_state == 0)
            {
                rotation_state = Tiles.Length - 1;
            } 
            else
            {
                rotation_state--;
            }
        }

        public void Move(int rows, int columms) //moves the block to its postion with its offset
        {
            offset.Rows += rows;
            offset.Columms += columms;
        }

        virtual public void Reset() //reset the rotation state back to its original state, (the func will be overidden at the block classes)
        {
            rotation_state = 0;
        }

    }
}
