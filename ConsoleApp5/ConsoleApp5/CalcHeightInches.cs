using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class CalcHeightInches
    {
        public static int CalcHeightInches2(int h)
        {
            int heightInches = h % 12;
            return heightInches;
        }
    }
}
