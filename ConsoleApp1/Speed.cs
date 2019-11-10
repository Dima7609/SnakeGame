using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Speed
    {
        Program prog = new Program();

         public void IncreaseSpeed()
        {
            string test = prog.ReturnScore().ToString();

            if (5 >= prog.ReturnScore())
            {
                System.Threading.Thread.Sleep(200);
            }
            else if (10 >= prog.ReturnScore())
            {
                System.Threading.Thread.Sleep(150);
            }
            else if (50 >= prog.ReturnScore())
            {
                System.Threading.Thread.Sleep(50);
            }
            else if (999 >= prog.ReturnScore())
            {
                System.Threading.Thread.Sleep(40);
            }
        }
    }
}