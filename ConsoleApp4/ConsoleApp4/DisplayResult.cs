using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class DisplayResult
    {
        private string name;
        private int age;

        public DisplayResult(string name, int age)
        {
            this.name = name;
            this.age = age;

            Console.WriteLine("Hi " + name + "you are " + age + " years old.");
        }
    }
}
