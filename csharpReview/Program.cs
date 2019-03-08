using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("First number? ");
            float first = float.Parse(Console.ReadLine());
            Console.Write("Second number? ");
            float second = float.Parse(Console.ReadLine());

            Calculator calc = new Calculator();
            char operation = 'L';
            do
            {
                Console.Write("Choose Add, Sub, Mult, Divide, Integer Division");
                operation = Console.ReadLine().ToUpper()[0];

                int remainder = 0;

                switch (operation)
                {
                    case 'A':
                        calc.Add(first, second);
                        break;
                    case 'S':
                        calc.Subtract(first, second);
                        break;
                    case 'M':
                        calc.Multiply(first, second);
                        break;
                    case 'D':
                        calc.Divide(first, second);
                        break;
                    case 'I':
                        remainder = calc.Divide((int)first, (int)second);
                            break;
                    case 'Q':
                        break;
                    default:
                        Console.WriteLine("please enter A, S, M, D or Q");
                        break;


                }
                if(remainder !=0)
                {
                    Console.WriteLine("Your answer is {0} with a remainder of {1}", calc.Answer, remainder);
                }
                else
                {
                    Console.WriteLine("The answer is {0}", calc.Answer);
                }
                
            } while (operation != 'Q');
            Console.ReadLine();
        }
    }
}
