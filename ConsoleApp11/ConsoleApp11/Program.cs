using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
           
                string watchingList = "Bread";
                watchingList = watchingList.Substring(0, watchingList.Length - 1);
                Console.WriteLine(watchingList);
            Console.ReadLine();
        }
    }
}
