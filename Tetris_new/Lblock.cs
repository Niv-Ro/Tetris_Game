using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_new
{
    [Serializable]

    public class LBlock : PositionedBlock
    {
        private readonly Position[][] tiles = new Position[][] //creates tiles to the shape of the the block on a 2 dimention array, each line is a rotation
                                                               //of the block
            {
                new Position[]{ new Position(0,2),new Position(1,0),new Position(1,1),new Position(1,2)},
                new Position[]{ new Position(0,1),new Position(1,1),new Position(2,1),new Position(2,2)},
                new Position[]{ new Position(1,0),new Position(1,1),new Position(1,2),new Position(2,0)},
                new Position[]{ new Position(0,0),new Position(0,1),new Position(1,1),new Position(2,1)}
            };

        public override int Id => 3; //the id of the block
        protected override Position Start_Offset => new Position(0, 3); //the offset of the block
        protected override Position[][] Tiles => tiles; //sets the position of the block
    }
}
