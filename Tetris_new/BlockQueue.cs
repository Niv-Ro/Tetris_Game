using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tetris_new
{
    [Serializable]
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[] //creates array of the posible blocks
            {
                new IBlock(),
                new JBlock(),
                new LBlock(),
                new OBlock(),
                new SBlock(),
                new TBlock(),
                new ZBlock(),
                new TeleportingBlock()

            };

        private readonly Random random = new Random();
        
        public Block Next_Block { get; private set; } 

        public BlockQueue() //constructor, the next block will be random from the block queue
        {
            Next_Block = Random_Block();
        }
        private Block Random_Block() //func that returnes the next random block from the queue
        {
            return blocks[random.Next(blocks.Length)];
        }


        public Block Get_And_Update() // updates next block
        {
            Block block = Next_Block;
            do
            {
                Next_Block = Random_Block();
            }
            while (block.Id == Next_Block.Id);
            return block;
        }


    }
}
