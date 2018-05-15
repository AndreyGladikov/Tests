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
            int res = FibonachiHelper.WriteFibonacciSequenceToFile("D:\\fbn.txt", 5);
            Console.WriteLine(res.ToString() + " elements were written to file");
        }
    }
}
