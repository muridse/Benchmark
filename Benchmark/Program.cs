using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BranchPrediction test = new BranchPrediction(100000000);
            test.Run(100);
            Console.ReadLine();
        }
    }
}
