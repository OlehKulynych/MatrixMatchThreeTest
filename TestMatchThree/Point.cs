using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMatchThree
{
    public class Point
    {
        public int start { get; set; }
        public int end { get; set; }

        public int row { get; set; }

        public Point(int start, int end, int row)
        {
            this.start = start;
            this.end = end;
            this.row = row;
        }
        
    }
}
