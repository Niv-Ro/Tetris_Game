using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_new
{
    [Serializable]

    public class TeleportingBlock : Block
    {
        private readonly Position[][] tiles = new Position[][] //creates tiles to the shape of the the block on a 2 dimention array, each line is a rotation
                                                               //of the block
        {
            new Position[]{ new Position(0,1), new Position(1,0), new Position(1,1), new Position(1,2),new Position(2,1) },
        };
        public override int Id => 8; //the id of the block
        protected override Position[][] Tiles => tiles; //sets the position of the block

        private Random random;

        public TeleportingBlock() //the offset of the block
        {
            offset = GenerateRandomPosition(); 
        }
        private Position GenerateRandomPosition() //generating a random position on the upper grid 
        {
            random = new Random();
            int rand_rows = random.Next(0,4);
            int rand_cols = random.Next(1, 8);
            return new Position(rand_rows, rand_cols); 
        }


        override public void Reset() //resets the rotation to default and the offset to the random position
        {
            base.Reset();
            offset = GenerateRandomPosition();
        }

    }
}

    
