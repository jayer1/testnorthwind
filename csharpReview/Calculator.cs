using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    class Calculator
    {
        public float Answer;

        public void Add(float number1, float number2)
        {
            Answer = number1 + number2;
        }

        public void Subtract(float number1, float number2)
        {
            Answer = number1 - number2;
        }

        public void Multiply(float number1, float number2)
        {
            Answer = number1 * number2;
        }

        public void Divide(float number1, float number2)
        {
            if (number2 == 0)
            {
                Answer = -1;
            }
            else
            {
                Answer = number1 / number2;
            }
            
        }

        // overloaded method
        public int Divide(int number1, int number2)
        {
            if (number2 == 0)
            {
                Answer = 0;
                return 0;
            }
            else
            {
                Answer = number1 / number2;
                return number1 % number2;
            }
        }


    }
}
