using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("What is your name?");
            String name = Console.ReadLine();

            Console.Write("How old are you?");
            int age = Convert.ToInt32(Console.ReadLine());

            DisplayResult c1 = new DisplayResult(name, age);
        }
    }
}
