using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ColorC
    {
        public void TestFrame()
        {
            Console.Write("Name a color: ");
            string color = Console.ReadLine();

            ConsoleColor consoleColor = ConsoleColor.White;
            try
            {
                consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
            }
            catch (Exception)
            {
                //Invalid color
            }
            Console.ForegroundColor = consoleColor;
        }
    }
}