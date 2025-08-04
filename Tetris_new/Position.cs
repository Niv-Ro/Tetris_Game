using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tetris_new
{    
    [Serializable]
    public class Position
    {
        public int Rows { get; set; }
        public int Columms { get; set; }

        public Position(int rows, int columms) //constructor of position, sets rows and cols
        {
            Rows = rows;
            Columms = columms;
        }


    }
}
