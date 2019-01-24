using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        private static int heightFeet;

        static void Main(string[] args)
        {
           
            // Get input
            Console.Write("Enter first name:");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name:");
            string lastName = Console.ReadLine();
            Console.Write("Enter your age:");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter height in inches:");
            int height = Convert.ToInt32(Console.ReadLine());
            int resultFeet = Feet.CalcHeight(height);
            int resultInches = CalcHeightInches.CalcHeightInches2(height);
            Console.WriteLine("Here is heightFeet " + resultFeet);
            Console.WriteLine("Here is heightInches " + resultInches);
            CombinedNames(firstName, lastName, age, height, resultFeet, resultInches);
        }
        

        public static void CombinedNames(string firstName, string lastName, int age, int height, int feet, int inches)
        {
            //Display output
 
            Console.WriteLine("Good morning, " + firstName + " " + lastName +"\nYou are " + height + " inches (or " + feet + " feet and " + inches + " inches tall) and " + age + " years old.");
            Console.ReadLine();
        }
    }
}
