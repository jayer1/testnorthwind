using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {

            string line = "40,'Cry, the Beloved Country (1995)',Drama,Dogs,Cats,People";


                // we have a comma in the title
                int idx = -1;
                idx = line.IndexOf(",");
                string first = line.Substring(0, idx);
                line = line.Substring(idx + 1);
                idx = line.IndexOf("\'", 1);
                string second = line.Substring(1, idx - 1);
                string third = line.Substring(idx + 2);
                var values = third.Split(',');
                string fourth = values[0];
                string fifth = values[1];
                string sixth = values[2];
                string seventh = values[3];
                Console.WriteLine(first);
                Console.WriteLine(second);
                Console.WriteLine("third is " + third);
                Console.WriteLine(fourth);
                Console.WriteLine(fifth);
                Console.WriteLine(sixth);
                Console.WriteLine(seventh);
                Console.ReadLine();

            }
        }
}
