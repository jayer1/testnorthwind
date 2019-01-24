using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a date");
            DateTime dueDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Stored date is " + dueDate);

            string displayDueDate = dueDate.ToString("MM/dd/yyyy");
            Console.WriteLine("Stored date is " + displayDueDate);
            Console.ReadLine();
        }
    }
}
