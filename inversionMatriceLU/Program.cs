using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] m1 = { { 1, 1, 1 }, { 0, 1, 0 }, { 1, 2, 3 } };
            MatriceCarre test1 = new MatriceCarre(m1);
            double[,] m2 = { { 1, 1, 1 }, { 1, 1, 2 }, { 1, 2, 2 } };
            MatriceCarre test2 = new MatriceCarre(m2);

            Console.WriteLine(test1 != test2);
            Console.WriteLine(DecompositionLU.Inversion(test2)["A"]);
            Console.WriteLine(DecompositionLU.Inversion(test2)["L"]);
        }
    }
}
