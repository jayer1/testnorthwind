using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            Console.WriteLine("Make a summary ");
            string str = Console.ReadLine();
            int index = str.IndexOfAny(specialCharactersArray);
            //index == -1 no special characters
            while (index != -1)
            {
                Console.WriteLine("Your summary has special character, please re-enter the summary");
                str = Console.ReadLine();
                index = str.IndexOfAny(specialCharactersArray);
            }
            Console.WriteLine("You wrote {0}", str);
            Console.ReadLine();
        }
    }
}

