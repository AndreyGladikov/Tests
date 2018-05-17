using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacchi
{
    class Program
    {
        static void Main(string[] args)
        {
            int res = FibonachiHelper.WriteFibonacciSequenceToFile("D:\\test.txt", 5);
            if (res >= 0)
            {
                Console.WriteLine(res.ToString() + " elements were written to file");
            }
            else
            {
                Console.WriteLine("error");
            }
            bool really = FibonachiHelper.CheckFibonachiSequence("D:\\test.txt");
            if (really) Console.WriteLine("zbs");
            else Console.WriteLine("ne zbs");
        }
    }
}
