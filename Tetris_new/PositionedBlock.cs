using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_new
{
    [Serializable]

    public abstract class PositionedBlock : Block
    {
        protected abstract Position Start_Offset { get; } //will be ovveriden in the block classes

        public PositionedBlock() // constructor of positiondblock sets the position with the offset
        {
            offset = new Position(Start_Offset.Rows, Start_Offset.Columms);
        }

        override public void Reset() //reset rotation to default and offset to blocks' default
        {
            base.Reset();
            offset.Rows = Start_Offset.Rows;
            offset.Columms = Start_Offset.Columms;
        }

    }
}
