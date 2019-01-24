using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            string finished = "no";
            while (finished != "yes")
            {
                int[] intArray2 = new int[5] { 10, 112, 943, 149, 501 };
                int counter = 0;
                int input;
                Console.WriteLine("Guess a number between 1 and 1000");
                input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("You picked in {0}", input);
                Console.WriteLine("These numbers are smaller than yours");
                for (counter = 0; counter <= 4; counter++)
                {
                    if (input >= intArray2[counter]) {
                        
                        Console.WriteLine(intArray2[counter]);
                    }

                
                }
                Console.WriteLine("Finished? (yes or hit Enter)");
                finished = Console.ReadLine();

            }
            Console.ReadLine();
            
            
        }
    }
}
